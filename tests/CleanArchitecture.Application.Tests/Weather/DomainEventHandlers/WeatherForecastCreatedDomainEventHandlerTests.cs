using CleanArchitecture.Application.Weather.DomainEventHandlers;
using CleanArchitecture.Core.Weather.DomainEvents;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Core.Abstractions.Services;

namespace CleanArchitecture.Application.Tests.Weather.DomainEventHandlers
{
    public class WeatherForecastCreatedDomainEventHandlerTests
    {
        private readonly WeatherForecastCreatedDomainEventHandler _handler;
        private readonly Mock<INotificationsService> _notificationsService = new Mock<INotificationsService>();

        public WeatherForecastCreatedDomainEventHandlerTests()
        {
            _handler = new WeatherForecastCreatedDomainEventHandler(Mock.Of<ILogger<WeatherForecastCreatedDomainEventHandler>>(), _notificationsService.Object);
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandleHotTemperature_ThenSendAlert()
        {
            Func<Task> action = () => _handler.Handle(new WeatherForecastCreatedDomainEvent(Guid.NewGuid(), 50, "Hot", DateTime.UtcNow), default(CancellationToken));
            await action.Should().NotThrowAsync();
            _notificationsService.Verify(e => e.WeatherAlertAsync("Hot", 50, It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandleColdTemperature_ThenSendAlert()
        {
            Func<Task> action = () => _handler.Handle(new WeatherForecastCreatedDomainEvent(Guid.NewGuid(), -1, "Cold", DateTime.UtcNow), default(CancellationToken));
            await action.Should().NotThrowAsync();
            _notificationsService.Verify(e => e.WeatherAlertAsync("Cold", -1, It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GivenWeatherForecastCreatedDomainEvent_WhenHandleNormalTemperature_ThenDontSendAlert()
        {
            Func<Task> action = () => _handler.Handle(new WeatherForecastCreatedDomainEvent(Guid.NewGuid(), 20, "Mild", DateTime.UtcNow), default(CancellationToken));
            await action.Should().NotThrowAsync();
            _notificationsService.Verify(e => e.WeatherAlertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()), Times.Never);
        }
    }
}
