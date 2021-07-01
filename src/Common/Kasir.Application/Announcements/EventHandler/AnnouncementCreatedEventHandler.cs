using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Domain.Event;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Announcements.EventHandler
{
    public class AnnouncementCreatedEventHandler : INotificationHandler<DomainEventNotification<AnnouncementCreatedEvent>>
    {
        private readonly ILogger<AnnouncementCreatedEventHandler> _logger;
        private readonly INotificationService notificationService;

        public AnnouncementCreatedEventHandler(ILogger<AnnouncementCreatedEventHandler> logger, INotificationService notificationService)
        {
            _logger = logger;
            this.notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<AnnouncementCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Kasir Kasir.Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            if (domainEvent.Announcement != null)
            {
                await notificationService.SendNotifiactionAsync(domainEvent.Announcement.Title, domainEvent.Announcement.Body, cancellationToken);
            }
        }
    }
}
