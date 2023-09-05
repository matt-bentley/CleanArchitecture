using CleanArchitecture.Core.Tests.Builders;
using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.AcceptanceTests.Hooks
{
    [Binding]
    public class WeatherForecastHooks
    {
        [BeforeFeature("weather_cleanup")]
        public static async Task CleanupWeatherForecasts(WeatherContext context)
        {
            var forecasts = await context.WeatherForecasts.ToListAsync();
            context.RemoveRange(forecasts);
            await context.SaveChangesAsync();
            var location = await context.Locations.FirstOrDefaultAsync(e => e.City == "New York");
            var forecast = new WeatherForecastBuilder().WithLocation(location.Id).Build();
            context.Add(forecast);
            await context.SaveChangesAsync();
        }
    }
}
