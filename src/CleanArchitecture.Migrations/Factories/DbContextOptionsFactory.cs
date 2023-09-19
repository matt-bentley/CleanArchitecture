using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Migrations.Factories
{
    public static class DbContextOptionsFactory
    {
        public static DbContextOptions<WeatherContext> Create(IConfiguration configuration)
        {
            var appSettings = DatabaseSettings.Create(configuration);

            return new DbContextOptionsBuilder<WeatherContext>()
#if (UseSqlServer)
                .UseSqlServer(appSettings.SqlConnectionString, b => b.MigrationsAssembly("CleanArchitecture.Migrations"))
#else
                .UseNpgsql(appSettings.PostgresConnectionString, b => b.MigrationsAssembly("CleanArchitecture.Migrations"))
#endif
                .Options;
        }
    }
}
