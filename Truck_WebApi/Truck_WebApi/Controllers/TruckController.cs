using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Truck_BusnessLogic.Services;
using Truck_Shared.Dto;
using Truck_Shared.Dto.Filters;
using Truck_Shared.Entities;

namespace Truck_WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class TruckController : Controller
    {
        private readonly ITruckService _service;

        public TruckController(ITruckService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create new Truck entity entry
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ServerResult<TruckDto>>> CreateAsync(TruckDto item)
        {
            var result = await _service.CreateAsync(item);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Search for Truck entity by provided ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResult<TruckDto?>>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Get all Truck entity list
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<ServerResult<List<TruckDto>>>> GetListAsync()
        {
            var result = await _service.GetListAsync();
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Gets a filtered list of trucks based on provided TruckFilterDto.
        /// </summary>
        /// <param name="filter">Filter with search criteria</param>
        /// <returns>A list of GearboxDto wrapped in ServerResult</returns>
        [HttpPost("filter")]
        public async Task<ActionResult<ServerResult<List<TruckDto>>>> GetListAsync([FromForm] TruckFilterDto filter)
        {
            var result = await _service.GetListAsync(filter);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Update existing Truck entity entry
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServerResult<TruckDto>>> UpdateAsync(
            [FromRoute] string id,
            [FromBody] TruckDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Delete existing Truck entity entry
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResult<TruckDto>>> DeleteAsync(string id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.ResponseCode, result);
        }
    }
}
