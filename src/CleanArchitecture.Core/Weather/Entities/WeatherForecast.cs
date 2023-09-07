using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Weather.DomainEvents;
using CleanArchitecture.Core.Weather.ValueObjects;

namespace CleanArchitecture.Core.Weather.Entities
{
    public sealed class WeatherForecast : AggregateRoot
    {
        private WeatherForecast(DateTime date, Temperature temperature, string summary, Guid locationId)
        {
            Date = date;
            Temperature = temperature;
            Summary = summary;
            LocationId = locationId;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private WeatherForecast()
#pragma warning restore CS8618
        {

        }

        public static WeatherForecast Create(DateTime date, Temperature temperature, string? summary, Guid locationId)
        {
            // validation should go here before the aggregate is created
            // an aggregate should never be in an invalid state
            // the temperature is validated in the Temperature ValueObject and is always valid
            var forecast = new WeatherForecast(date, temperature, ValidateSummary(summary), locationId);
            forecast.PublishCreated();
            return forecast;
        }

        private void PublishCreated()
        {
            AddDomainEvent(new WeatherForecastCreatedDomainEvent(Id, Temperature.Celcius, Summary!, Date));
        }

        public DateTime Date { get; private set; }
        public Temperature Temperature { get; private set; }
        public string Summary { get; private set; }
        public Guid LocationId { get; private set; }

        public void UpdateDate(DateTime date)
        {
            Date = date;
        }

        public void Update(Temperature temperature, string summary)
        {
            Temperature = temperature;
            Summary = ValidateSummary(summary);
        }

        private static string ValidateSummary(string? summary)
        {
            summary = (summary ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(summary, nameof(Summary));
            return summary;
        }
    }
}
