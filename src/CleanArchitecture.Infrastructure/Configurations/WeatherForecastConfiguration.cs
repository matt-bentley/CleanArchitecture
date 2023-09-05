using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Weather.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.Property(e => e.Summary)
                   .HasColumnType("varchar(64)")
                   .IsRequired();

            builder.OwnsOne(e => e.Temperature, tempBuilder =>
            {
                tempBuilder.Property(e => e.Celcius)
                .HasColumnName("Temperature")
                .IsRequired();
            });

            builder.HasOne<Location>()
                   .WithMany()
                   .HasForeignKey(e => e.LocationId)
                   .IsRequired();
        }
    }
}
