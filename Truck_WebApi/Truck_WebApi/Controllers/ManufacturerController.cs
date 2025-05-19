using Microsoft.AspNetCore.Mvc;
using Truck_BusnessLogic.Services;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _service;

        public ManufacturerController(IManufacturerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create new manufaturer entry
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ServerResult<ManufacturerDto>>> CreateAsync(string item)
        {
            var result = await _service.CreateAsync(item);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Search for Manufacturer entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResult<ManufacturerDto?>>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Get list of manufacturers
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<ServerResult<List<ManufacturerDto>>>> GetListAsync()
        {
            var result = await _service.GetListAsync();
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Update manufacturer entity By ID 
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServerResult<ManufacturerDto>>> UpdateAsync(
            [FromRoute] string id,
            [FromBody] string name)
        {
            var result = await _service.UpdateAsync(id, name);
            return StatusCode(result.ResponseCode, result);
        }

        /// <summary>
        /// Delete manufacturer entity by ID
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResult<ManufacturerDto>>> DeleteAsync(string id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.ResponseCode, result);
        }
    }
}
