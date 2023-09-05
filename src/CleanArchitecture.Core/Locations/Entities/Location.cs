using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.ValueObjects;

namespace CleanArchitecture.Core.Locations.Entities
{
    public sealed class Location : AggregateRoot
    {
        private Location(string country, string city, Coordinates coordinates)
        {
            Country = country;
            City = city;
            Coordinates = coordinates;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private Location()
#pragma warning restore CS8618
        {

        }

        public static Location Create(string country, string city, Coordinates coordinates)
        {
            // validation should go here before the aggregate is created
            // an aggregate should never be in an invalid state
            // the coordinates are validated in the Coordinates ValueObject and is always valid
            country = (country ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(country, nameof(Country));
            city = (city ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(city, nameof(City));

            return new Location(country, city, coordinates);
        }

        public string Country { get; private set; }
        public string City { get; private set; }
        public Coordinates Coordinates { get; private set; }
    }
}
