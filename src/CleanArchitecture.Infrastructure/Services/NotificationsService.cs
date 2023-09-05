using CleanArchitecture.Core.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services
{
    internal sealed class NotificationsService : INotificationsService
    {
        private readonly ILogger<NotificationsService> _logger;

        public NotificationsService(ILogger<NotificationsService> logger)
        {
            _logger = logger;
        }

        public Task WeatherAlertAsync(string summary, int temperatureC, DateTime date)
        {
            // This class is included for demonstration only
            // In a real app it would integrate with an SMTP server or messaging service
            _logger.LogInformation("Send Weather Alert Notification");
            return Task.CompletedTask;
        }
    }
}
