using AutoMapper;
using StockAllocation.Application.DTOs;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Pagination;
using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<LocationOutputDto>> GetPagedLocationsAsync(PaginationRequest paging)
        {
            var totalCount = await _locationRepository.GetTotalLocationsCountAsync();
            var locations = await _locationRepository.GetPagedLocationsAsync(paging.PageNumber, paging.PageSize);
            var items = _mapper.Map<List<LocationOutputDto>>(locations);

            return new PaginationResponse<LocationOutputDto>(items, totalCount, paging.PageNumber, paging.PageSize);
        }

        public async Task<LocationOutputDto> GetLocationByIdAsync(Guid id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
                throw new KeyNotFoundException("Location not found");

            return _mapper.Map<LocationOutputDto>(location);
        }

        public async Task<LocationOutputDto> CreateLocationAsync(LocationInputDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            var created = await _locationRepository.AddAsync(location);
            return _mapper.Map<LocationOutputDto>(created);
        }

        public async Task UpdateLocationAsync(Guid id, LocationInputDto locationDto)
        {
            var existing = await _locationRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Location not found");

            existing.IsLocked = locationDto.IsLocked;
            await _locationRepository.UpdateAsync(existing);
        }

        public async Task DeleteLocationAsync(Guid id)
        {
            await _locationRepository.DeleteAsync(id);
        }

        public async Task SetLockStatusAsync(Guid id, bool isLocked)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
                throw new KeyNotFoundException("Location not found");

            location.IsLocked = isLocked;
            await _locationRepository.UpdateAsync(location);
        }
    }
}
