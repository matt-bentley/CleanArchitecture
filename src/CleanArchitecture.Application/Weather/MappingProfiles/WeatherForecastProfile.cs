using AutoMapper;
using CleanArchitecture.Application.Weather.Models;
using CleanArchitecture.Core.Weather.Entities;

namespace CleanArchitecture.Application.Weather.MappingProfiles
{
    public sealed class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastDto>()
                .ForMember(dest => dest.TemperatureF,
                            e => e.MapFrom(src => src.Temperature.Farenheit))
                .ForMember(dest => dest.TemperatureC,
                            e => e.MapFrom(src => src.Temperature.Celcius))
                .ForMember(dest => dest.Current,
                            e => e.MapFrom(src => src.Date.Day == DateTime.UtcNow.Day
                                    && src.Date.Month == DateTime.UtcNow.Month
                                    && src.Date.Year == DateTime.UtcNow.Year));
        }
    }
}
