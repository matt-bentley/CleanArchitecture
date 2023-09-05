using CleanArchitecture.AcceptanceTests.Pages;
using CleanArchitecture.AcceptanceTests.Steps.Abstract;

namespace CleanArchitecture.AcceptanceTests.Steps
{
    public class WeatherForecastSteps : BaseSteps
    {
        private WeatherForecastPage _page;

        public WeatherForecastSteps(TestHarness testHarness) : base(testHarness)
        {

        }

        [Given(@"a user is on the Weather Forecast page")]
        public async Task GivenUserOnHomePage()
        {
            _page = new WeatherForecastPage(await TestHarness.GotoAsync("/weather-forecast"));
            TestHarness.CurrentPage = _page;
        }

        [When(@"'(.*)' location is selected")]
        public async Task WhenSelectLocation(string location)
        {
            await _page.SelectLocation(location);
        }

        [When(@"a weather forecast is generated")]
        public async Task WhenWeatherForecastGenerated()
        {
            await _page.GenerateButton.ClickAsync();
        }

        [Then(@"Weather Forecast page is open")]
        public async Task ThenWeatherForecastOpen()
        {
            _page = TestHarness.CurrentPage as WeatherForecastPage;
            var isVisiable = await _page.Title.IsVisibleAsync();
            isVisiable.Should().BeTrue();
        }

        [Then(@"'(.*)' weather forecasts present")]
        public async Task ThenWeatherForecastsPresent(int count)
        {
            if(count == 0)
            {
                var isVisible = await _page.Forecasts.IsVisibleAsync();
                isVisible.Should().BeFalse();
            }
            else
            {
                var hasCount = await _page.WaitForConditionAsync(async () =>
                {
                    var actualCount = await _page.ForecastRows.CountAsync();
                    return actualCount == count;
                });
                hasCount.Should().BeTrue();
            }
        }

        [Then(@"Generate prompt is visible")]
        public async Task ThenGeneratePromptVisible()
        {
            var isVisiable = await _page.GeneratePrompt.IsVisibleAsync();
            isVisiable.Should().BeTrue();
        }
    }
}
