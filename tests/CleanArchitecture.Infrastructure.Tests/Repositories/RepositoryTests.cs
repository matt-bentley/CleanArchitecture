using CleanArchitecture.Core.Tests.Builders;
using CleanArchitecture.Core.Weather.Entities;
using CleanArchitecture.Infrastructure.Tests.Repositories.Abstract;

namespace CleanArchitecture.Infrastructure.Tests.Repositories
{
    public class RepositoryTests : BaseRepositoryTests
    {
        [Fact]
        public async Task GivenRepository_WhenInsert_ThenInserted()
        {
            var repository = GetRepository<WeatherForecast>();
            var entity = new WeatherForecastBuilder().WithLocation(Location.Id).Build();

            repository.Insert(entity);
            await GetUnitOfWork().CommitAsync();

            var inserted = await repository.GetByIdAsync(entity.Id);
            inserted.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenRepository_WhenInsertMultiple_ThenInserted()
        {
            var repository = GetRepository<WeatherForecast>();

            var entities = new List<WeatherForecast>()
            {
                new WeatherForecastBuilder().WithLocation(Location.Id).Build(),
                new WeatherForecastBuilder().WithLocation(Location.Id).Build()
            };

            repository.Insert(entities);
            await GetUnitOfWork().CommitAsync();

            var inserted = await repository.GetByIdAsync(entities.Last().Id);
            inserted.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenRepository_WhenUpdate_ThenUpdated()
        {
            var repository = GetRepository<WeatherForecast>();
            var date = DateTime.UtcNow;
            var entity = new WeatherForecastBuilder().WithLocation(Location.Id).Build();

            repository.Insert(entity);
            await GetUnitOfWork().CommitAsync();

            var inserted = await repository.GetByIdAsync(entity.Id);
            inserted?.UpdateDate(date);

            await GetUnitOfWork().CommitAsync();

            var updated = await repository.GetByIdAsync(entity.Id);
            updated!.Id.Should().Be(entity.Id);
            updated.Date.Should().Be(date);
        }

        [Fact]
        public async Task GivenRepository_WhenDelete_ThenDeleted()
        {
            var repository = GetRepository<WeatherForecast>();
            var entity = new WeatherForecastBuilder().WithLocation(Location.Id).Build();

            repository.Insert(entity);
            var id = entity.Id;
            await GetUnitOfWork().CommitAsync();

            repository.Delete(entity);
            await GetUnitOfWork().CommitAsync();
            var inserted = await repository.GetByIdAsync(id);
            inserted.Should().BeNull();
        }

        [Fact]
        public async Task GivenRepository_WhenRemove_ThenRemoved()
        {
            var repository = GetRepository<WeatherForecast>();
            var entity = new WeatherForecastBuilder().WithLocation(Location.Id).Build();
            repository.Insert(entity);
            var id = entity.Id;
            await GetUnitOfWork().CommitAsync();

            repository.Remove(new List<WeatherForecast>() { entity });
            await GetUnitOfWork().CommitAsync();
            var inserted = await repository.GetByIdAsync(id);
            inserted.Should().BeNull();
        }

        [Fact]
        public async Task GivenRepository_WhenGetAll_ThenGetAll()
        {
            var repository = GetRepository<WeatherForecast>();
            var entity1 = new WeatherForecastBuilder().WithLocation(Location.Id).Build();
            var entity2 = new WeatherForecastBuilder().WithLocation(Location.Id).Build();
            repository.Insert(entity1);
            repository.Insert(entity2);
            await GetUnitOfWork().CommitAsync();

            var inserted = repository.GetAll();
            inserted.ToList().Count.Should().Be(2);
        }

        [Fact]
        public async Task GivenRepository_WhenGetAllTracked_ThenChangesCommitted()
        {
            var repository = GetRepository<WeatherForecast>();
            var entity1 = new WeatherForecastBuilder().WithLocation(Location.Id).Build();
            var entity2 = new WeatherForecastBuilder().WithLocation(Location.Id).Build();
            var date = DateTime.UtcNow;

            repository.Insert(entity1);
            var id1 = entity1.Id;
            repository.Insert(entity2);
            await GetUnitOfWork().CommitAsync();

            var inserted = repository.GetAll(false).Where(u => u.Id == id1).FirstOrDefault();
            inserted?.UpdateDate(date);
            await GetUnitOfWork().CommitAsync();

            var updated = await repository.GetByIdAsync(id1);
            updated!.Date.Should().Be(date);
        }
    }
}
