using CleanArchitecture.AcceptanceTests.Pages.Abstract;

namespace CleanArchitecture.AcceptanceTests.Pages
{
    public class NavBar : PageObject
    {
        public NavBar(IPage page) : base(page)
        {
        }

        public ILocator Header => Page.Locator("app-nav-menu");
        public ILocator Home => Header.GetByText("Home");
        public ILocator WeatherForecast => Header.GetByText("Weather Forecast");

        public async Task<WeatherForecastPage> OpenWeatherForecast()
        {
            await WeatherForecast.ClickAsync();
            return new WeatherForecastPage(Page);
        }
    }
}
