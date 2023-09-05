
namespace CleanArchitecture.Application.Locations.Models
{
    public sealed class LocationDto
    {
        public Guid Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
