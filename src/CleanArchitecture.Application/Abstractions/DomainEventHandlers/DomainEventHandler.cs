using MediatR;
using CleanArchitecture.Core.Abstractions.DomainEvents;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.DomainEventHandlers
{
    public abstract class DomainEventHandler<T> : INotificationHandler<T> where T : DomainEvent
    {
        protected readonly ILogger<DomainEventHandler<T>> Logger;

        protected DomainEventHandler(ILogger<DomainEventHandler<T>> logger)
        {
            Logger = logger;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Processing domain event: {type}", this.GetType().Name);
            await OnHandleAsync(notification);
            Logger.LogInformation("Completed processing domain event: {type}", this.GetType().Name);
        }

        protected abstract Task OnHandleAsync(T @event);
    }
}
