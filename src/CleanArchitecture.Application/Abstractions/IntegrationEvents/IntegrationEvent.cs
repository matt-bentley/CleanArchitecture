
namespace CleanArchitecture.Application.Abstractions.IntegrationEvents
{
    public abstract record IntegrationEvent(string CorrelationId);
}
