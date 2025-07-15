using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAllocation.Application.DTOs;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Pagination;
using StockAllocation.Domain.Models;
using StockAllocation.Infrastructure.Data;

namespace StockAllocation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<LocationOutputDto>>> GetLocations([FromQuery] PaginationRequest paging)
        {
            var result = await _locationService.GetPagedLocationsAsync(paging);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationOutputDto>> GetLocation(Guid id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            return Ok(location);
        }

        [HttpPost]
        public async Task<ActionResult<LocationOutputDto>> CreateLocation(LocationInputDto locationDto)
        {
            var created = await _locationService.CreateLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocation), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(Guid id, LocationInputDto locationDto)
        {
            await _locationService.UpdateLocationAsync(id, locationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/lock")]
        public async Task<IActionResult> SetLockStatus(Guid id, [FromQuery] bool isLocked)
        {
            await _locationService.SetLockStatusAsync(id, isLocked);
            return NoContent();
        }
    }
}
