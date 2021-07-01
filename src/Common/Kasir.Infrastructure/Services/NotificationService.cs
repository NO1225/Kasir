using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expo.Server.Client;
using Expo.Server.Models;
using Kasir.Application.Common.Interfaces;
using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kasir.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> logger;
        private readonly IApplicationDbContext context;
        private readonly PushApiClient expoSDKClient;

        public class OurPushTicketRequest : PushTicketRequest
        {
            public PushToken PushToken { get; set; }
        }

        public NotificationService(
            ILogger<NotificationService> logger,
            IApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
            this.expoSDKClient = new PushApiClient();
        }

        public async Task SendNotifiactionAsync(string title, string body, object content = null, CancellationToken cancellationToken = default)
        {
            var pushTokens = await context
                .PushTokens
                .Where(pt => pt.Valid)
                .ToListAsync(cancellationToken);

            if (pushTokens == null || pushTokens.Count == 0)
                return;

            foreach (var pushToken in pushTokens)
            {
                var pushTicketReq = new OurPushTicketRequest()
                {
                    PushTo = new List<string>() { pushToken.Token },
                    PushToken = pushToken,
                    PushBadgeCount = 1,
                    PushBody = body,
                    PushTitle = title,
                    PushSound = "default",
                    PushData = content
                };
                await SendAsync(pushTicketReq, cancellationToken);
            }
            
        }

        public async Task CleanTicketsAsync(CancellationToken cancellationToken = default)
        {
            var pushTickets = await context.PushTickets.ToListAsync(cancellationToken);

            var pushReceiptReq = new PushReceiptRequest()
            {
                PushTicketIds = pushTickets.Select(pt => pt.ReceiptId).ToList(),
            };

            var pushReceiptResult = expoSDKClient.PushGetReceiptsAsync(pushReceiptReq).GetAwaiter().GetResult();

            if (pushReceiptResult?.ErrorInformations?.Count > 0)
            {
                foreach (var error in pushReceiptResult.ErrorInformations)
                {
                    Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
                }
            }

            foreach (var pushReceipt in pushReceiptResult.PushTicketReceipts)
            {
                Console.WriteLine($"TicketId & Delivery Status: {pushReceipt.Key} {pushReceipt.Value.DeliveryStatus} {pushReceipt.Value.DeliveryMessage}");
                var ticket = pushTickets.FirstOrDefault(pt => pt.ReceiptId == pushReceipt.Key);

                if (pushReceipt.Value.DeliveryStatus != "ok")
                {
                   
                    var pushToken = await context.PushTokens.FirstOrDefaultAsync(p => p.Id == ticket.PushTokenId, cancellationToken);
                    pushToken.Valid = false;
                    //await pushTokenRepository.UpdateAsync(pushToken, cancellationToken);
                }
                context.PushTickets.Remove(ticket);
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task SendAsync(OurPushTicketRequest pushTicketReq, CancellationToken cancellationToken)
        {
            try
            {
                var result = await expoSDKClient.PushSendAsync(pushTicketReq);

                if (result?.PushTicketErrors?.Count > 0)
                {
                    foreach (var error in result.PushTicketErrors)
                    {
                        Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
                    }
                }

                if (result?.PushTicketStatuses?.Count > 0)
                {
                    for (int i = 0; i < result.PushTicketStatuses.Count; i++)
                    {
                        PushTicketStatus ticket = result.PushTicketStatuses[i];
                        await context.PushTickets.AddAsync(new PushTicket
                        {
                            ReceiptId = ticket.TicketId,
                            PushTokenId = pushTicketReq.PushToken.Id,
                        }, cancellationToken);
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Error: ${e.Message}");
            }

        }

    }
}
