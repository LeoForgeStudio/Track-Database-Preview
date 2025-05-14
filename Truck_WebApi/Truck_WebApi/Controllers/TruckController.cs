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

        [HttpGet]
        public async Task<ActionResult<ServerResult<List<TruckDto>>>> GetListAsync()
        {
            var result = await _service.GetListAsync();
            return StatusCode(result.ResponseCode, result);
        }
    }
}
