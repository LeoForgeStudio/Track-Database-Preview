<<<<<<< Updated upstream
﻿using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public interface IManufacturerService
    {
        Task<ServerResult<ManufacturerDto>> CreateAsync(string name);
        Task<ServerResult<ManufacturerDto?>> GetByIdAsync(string id);
        Task<ServerResult<List<ManufacturerDto>>> GetListAsync();
        Task<ServerResult<ManufacturerDto>> UpdateAsync(string id, string name);
        Task<ServerResult<ManufacturerDto>> DeleteAsync(string id);

=======
﻿namespace Truck_BusnessLogic.Services
{
    public interface IManufacturerService
    {
>>>>>>> Stashed changes
    }
}