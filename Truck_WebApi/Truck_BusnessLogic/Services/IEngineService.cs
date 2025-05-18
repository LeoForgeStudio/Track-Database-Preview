using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public interface IEngineService
    {
        Task<ServerResult<EngineDto>> CreateAsync(EngineDto dto);
        Task<ServerResult<EngineDto?>> GetByIdAsync(string id);
        Task<ServerResult<List<EngineDto>>> GetListAsync();
        Task<ServerResult<EngineDto>> UpdateAsync(string id, EngineDto item);
        Task<ServerResult<EngineDto>> DeleteAsync(string id);
        
    }
}