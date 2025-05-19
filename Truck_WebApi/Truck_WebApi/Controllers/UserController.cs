using Microsoft.AspNetCore.Mvc;
using Truck_BusnessLogic.Services;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Register new user 
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ServerResult>> CreateAsync(UserDto user)
        {
            var result = await _service.CreateAsync(user);
            return StatusCode(result.ResponseCode, result);
        }
    }
}
