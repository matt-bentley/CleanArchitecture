using CleanArchitecture.Core.Locations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(e => e.Country)
                   .HasColumnType("varchar(64)")
                   .IsRequired();

            builder.Property(e => e.City)
                   .HasColumnType("varchar(64)")
                   .IsRequired();


            builder.OwnsOne(e => e.Coordinates, coordinatesBuilder =>
            {
                coordinatesBuilder.Property(e => e.Latitude)
                                  .HasColumnName("Latitude")
                                  .IsRequired();

                coordinatesBuilder.Property(e => e.Longitude)
                                  .HasColumnName("Longitude")
                                  .IsRequired();
            });
        }
    }
}
