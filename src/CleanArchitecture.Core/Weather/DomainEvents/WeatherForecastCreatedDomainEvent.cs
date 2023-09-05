using CleanArchitecture.Core.Abstractions.DomainEvents;

namespace CleanArchitecture.Core.Weather.DomainEvents
{
    public sealed record WeatherForecastCreatedDomainEvent(Guid Id, int Temperature, string Summary, DateTime Date) : DomainEvent;
}
