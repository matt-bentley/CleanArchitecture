
namespace CleanArchitecture.Application.Weather.Models
{
    public sealed class WeatherForecastCreateDto
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public Guid LocationId { get; set; }
    }
}
