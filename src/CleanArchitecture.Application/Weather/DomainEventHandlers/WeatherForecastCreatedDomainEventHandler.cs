using CleanArchitecture.Application.Abstractions.DomainEventHandlers;
using CleanArchitecture.Core.Weather.DomainEvents;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Core.Abstractions.Services;

namespace CleanArchitecture.Application.Weather.DomainEventHandlers
{
    public sealed class WeatherForecastCreatedDomainEventHandler : DomainEventHandler<WeatherForecastCreatedDomainEvent>
    {
        private readonly INotificationsService _notificationsService;

        public WeatherForecastCreatedDomainEventHandler(ILogger<DomainEventHandler<WeatherForecastCreatedDomainEvent>> logger,
            INotificationsService notificationsService) : base(logger)
        {
            _notificationsService = notificationsService;
        }

        protected override async Task OnHandleAsync(WeatherForecastCreatedDomainEvent @event)
        {
            if (IsExtremeTemperature(@event.Temperature))
            {
                Logger.LogWarning("{summary} temperature alert - {temperature}C", @event.Summary, @event.Temperature);
                await _notificationsService.WeatherAlertAsync(@event.Summary, @event.Temperature, @event.Date);
            }
        }

        private bool IsExtremeTemperature(int temperatureC)
        {
            return temperatureC < 0 || temperatureC > 40;
        }
    }
}
