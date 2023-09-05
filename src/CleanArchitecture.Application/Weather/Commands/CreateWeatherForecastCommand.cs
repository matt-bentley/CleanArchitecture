using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Weather.Entities;
using CleanArchitecture.Core.Weather.ValueObjects;

namespace CleanArchitecture.Application.Weather.Commands
{
    public sealed record CreateWeatherForecastCommand(int Temperature, DateTime Date, string? Summary, Guid LocationId) : CreateCommand;

    public sealed class CreateWeatherForecastCommandHandler : CreateCommandHandler<CreateWeatherForecastCommand>
    {
        private readonly IRepository<WeatherForecast> _repository;
        private readonly IRepository<Location> _locationsRepository;

        public CreateWeatherForecastCommandHandler(IRepository<WeatherForecast> repository,
            IRepository<Location> locationsRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _locationsRepository = locationsRepository;
        }

        protected override async Task<Guid> HandleAsync(CreateWeatherForecastCommand request)
        {
            var location = await _locationsRepository.GetByIdAsync(request.LocationId);
            location = Guard.Against.NotFound(location, $"Location not found: {request.LocationId}");

            var created = WeatherForecast.Create(request.Date,
                                                 Temperature.FromCelcius(request.Temperature),
                                                 request.Summary,
                                                 location.Id);
            _repository.Insert(created);
            await UnitOfWork.CommitAsync();
            return created.Id;
        }
    }
}
