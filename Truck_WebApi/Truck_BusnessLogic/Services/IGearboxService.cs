using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public interface IGearboxService
    {
        Task<ServerResult<GearboxDto>> CreateAsync(GearboxDto dto);
        Task<ServerResult<GearboxDto?>> GetByIdAsync(string id);
        Task<ServerResult<List<GearboxDto>>> GetListAsync();
        Task<ServerResult<GearboxDto>> UpdateAsync(string id, GearboxDto item);
        Task<ServerResult<GearboxDto>> DeleteAsync(string id);
    }
}
