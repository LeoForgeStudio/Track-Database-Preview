using MongoDB.Bson;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Entities;

namespace Truck_BusnessLogic.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer, ManufacturerFilter> _manufacturerRepository;

        public ManufacturerService(IRepository<Manufacturer, ManufacturerFilter> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<ServerResult<ManufacturerDto>> CreateAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ServerResult<ManufacturerDto>
                {
                    Success = false,
                    Message = "Name is required",
                    ResponseCode = 400,
                    Data = null
                };
            }

            var items = await _manufacturerRepository.GetListAsync();
            var exist = items.FirstOrDefault(m => m.Name == name);

            if (exist != null)
            {
                return new ServerResult<ManufacturerDto>
                {
                    Success = false,
                    Message = "Manufacturer already exists",
                    ResponseCode = 409,
                    Data = null
                };
            }

            var entity = new Manufacturer
            {
                Name = name,
                Model = null
            };

            await _manufacturerRepository.CreateAsync(entity);

            var createdDto = new ManufacturerDto
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Model = null
            };

            return new ServerResult<ManufacturerDto>
            {
                Success = true,
                Message = "Manufacturer created successfully",
                ResponseCode = 201,
                Data = createdDto
            };
        }


        public async Task<ServerResult<ManufacturerDto?>> GetByIdAsync(string id)
        {
            var entity = await _manufacturerRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServerResult<ManufacturerDto?>
                {
                    Success = true,
                    Message = "Not found",
                    ResponseCode = 404,
                    Data = null
                };
            }

            var dto = Map(entity);
            return new ServerResult<ManufacturerDto?>
            {
                Success = true,
                ResponseCode = 200,
                Data = dto
            };
        }

        public async Task<ServerResult<List<ManufacturerDto>>> GetListAsync()
        {
            var data = await _manufacturerRepository.GetListAsync();
            var result = new ServerResult<List<ManufacturerDto>>
            {
                Success = true,
                ResponseCode = 200,
                Message = "Items successfully found",
                Data = new List<ManufacturerDto>()
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

        public async Task<ServerResult<ManufacturerDto>> UpdateAsync(string id, string name)
        {
            try
            {
                var entity = await _manufacturerRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new ServerResult<ManufacturerDto>
                    {
                        Success = false,
                        Message = "Manufacturer not found",
                        ResponseCode = 404,
                        Data = null
                    };
                }

                entity.Name = string.IsNullOrWhiteSpace(name) ? entity.Name : name;

                await _manufacturerRepository.UpdateAsync(entity);
                var updatedDto = Map(entity);

                return new ServerResult<ManufacturerDto>
                {
                    Success = true,
                    Message = "Manufacturer updated successfully",
                    ResponseCode = 200,
                    Data = updatedDto
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<ManufacturerDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }


        public async Task<ServerResult<ManufacturerDto>> DeleteAsync(string id)
        {
            try
            {
                var objectId = new ObjectId(id);
                await _manufacturerRepository.DeleteAsync(objectId);

                return new ServerResult<ManufacturerDto>
                {
                    Success = true,
                    Message = "Manufacturer deleted successfully",
                    ResponseCode = 200,
                    Data = null
                };
            }
            catch (InvalidOperationException ex)
            {
                return new ServerResult<ManufacturerDto>
                {
                    Success = false,
                    Message = ex.Message,
                    ResponseCode = 404,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<ManufacturerDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }

        private ManufacturerDto Map(Manufacturer item)
        {
            return new ManufacturerDto
            {
                Id = item.Id.ToString(),
                Name = item.Name
            };
        }

        private Manufacturer Map(ManufacturerDto item)
        {
            return new Manufacturer
            {
                Id = string.IsNullOrWhiteSpace(item.Id) ? ObjectId.Empty : new ObjectId(item.Id),
                Name = item.Name
            };
        }
    }
}
