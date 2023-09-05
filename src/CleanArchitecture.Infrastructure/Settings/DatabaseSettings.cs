using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Settings
{
    public sealed class DatabaseSettings
    {
        public static DatabaseSettings Create(IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings();
            configuration.GetSection("Database").Bind(databaseSettings);
            return databaseSettings;
        }

        public string? ConnectionString { get; set; }
    }
}
