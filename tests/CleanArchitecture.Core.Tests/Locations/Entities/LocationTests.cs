using CleanArchitecture.Core.Abstractions.Exceptions;
using CleanArchitecture.Core.Tests.Builders;

namespace CleanArchitecture.Core.Tests.Locations.Entities
{
    public class LocationTests
    {
        [Fact]
        public void GivenLocation_WhenCreateValid_ThenCreate()
        {
            var location = new LocationBuilder().Build();
            location.City.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GivenLocation_WhenCreateEmptyCity_ThenError()
        {
            Action action = () => new LocationBuilder().WithCity("").Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'City' is missing.");
        }

        [Fact]
        public void GivenLocation_WhenLatitudeOver90_ThenError()
        {
            Action action = () => new LocationBuilder().WithLatitude(91).Build();
            action.Should().Throw<DomainException>().WithMessage("'Latitude' must be between -90° and 90°.");
        }

        [Fact]
        public void GivenLocation_WhenLatitudeUnder90_ThenError()
        {
            Action action = () => new LocationBuilder().WithLatitude(-91).Build();
            action.Should().Throw<DomainException>().WithMessage("'Latitude' must be between -90° and 90°.");
        }
    }
}
