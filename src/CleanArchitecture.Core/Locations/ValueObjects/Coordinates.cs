using CSharpFunctionalExtensions;
using CleanArchitecture.Core.Abstractions.Guards;

namespace CleanArchitecture.Core.Locations.ValueObjects
{
    public sealed class Coordinates : ValueObject
    {
        private const int MaxLatitude = 90;
        private const int MaxLongitude = 180;

        private Coordinates(decimal latitude, decimal longitude)
        {
            Latitude= latitude;
            Longitude= longitude;
        }

        public static Coordinates Create(decimal latitude, decimal longitude)
        {
            Guard.Against.ValueOutOfRange(latitude, -MaxLatitude, MaxLatitude, nameof(Latitude), "°");
            Guard.Against.ValueOutOfRange(longitude, -MaxLongitude, MaxLongitude, nameof(Longitude), "°");
            return new Coordinates(latitude, longitude);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Latitude; 
            yield return Longitude;
        }

        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
    }
}
