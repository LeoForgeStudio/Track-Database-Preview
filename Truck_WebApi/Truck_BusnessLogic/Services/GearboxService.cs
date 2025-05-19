using MongoDB.Bson;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public class GearboxService : IGearboxService
    {
        private readonly IRepository<Gearbox, GearboxFilter> _gearboxRepository;

        public GearboxService(IRepository<Gearbox, GearboxFilter> gearboxRepository)
        {
            _gearboxRepository = gearboxRepository;
        }

        public async Task<ServerResult<GearboxDto>> CreateAsync(GearboxDto item)
        {
            if (string.IsNullOrWhiteSpace(item.Model))
            {
                return new ServerResult<GearboxDto>
                {
                    Success = false,
                    Message = "Model value is required",
                    ResponseCode = 400,
                    Data = null
                };
            }

            var items = await _gearboxRepository.GetListAsync();
            var exist = items.FirstOrDefault(g => g.Model == item.Model);

            if (exist != null)
            {
                return new ServerResult<GearboxDto>
                {
                    Success = false,
                    Message = "Gearbox already exists",
                    ResponseCode = 409,
                    Data = null
                };
            }

            var entity = Map(item);
            await _gearboxRepository.CreateAsync(entity);
            var createdDto = Map(entity);

            return new ServerResult<GearboxDto>
            {
                Success = true,
                Message = "Gearbox created successfully",
                ResponseCode = 201,
                Data = createdDto
            };
        }


        public async Task<ServerResult<GearboxDto?>> GetByIdAsync(string id)
        {
            var entity = await _gearboxRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServerResult<GearboxDto?>
                {
                    Success = true,
                    Message = "Not found",
                    ResponseCode = 404,
                    Data = null
                };
            }

            var dto = Map(entity);
            return new ServerResult<GearboxDto?>
            {
                Success = true,
                ResponseCode = 200,
                Data = dto
            };
        }

        public async Task<ServerResult<List<GearboxDto>>> GetListAsync()
        {
            var data = await _gearboxRepository.GetListAsync();
            var result = new ServerResult<List<GearboxDto>>
            {
                Success = true,
                ResponseCode = 200,
                Message = "Items successfully found",
                Data = new List<GearboxDto>()
            };

            if (data != null && data.Count > 0)
            {
                result.Data = data.Select(Map).ToList();
            }
            else
            {
                result.Success = false;
                result.ResponseCode = 404;
                result.Message = "No items found";
            }

            return result;
        }

        public async Task<ServerResult<GearboxDto>> UpdateAsync(string id, GearboxDto item)
        {
            try
            {
                var entity = await _gearboxRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new ServerResult<GearboxDto>
                    {
                        Success = false,
                        Message = "Gearbox not found",
                        ResponseCode = 404,
                        Data = null
                    };
                }

                item.Id = entity.Id.ToString();
                item.Model = string.IsNullOrWhiteSpace(item.Model) ? entity.Model : item.Model;
                item.Ratio = string.IsNullOrWhiteSpace(item.Ratio) ? entity.Ratio : item.Ratio;
                item.MaxTorque = item.MaxTorque == 0 ? entity.MaxTorque : item.MaxTorque;
                item.MaxSpeed = item.MaxSpeed == 0 ? entity.MaxSpeed : item.MaxSpeed;
                item.MaxTemp = item.MaxTemp == 0 ? entity.MaxTemp : item.MaxTemp;

                var updatedEntity = Map(item);
                await _gearboxRepository.UpdateAsync(updatedEntity);
                var updatedDto = Map(updatedEntity);

                return new ServerResult<GearboxDto>
                {
                    Success = true,
                    Message = "Gearbox updated successfully",
                    ResponseCode = 200,
                    Data = updatedDto
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<GearboxDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }

        public async Task<ServerResult<GearboxDto>> DeleteAsync(string id)
        {
            try
            {
                var objectId = new ObjectId(id);
                await _gearboxRepository.DeleteAsync(objectId);

                return new ServerResult<GearboxDto>
                {
                    Success = true,
                    Message = "Gearbox deleted successfully",
                    ResponseCode = 200,
                    Data = null
                };
            }
            catch (InvalidOperationException ex)
            {
                return new ServerResult<GearboxDto>
                {
                    Success = false,
                    Message = ex.Message,
                    ResponseCode = 404,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<GearboxDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }

        private GearboxDto Map(Gearbox item)
        {
            return new GearboxDto
            {
                Id = item.Id.ToString(),
                Model = item.Model,
                Ratio = item.Ratio,
                MaxTorque = item.MaxTorque,
                MaxSpeed = item.MaxSpeed,
                MaxTemp = item.MaxTemp
            };
        }

        private Gearbox Map(GearboxDto item)
        {
            return new Gearbox
            {
                Id = string.IsNullOrWhiteSpace(item.Id) ? ObjectId.Empty : new ObjectId(item.Id),
                Model = item.Model,
                Ratio = item.Ratio,
                MaxTorque = item.MaxTorque,
                MaxSpeed = item.MaxSpeed,
                MaxTemp = item.MaxTemp
            };
        }
    }
}
