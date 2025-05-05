using CleanArchitecture.Application.Abstractions.IntegrationEvents;
using CleanArchitecture.Core.Abstractions.Services;
using Microsoft.Extensions.Logging;
using MiniTransit;

namespace CleanArchitecture.Application.Weather.IntegrationEvents
{
    public sealed record WeatherForecastCreatedEvent(Guid WeatherForecastId, int Temperature, string Summary, DateTime Date, string CorrelationId) : IntegrationEvent(CorrelationId);

    public sealed class WeatherForecastCreatedEventHandler : IConsumer<WeatherForecastCreatedEvent>
    {
        private readonly INotificationsService _notificationsService;
        private readonly ILogger<WeatherForecastCreatedEventHandler> _logger;

        public WeatherForecastCreatedEventHandler(
            INotificationsService notificationsService,
            ILogger<WeatherForecastCreatedEventHandler> logger)
        {
            _notificationsService = notificationsService;
            _logger = logger;
        }

        public async Task ConsumeAsync(ConsumeContext<WeatherForecastCreatedEvent> context)
        {
            var @event = context.Message;
            _logger.LogInformation("Processing Weather Forecast: {id}", @event.WeatherForecastId);
            if (IsExtremeTemperature(@event.Temperature))
            {
                _logger.LogWarning("{summary} temperature alert - {temperature}C", @event.Summary, @event.Temperature);
                await _notificationsService.WeatherAlertAsync(@event.Summary, @event.Temperature, @event.Date);
            }
        }

        private static bool IsExtremeTemperature(int temperatureC)
        {
            return temperatureC < 0 || temperatureC > 40;
        }
    }
}
