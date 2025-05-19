using Microsoft.AspNetCore.Mvc;
using Truck_BusnessLogic.Services;
using Truck_Shared.Dto;
using Truck_Shared.Dto.Filters;
using Truck_Shared.Entities;

namespace Truck_WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EngineController : Controller
    {
        private readonly IEngineService _service;

        public EngineController(IEngineService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create new engine entity
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ServerResult<EngineDto>>> CreateAsync(EngineDto item)
        {
            var result = await _service.CreateAsync(item);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Get engine entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResult<EngineDto?>>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Get Get list of engines 
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<ServerResult<List<EngineDto>>>> GetListAsync()
        {
            var result = await _service.GetListAsync();
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Gets a filtered list of Engine based on provided EngineFilterDto.
        /// </summary>
        /// <param name="filter">Filter with search criteria</param>
        /// <returns>A list of GearboxDto wrapped in ServerResult</returns>
        [HttpPost("filter")]
        public async Task<ActionResult<ServerResult<List<EngineDto>>>> GetListAsync([FromForm] EngineFilterDto filter)
        {
            var result = await _service.GetListAsync(filter);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Update engine entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServerResult<EngineDto>>> UpdateAsync(
            [FromRoute] string id,
            [FromBody] EngineDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Delete engine entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResult<EngineDto>>> DeleteAsync(string id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.ResponseCode, result);
        }
    }
}
