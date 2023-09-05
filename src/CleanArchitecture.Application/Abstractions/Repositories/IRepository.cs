using CleanArchitecture.Core.Abstractions.Entities;

namespace CleanArchitecture.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        IQueryable<T> GetAll(bool noTracking = true);
        Task<T?> GetByIdAsync(Guid id);
        void Insert(T entity);
        void Insert(List<T> entities);
        void Delete(T entity);
        void Remove(IEnumerable<T> entitiesToRemove);
    }
}
