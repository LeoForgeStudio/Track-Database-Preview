using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Truck_BusnessLogic.Services;
using Truck_Shared.Dto;
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

        [HttpGet("{id}")]

        public async Task<ActionResult<ServerResult<TruckDto?>>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.ResponseCode, result);
        }

        [HttpGet]
        public async Task<ActionResult<ServerResult<List<TruckDto>>>> GetListAsync()
        {
            var result = await _service.GetListAsync();
            return StatusCode(result.ResponseCode, result);
        }
    }
}
