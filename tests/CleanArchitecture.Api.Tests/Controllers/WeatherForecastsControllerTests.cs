using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Weather.Models;
using CleanArchitecture.Core.Tests.Builders;
using CleanArchitecture.Core.Weather.Entities;

namespace CleanArchitecture.Api.Tests.Controllers
{
    public class WeatherForecastsControllerTests
    {
        private const string BASE_URL = "api/weather-forecasts";
        private readonly TestWebApplication _application = new TestWebApplication();

        public WeatherForecastsControllerTests()
        {
            _application.TestWeatherForecasts.Add(new WeatherForecastBuilder().Build());
            _application.TestWeatherForecasts.Add(new WeatherForecastBuilder().Build());
            _application.TestWeatherForecasts.Add(new WeatherForecastBuilder().Build());
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenGet_ThenOk()
        {
            using var client = _application.CreateClient();
            var response = await client.GetAsync(BASE_URL);

            var forecast = await response.ReadAndAssertSuccessAsync<List<WeatherForecastDto>>();

            forecast.Should().HaveCount(_application.TestWeatherForecasts.Count);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenGetByLocation_ThenOk()
        {
            var locationId = Guid.NewGuid();
            _application.TestWeatherForecasts.Add(new WeatherForecastBuilder().WithLocation(locationId).Build());
            using var client = _application.CreateClient();
            var response = await client.GetAsync($"{BASE_URL}?locationid={locationId}");

            var forecast = await response.ReadAndAssertSuccessAsync<List<WeatherForecastDto>>();

            forecast.Should().HaveCount(1);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenGetById_ThenOk()
        {
            using var client = _application.CreateClient();
            var response = await client.GetAsync($"{BASE_URL}/{_application.TestWeatherForecasts.First().Id}");

            var forecast = await response.ReadAndAssertSuccessAsync<WeatherForecastDto>();

            forecast!.Id.Should().Be(_application.TestWeatherForecasts.First().Id);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenGetByMissingId_ThenNotFound()
        {
            using var client = _application.CreateClient();
            var id = Guid.NewGuid();
            var response = await client.GetAsync($"{BASE_URL}/{id}");

            var error = await response.ReadAndAssertError(HttpStatusCode.NotFound);
            error.Status.Should().Be(404);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenUpdate_ThenNoContent()
        {
            using var client = _application.CreateClient();
            var newDate = DateTime.UtcNow.AddDays(-1);
            var forecast = _application.TestWeatherForecasts.First();
            var response = await client.PutAsync($"{BASE_URL}/{forecast.Id}", _application.GetStringContent(new WeatherForecastUpdateDto() { Date = newDate }));

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            forecast.Date.Should().Be(newDate);
            _application.UnitOfWork.Verify(e => e.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenCreate_ThenOk()
        {
            using var client = _application.CreateClient();
            var summary = "Mild";
            var date = DateTime.UtcNow;
            var response = await client.PostAsync(BASE_URL, _application.GetStringContent(new WeatherForecastCreateDto() { Date = date, Summary = summary, TemperatureC = 20, LocationId = _application.TestLocations.First().Id }));

            var result = await response.ReadAndAssertSuccessAsync<CreatedResultEnvelope>();
            result!.Id.Should().NotBeEmpty();
            _application.UnitOfWork.Verify(e => e.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            _application.WeatherForecastsRepository.Verify(e => e.Insert(It.Is<WeatherForecast>(e => e.Summary == summary && e.Date == date)), Times.Once);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenCreateInvalid_ThenBadRequest()
        {
            using var client = _application.CreateClient();
            var response = await client.PostAsync(BASE_URL, _application.GetStringContent(new WeatherForecastCreateDto() { Summary = "", TemperatureC = 20, Date = DateTime.UtcNow, LocationId = _application.TestLocations.First().Id }));

            var error = await response.ReadAndAssertError(HttpStatusCode.BadRequest);
            error.ErrorMessage.Should().Be("Required input 'Summary' is missing.");
            _application.UnitOfWork.Verify(e => e.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenCreateInvalidLocation_ThenNotFound()
        {
            using var client = _application.CreateClient();
            var locationId = Guid.NewGuid();
            var response = await client.PostAsync(BASE_URL, _application.GetStringContent(new WeatherForecastCreateDto() { Summary = "", TemperatureC = 20, Date = DateTime.UtcNow, LocationId = locationId }));

            var error = await response.ReadAndAssertError(HttpStatusCode.NotFound);
            error.ErrorMessage.Should().Be($"Location not found: {locationId}");
            _application.UnitOfWork.Verify(e => e.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GivenWeatherForecastsController_WhenDelete_ThenNoContent()
        {
            using var client = _application.CreateClient();
            var id = _application.TestWeatherForecasts.First().Id;
            var response = await client.DeleteAsync($"{BASE_URL}/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _application.WeatherForecastsRepository.Verify(e => e.Delete(It.Is<WeatherForecast>(e => e.Id == id)), Times.Once);
            _application.UnitOfWork.Verify(e => e.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
