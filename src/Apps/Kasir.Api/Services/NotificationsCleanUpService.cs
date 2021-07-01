using Kasir.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Api.Services
{
    internal class NotificationsCleanUpService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private Timer _timer;

        public NotificationsCleanUpService(ILogger<NotificationsCleanUpService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            var now = DateTimeOffset.Now;

            var minutes = 60 - (now.Minute + 7) % 60;
            _logger.LogInformation($"Minutes is ${minutes}");

            _timer = new Timer(async (s) => await DoWork(s), null, TimeSpan.FromMinutes(minutes),
                TimeSpan.FromMinutes(60));

            return Task.CompletedTask;
        }

        private async Task DoWork(object state)
        {
            _logger.LogInformation($"Timed Background Service is working. {DateTimeOffset.Now.Minute}");
            using var scope = serviceScopeFactory.CreateScope();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            await notificationService.CleanTicketsAsync();

            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
