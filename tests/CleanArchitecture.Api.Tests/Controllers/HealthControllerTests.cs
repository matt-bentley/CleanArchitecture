
namespace CleanArchitecture.Api.Tests.Controllers
{
    public class HealthControllerTests
    {
        private readonly TestWebApplication _application = new TestWebApplication();

        [Fact]
        public async Task GivenHealthEndpoint_WhenHealthy_ThenOk()
        {
            using var client = _application.CreateClient();
            var response = await client.GetAsync("healthz");
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.ServiceUnavailable);
        }

        [Fact]
        public async Task GivenLivenessEndpoint_WhenHealthy_ThenOk()
        {
            using var client = _application.CreateClient();
            var response = await client.GetAsync("liveness");

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
