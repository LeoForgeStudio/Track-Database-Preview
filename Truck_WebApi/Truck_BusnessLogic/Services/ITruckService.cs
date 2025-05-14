using Truck_DataAccess.Entities;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public interface ITruckService
    {
        Task<ServerResult<TruckDto>> GetByIdAsync(string id);
        Task<ServerResult<List<TruckDto?>>> GetListAsync();
        
    }
}