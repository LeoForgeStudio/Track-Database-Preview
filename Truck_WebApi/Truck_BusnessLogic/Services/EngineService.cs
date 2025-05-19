using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public class EngineService : IEngineService
    {
        private readonly IRepository<Engine, EngineFilter> _engineRepository;

        public EngineService(IRepository<Engine, EngineFilter> engineRepository)
        {
            _engineRepository = engineRepository;
        }
        public async Task<ServerResult<EngineDto>> CreateAsync(EngineDto item)
        {
            if (string.IsNullOrWhiteSpace(item.Model))
            {
                return new ServerResult<EngineDto>
                {
                    Success = false,
                    Message = "Model value is required",
                    ResponseCode = 400,
                    Data = null
                };
            }

            var engines = await _engineRepository.GetListAsync();
            var exist = engines.FirstOrDefault(e => e.Model == item.Model);

            if (exist != null)
            {
                return new ServerResult<EngineDto>
                {
                    Success = false,
                    Message = "Engine already exists",
                    ResponseCode = 409,
                    Data = null
                };
            }

            var entity = Map(item);
            await _engineRepository.CreateAsync(entity);
            var createdDto = Map(entity);

            return new ServerResult<EngineDto>
            {
                Success = true,
                Message = "Engine created successfully",
                ResponseCode = 201,
                Data = createdDto
            };
        }


        public async Task<ServerResult<EngineDto?>> GetByIdAsync(string id)
        {
            var result = await _engineRepository.GetByIdAsync(id);
            if (result == null)
            {
                return new ServerResult<EngineDto?>
                {
                    Success = true,
                    Message = "Not found",
                    ResponseCode = 404

                };
            }
            var dto = Map(result);

            return new ServerResult<EngineDto?>
            {
                Success = true,
                ResponseCode = 200,
                Data = dto
            };
        }
        public async Task<ServerResult<List<EngineDto>>> GetListAsync()
        {
            var data = await _engineRepository.GetListAsync();
            var result = new ServerResult<List<EngineDto>>
            {
                Success = true,
                ResponseCode = 200,
                Message = "Items successfully found",
                Data = new List<EngineDto>()
            };

            if (data != null && data.Count > 0)
            {
                var dtoList = data.Select(item => Map(item)).ToList();
                result.Data = dtoList;
            }
            else
            {
                result.Success = false;
                result.ResponseCode = 404;
                result.Message = "No items found";
            }

            return result;
        }

        public async Task<ServerResult<EngineDto>> UpdateAsync(string id, EngineDto item)
        {
            try
            {
                var entity = await _engineRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new ServerResult<EngineDto>
                    {
                        Success = false,
                        Message = "Engine not found",
                        ResponseCode = 404,
                        Data = null
                    };
                }

                item.Id = entity.Id.ToString();
                item.Model = string.IsNullOrWhiteSpace(item.Model) ? entity.Model : item.Model;
                item.Cilinders = item.Cilinders == null ? entity.Cilinders : item.Cilinders;
                item.Power = item.Power == null ? entity.Power : item.Power;
                item.MaxTorque = item.MaxTorque == null ? entity.MaxTorque : item.MaxTorque;

                var updatedEntity = Map(item);
                await _engineRepository.UpdateAsync(updatedEntity);

                var updatedDto = Map(updatedEntity);
                return new ServerResult<EngineDto>
                {
                    Success = true,
                    Message = "Engine updated successfully",
                    ResponseCode = 200,
                    Data = updatedDto
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<EngineDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }

        public async Task<ServerResult<EngineDto>> DeleteAsync(string id)
        {
            try
            {
                var objectId = new ObjectId(id);
                await _engineRepository.DeleteAsync(objectId);

                return new ServerResult<EngineDto>
                {
                    Success = true,
                    Message = "Engine deleted successfully",
                    ResponseCode = 200,
                    Data = null
                };
            }
            catch (InvalidOperationException ex)
            {
                return new ServerResult<EngineDto>
                {
                    Success = false,
                    Message = ex.Message,
                    ResponseCode = 404,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<EngineDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }


        private EngineDto Map(Engine item)
        {
            return new EngineDto
            {
                Id = item.Id.ToString(),
                Model = item.Model,
                Cilinders = item.Cilinders,
                Power = item.Power,
                MaxTorque = item.MaxTorque,
            };
        }

        private Engine Map(EngineDto item)
        {
            return new Engine
            {
                Id = string.IsNullOrWhiteSpace(item.Id) ? ObjectId.Empty : new ObjectId(item.Id),
                Model = item.Model,
                Cilinders = item.Cilinders,
                Power = item.Power,
                MaxTorque = item.MaxTorque,
            };
        }
    }
}
