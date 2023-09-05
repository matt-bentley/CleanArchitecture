using AutoMapper;
using CleanArchitecture.Application.Locations.Models;
using CleanArchitecture.Core.Locations.Entities;

namespace CleanArchitecture.Application.Locations.MappingProfiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {

            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.Latitude,
                            e => e.MapFrom(src => src.Coordinates.Latitude))
                .ForMember(dest => dest.Longitude,
                            e => e.MapFrom(src => src.Coordinates.Longitude));
        }
    }
}
