using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public interface IUserService
    {
        Task<ServerResult> CreateAsync(UserDto newUser);
    }
}