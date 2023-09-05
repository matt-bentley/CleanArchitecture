using CleanArchitecture.Application.Abstractions.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Commands
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected CommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }

    public abstract class CommandHandler<TCommand> : CommandHandler, IRequestHandler<TCommand, Unit> where TCommand : Command
    {
        protected CommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await HandleAsync(request);
            return Unit.Value;
        }

        protected abstract Task HandleAsync(TCommand request);
    }

    public abstract class CreateCommandHandler<TCommand> : CommandHandler, IRequestHandler<TCommand, Guid> where TCommand : CreateCommand
    {
        protected CreateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Guid> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await HandleAsync(request);
        }

        protected abstract Task<Guid> HandleAsync(TCommand request);
    }
}
