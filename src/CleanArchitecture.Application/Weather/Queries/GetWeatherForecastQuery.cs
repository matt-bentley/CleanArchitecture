using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Weather.Models;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Weather.Entities;
using CleanArchitecture.Core.Abstractions.Guards;

namespace CleanArchitecture.Application.Weather.Queries
{
    public sealed record GetWeatherForecastQuery(Guid Id) : Query<WeatherForecastDto>;

    public sealed class GetWeatherForecastQueryHandler : QueryHandler<GetWeatherForecastQuery, WeatherForecastDto>
    {
        private readonly IRepository<WeatherForecast> _repository;

        public GetWeatherForecastQueryHandler(IMapper mapper,
            IRepository<WeatherForecast> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected override async Task<WeatherForecastDto> HandleAsync(GetWeatherForecastQuery request)
        {
            var forecast = await _repository.GetByIdAsync(request.Id);
            Guard.Against.NotFound(forecast);
            return Mapper.Map<WeatherForecastDto>(forecast);
        }
    }
}
