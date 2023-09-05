
namespace CleanArchitecture.Application.Weather.Models
{
    public sealed class WeatherForecastDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string? Summary { get; set; }
        public bool Current { get; set; }
        public Guid LocationId { get; set; }
    }
}
