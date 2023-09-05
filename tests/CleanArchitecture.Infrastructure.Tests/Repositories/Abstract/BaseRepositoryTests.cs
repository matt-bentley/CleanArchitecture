using MediatR;
using Autofac;
using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Infrastructure.AutofacModules;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using CleanArchitecture.Core.Tests.Builders;
using CleanArchitecture.Core.Locations.Entities;

namespace CleanArchitecture.Infrastructure.Tests.Repositories.Abstract
{
    public abstract class BaseRepositoryTests : IAsyncLifetime
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected readonly WeatherContext Database;
        private readonly IContainer _container;
        protected readonly Location Location = new LocationBuilder().Build();

        public BaseRepositoryTests()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<WeatherContext>()
                    .UseSqlite(_connection)
                    .Options;

            var configuration = new ConfigurationBuilder().Build();
            var containerBuilder = new ContainerBuilder();

            var env = Mock.Of<IHostEnvironment>();
            containerBuilder.RegisterInstance(env);
            containerBuilder.RegisterInstance(Mock.Of<IMediator>());
            Database = new WeatherContext(options, env);
            Database.Database.EnsureCreated();

            containerBuilder.RegisterModule(new InfrastructureModule(options, configuration));
            _container = containerBuilder.Build();
        }

        public async Task InitializeAsync()
        {
            var locationsRepository = GetRepository<Location>();
            locationsRepository.Insert(Location);
            await GetUnitOfWork().CommitAsync();
        }

        public Task DisposeAsync()
        {
            Database.Dispose();
            _connection.Close();
            _connection.Dispose();
            return Task.CompletedTask;
        }

        protected IRepository<T> GetRepository<T>()
        where T : AggregateRoot
        {
            return _container.Resolve<IRepository<T>>();
        }

        protected IUnitOfWork GetUnitOfWork()
        {
            return _container.Resolve<IUnitOfWork>();
        }
    }
}
