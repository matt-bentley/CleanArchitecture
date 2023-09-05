using CleanArchitecture.AcceptanceTests.Pages;
using CleanArchitecture.AcceptanceTests.Steps.Abstract;

namespace CleanArchitecture.AcceptanceTests.Steps
{
    public class HomeSteps : BaseSteps
    {
        private HomePage _page;

        public HomeSteps(TestHarness testHarness) : base(testHarness)
        {

        }

        [Given(@"a user is on the Home page")]
        public async Task GivenUserOnHomePage()
        {
            _page = new HomePage(await TestHarness.GotoAsync("/"));
            TestHarness.CurrentPage = _page;
        }

        [When(@"Weather Forecast page is opened")]
        public async Task WhenWeatherForecastOpened()
        {
            TestHarness.CurrentPage = await _page.NavBar.OpenWeatherForecast();
        }
    }
}
