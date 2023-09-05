using CleanArchitecture.Application.Locations.Models;

namespace CleanArchitecture.Api.Tests.Controllers
{
    public class LocationsControllerTests
    {
        private const string BASE_URL = "api/locations";
        private readonly TestWebApplication _application = new TestWebApplication();

        [Fact]
        public async Task GivenLocationsController_WhenGet_ThenOk()
        {
            using var client = _application.CreateClient();
            var response = await client.GetAsync(BASE_URL);

            var locations = await response.ReadAndAssertSuccessAsync<List<LocationDto>>();

            locations.Should().HaveCount(_application.TestLocations.Count);
        }
    }
}
