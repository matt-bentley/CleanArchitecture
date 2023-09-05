using MediatR;

namespace CleanArchitecture.Application.Abstractions.Queries
{
    public abstract record Query<T> : IRequest<T>;
}
