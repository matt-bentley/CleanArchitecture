using CleanArchitecture.Application.Weather.IntegrationEvents;
using CleanArchitecture.Core.Abstractions.Services;
using Microsoft.Extensions.Logging;
using MiniTransit;
using MiniTransit.Subscriptions;

namespace CleanArchitecture.Application.Tests.Weather.IntegrationEvents
{
    public class WeatherForecastCreatedEventTests
    {
        private readonly WeatherForecastCreatedEventHandler _handler;
        private readonly Mock<INotificationsService> _notificationsService = new Mock<INotificationsService>();
        private readonly string _correlationId = Guid.NewGuid().ToString();

        public WeatherForecastCreatedEventTests()
        {
            _handler = new WeatherForecastCreatedEventHandler(_notificationsService.Object, Mock.Of<ILogger<WeatherForecastCreatedEventHandler>>());
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandleHotTemperature_ThenSendAlert()
        {
            var context = GenerateContext(new WeatherForecastCreatedEvent(Guid.NewGuid(), 50, "Hot", DateTime.UtcNow, _correlationId));
            Func<Task> action = () => _handler.ConsumeAsync(context);
            await action.Should().NotThrowAsync();
            _notificationsService.Verify(e => e.WeatherAlertAsync("Hot", 50, It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandleColdTemperature_ThenSendAlert()
        {
            var context = GenerateContext(new WeatherForecastCreatedEvent(Guid.NewGuid(), -1, "Cold", DateTime.UtcNow, _correlationId));
            Func<Task> action = () => _handler.ConsumeAsync(context);
            await action.Should().NotThrowAsync();
            _notificationsService.Verify(e => e.WeatherAlertAsync("Cold", -1, It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandleNormalTemperature_ThenDontSendAlert()
        {
            var context = GenerateContext(new WeatherForecastCreatedEvent(Guid.NewGuid(), 20, "Mild", DateTime.UtcNow, _correlationId));
            Func<Task> action = () => _handler.ConsumeAsync(context);
            await action.Should().NotThrowAsync();
            _notificationsService.Verify(e => e.WeatherAlertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()), Times.Never);
        }

        private ConsumeContext<WeatherForecastCreatedEvent> GenerateContext(WeatherForecastCreatedEvent @event)
        {
            var subscriptionContext = new SubscriptionContext("events", "test", @event.GetType().Name, _handler.GetType().Name, 0);
            return new ConsumeContext<WeatherForecastCreatedEvent>(@event, subscriptionContext, Mock.Of<IPublisher>());
        }
    }
}
