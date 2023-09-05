using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Locations.Models;
using CleanArchitecture.Core.Locations.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Locations.Queries
{
    public sealed record GetLocationsQuery() : Query<List<LocationDto>>;

    public sealed class GetLocationsQueryHandler : QueryHandler<GetLocationsQuery, List<LocationDto>>
    {
        private readonly IRepository<Location> _repository;

        public GetLocationsQueryHandler(IMapper mapper,
            IRepository<Location> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected override async Task<List<LocationDto>> HandleAsync(GetLocationsQuery request)
        {
            var locations = await _repository.GetAll()
                                                 .OrderBy(e => e.City)
                                                 .ToListAsync();
            return Mapper.Map<List<LocationDto>>(locations);
        }
    }
}
