using Microsoft.AspNetCore.Mvc;
using System.Text;
using Truck_DataAccess.Repositories;
using Truck_Shared.Helpers;
using Truck_Shared.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Truck_Shared.Dto;

namespace Truck_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Login test
        /// </summary>
        /// <param name="id"></param>>
        /// <returns>
        /// CustomerDto if entity is found
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.Username);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var hash = PasswordHasher.GenerateHash(request.Password, user.PasswordSalt);
            if (!hash.SequenceEqual(user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{request.Username}:{request.Password}"));
            return Ok(new { Token = token });
        }
    }
}
