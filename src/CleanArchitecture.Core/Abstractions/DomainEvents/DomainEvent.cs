using MediatR;

namespace CleanArchitecture.Core.Abstractions.DomainEvents
{
    public abstract record DomainEvent : INotification;
}
