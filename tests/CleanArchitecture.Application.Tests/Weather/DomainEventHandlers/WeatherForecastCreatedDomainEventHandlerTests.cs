using CleanArchitecture.Application.Weather.DomainEventHandlers;
using CleanArchitecture.Application.Weather.IntegrationEvents;
using CleanArchitecture.Core.Weather.DomainEvents;
using Microsoft.Extensions.Logging;
using MiniTransit;

namespace CleanArchitecture.Application.Tests.Weather.DomainEventHandlers
{
    public class WeatherForecastCreatedDomainEventHandlerTests
    {
        private readonly WeatherForecastCreatedDomainEventHandler _handler;
        private readonly Mock<IBus> _eventBus = new Mock<IBus>();

        public WeatherForecastCreatedDomainEventHandlerTests()
        {
            _handler = new WeatherForecastCreatedDomainEventHandler(Mock.Of<ILogger<WeatherForecastCreatedDomainEventHandler>>(), _eventBus.Object);
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandle_ThenPublishIntegrationEvent()
        {
            var @event = new WeatherForecastCreatedDomainEvent(Guid.NewGuid(), 25, "Sunny", DateTime.UtcNow);
            Func<Task> action = () => _handler.Handle(@event, default);
            await action.Should().NotThrowAsync();
            _eventBus.Verify(e => e.PublishAsync(It.IsAny<WeatherForecastCreatedEvent>()), Times.Once);
        }
    }
}
