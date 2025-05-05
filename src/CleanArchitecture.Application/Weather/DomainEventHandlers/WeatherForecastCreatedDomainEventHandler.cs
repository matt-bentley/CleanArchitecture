using CleanArchitecture.Application.Abstractions.DomainEventHandlers;
using CleanArchitecture.Application.Weather.IntegrationEvents;
using CleanArchitecture.Core.Weather.DomainEvents;
using Microsoft.Extensions.Logging;
using MiniTransit;

namespace CleanArchitecture.Application.Weather.DomainEventHandlers
{
    public sealed class WeatherForecastCreatedDomainEventHandler : DomainEventHandler<WeatherForecastCreatedDomainEvent>
    {
        private readonly IBus _eventBus;

        public WeatherForecastCreatedDomainEventHandler(ILogger<DomainEventHandler<WeatherForecastCreatedDomainEvent>> logger,
            IBus eventBus) : base(logger)
        {
            _eventBus = eventBus;
        }

        protected override async Task OnHandleAsync(WeatherForecastCreatedDomainEvent @event)
        {
            await _eventBus.PublishAsync(new WeatherForecastCreatedEvent(@event.Id, @event.Temperature, @event.Summary, @event.Date, CorrelationId));
        }
    }
}
