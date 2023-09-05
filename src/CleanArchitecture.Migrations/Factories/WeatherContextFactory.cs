using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Migrations.Factories
{
    public class WeatherContextFactory : IDesignTimeDbContextFactory<WeatherContext>
    {
        private IConfiguration _configuration;

        public WeatherContextFactory()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json")
                   .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public WeatherContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WeatherContext CreateDbContext(string[] args)
        {
            return new WeatherContext(DbContextOptionsFactory.Create(_configuration), null);
        }
    }
}
