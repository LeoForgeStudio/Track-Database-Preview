using Microsoft.AspNetCore.Mvc;
using Truck_BusnessLogic.Services;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GearboxController : Controller
    {
        private readonly IGearboxService _service;

        public GearboxController(IGearboxService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create new Gearbox entity
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ServerResult<GearboxDto>>> CreateAsync(GearboxDto item)
        {
            var result = await _service.CreateAsync(item);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Get gearbox entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResult<GearboxDto?>>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Get list of gearbox entities
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<ServerResult<List<GearboxDto>>>> GetListAsync()
        {
            var result = await _service.GetListAsync();
            return StatusCode(result.ResponseCode, result);
        }

        

        /// <summary>
        /// Update gearbox entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServerResult<GearboxDto>>> UpdateAsync(
            [FromRoute] string id,
            [FromBody] GearboxDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Delete gearbox  entity by  ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResult<GearboxDto>>> DeleteAsync(string id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.ResponseCode, result);
        }
    }
}
