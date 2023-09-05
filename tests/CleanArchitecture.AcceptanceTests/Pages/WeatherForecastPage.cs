using CleanArchitecture.AcceptanceTests.Pages.Abstract;

namespace CleanArchitecture.AcceptanceTests.Pages
{
    public class WeatherForecastPage : PageObject
    {
        public readonly NavBar NavBar;

        public WeatherForecastPage(IPage page) : base(page)
        {
            NavBar = new NavBar(page);
        }

        public ILocator Title => Page.Locator("h1").GetByText("Weather forecast");
        public ILocator LocationSelector => Page.GetByLabel("Location");
        public ILocator GenerateButton => Page.GetByRole(AriaRole.Button, new() { Name = "Generate" });
        public ILocator Forecasts => Page.Locator("#forecasts");
        public ILocator ForecastRows => Forecasts.Locator("tbody").Locator("tr");
        public ILocator GeneratePrompt => Page.Locator("#generate-prompt");

        public async Task SelectLocation(string location)
        {
            await LocationSelector.SelectOptionAsync(new[] { location });
        }
    }
}
