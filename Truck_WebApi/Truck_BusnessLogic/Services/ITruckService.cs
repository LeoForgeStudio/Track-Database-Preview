using Truck_DataAccess.Entities;
using Truck_Shared.Dto;
using Truck_Shared.Dto.Filters;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public interface ITruckService
    {
        Task<ServerResult<TruckDto>> CreateAsync(TruckDto dto);
        Task<ServerResult<TruckDto>> GetByIdAsync(string id);
        Task<ServerResult<List<TruckDto?>>> GetListAsync();
        Task<ServerResult<List<TruckDto?>>> GetListAsync(TruckFilterDto item);
        Task<ServerResult<TruckDto>> UpdateAsync(string id, TruckDto dto);
        Task<ServerResult<TruckDto>> DeleteAsync(string id);


    }
}