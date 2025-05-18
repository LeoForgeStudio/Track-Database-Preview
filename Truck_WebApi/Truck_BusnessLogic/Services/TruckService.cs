using MongoDB.Bson;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Entities;
using Truck_Shared.Enums;


namespace Truck_BusnessLogic.Services
{
    public class TruckService : ITruckService
    {
        private readonly IRepository<Truck, TruckFilter> _truckRepository;
        private readonly IRepository<Gearbox, GearboxFilter> _gearboxRepository;
        private readonly IRepository<Engine, EngineFilter> _engineRepository;
        private readonly IRepository<Manufacturer, ManufacturerFilter> _manufacturerRepository;


        public TruckService(IRepository<Truck, TruckFilter> truckRepository, 
                            IRepository<Gearbox, GearboxFilter> gearboxRepository,
                            IRepository<Engine, EngineFilter> engineRepository,
                            IRepository<Manufacturer, ManufacturerFilter> manufacturerRepository)
        {
            _truckRepository = truckRepository;
            _gearboxRepository = gearboxRepository;
            _engineRepository = engineRepository;
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<ServerResult<TruckDto>> CreateAsync(TruckDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Model) ||
                string.IsNullOrWhiteSpace(dto.Manufacturer) ||
                dto.TechnicalData == null ||
                string.IsNullOrWhiteSpace(dto.TechnicalData.Engine) ||
                string.IsNullOrWhiteSpace(dto.TechnicalData.Gearbox))
            {
                return new ServerResult<TruckDto>
                {
                    Success = false,
                    Message = "Missing required fields",
                    ResponseCode = 400,
                    Data = null
                };
            }

            var manufacturer = await _manufacturerRepository.GetByIdAsync(dto.Manufacturer);
            var engine = await _engineRepository.GetByIdAsync(dto.TechnicalData.Engine);
            var gearbox = await _gearboxRepository.GetByIdAsync(dto.TechnicalData.Gearbox);

            if (manufacturer == null || engine == null || gearbox == null)
            {
                return new ServerResult<TruckDto>
                {
                    Success = false,
                    Message = "Manufacturer, engine or gearbox not found",
                    ResponseCode = 404,
                    Data = null
                };
            }

            var trucks = await _truckRepository.GetListAsync();
            var existing = trucks.FirstOrDefault(x =>
                x.Model == dto.Model &&
                x.Manufacturer == manufacturer.Id.ToString() &&
                x.TechnicalData.Engine == engine.Id.ToString() &&
                x.TechnicalData.Gearbox == gearbox.Id.ToString());

            if (existing != null)
            {
                return new ServerResult<TruckDto>
                {
                    Success = false,
                    Message = "Truck already exists",
                    ResponseCode = 409,
                    Data = null
                };
            }

            var entity = Map(dto);
            await _truckRepository.CreateAsync(entity);
            var createdDto = await MapAsync(entity);

            return new ServerResult<TruckDto>
            {
                Success = true,
                Message = "Truck created successfully",
                ResponseCode = 201,
                Data = createdDto
            };
        }

        public async Task<ServerResult<TruckDto?>> GetByIdAsync(string id)
        {
            
            var result = await _truckRepository.GetByIdAsync(id);
            if (result == null)
            {
                return new ServerResult<TruckDto?>
                {
                    Success = true,
                    Message = "Not found",
                    ResponseCode = 404

                };
            }
            var dto = await MapAsync(result);

            return new ServerResult<TruckDto?>
            {
                Success = true,
                ResponseCode = 200,
                Data = dto
            };
        }

        public async Task<ServerResult<List<TruckDto?>>> GetListAsync()
        {
            var data = await _truckRepository.GetListAsync();
            var result = new ServerResult<List<TruckDto>>
            {
                Success = true,
            };

            if (data != null && data.Count > 0)
            {
                var dtoList = await Task.WhenAll(data.Select(item => MapAsync(item)));
                result.Data = dtoList.ToList();
            }
            else
            {
                result.Data = new List<TruckDto>();
            }
            return result;
        }

        public async Task<ServerResult<TruckDto>> UpdateAsync(string id, TruckDto item)
        {
            try
            {
                var entity = await _truckRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new ServerResult<TruckDto>
                    {
                        Success = false,
                        Message = "Truck not found",
                        ResponseCode = 404,
                        Data = null
                    };
                }

                item.Id = entity.Id.ToString();
                item.Model = string.IsNullOrWhiteSpace(item.Model) ? entity.Model : item.Model;
                item.Manufacturer = string.IsNullOrWhiteSpace(item.Manufacturer) ? entity.Manufacturer : item.Manufacturer;

                if (item.ConstructDate != default && item.ConstructDate != entity.ConstructDate)
                {
                    entity.ConstructDate = item.ConstructDate;
                }

                item.Condition = item.Condition == 0 ? entity.Condition : item.Condition;
                item.Price = item.Price == 0 ? entity.Price : item.Price;
                item.Location = string.IsNullOrWhiteSpace(item.Location) ? entity.Location : item.Location;
                item.Description = string.IsNullOrWhiteSpace(item.Description) ? entity.Description : item.Description;

                var td = item.TechnicalData;
                var et = entity.TechnicalData;

                td.Engine = string.IsNullOrWhiteSpace(td.Engine) ? et.Engine : td.Engine;
                td.Gearbox = string.IsNullOrWhiteSpace(td.Gearbox) ? et.Gearbox : td.Gearbox;
                td.Weight = td.Weight == 0 ? et.Weight : td.Weight;
                td.FuelType = td.FuelType == 0 ? et.FuelType : td.FuelType;
                td.Color = string.IsNullOrWhiteSpace(td.Color) ? et.Color : td.Color;
                td.Axle = td.Axle == 0 ? et.Axle : td.Axle;
                td.WheelBase = td.WheelBase == 0 ? et.WheelBase : td.WheelBase;
                td.EmissionClass = td.EmissionClass == 0 ? et.EmissionClass : td.EmissionClass;

                if (td.Dimentions != null && et.Dimentions != null)
                {
                    td.Dimentions.Length = td.Dimentions.Length == 0 ? et.Dimentions.Length : td.Dimentions.Length;
                    td.Dimentions.Width = td.Dimentions.Width == 0 ? et.Dimentions.Width : td.Dimentions.Width;
                    td.Dimentions.Height = td.Dimentions.Height == 0 ? et.Dimentions.Height : td.Dimentions.Height;
                }

                var updatedEntity = Map(item);
                await _truckRepository.UpdateAsync(updatedEntity);

                var updatedDto = await MapAsync(updatedEntity);

                return new ServerResult<TruckDto>
                {
                    Success = true,
                    Message = "Truck updated successfully",
                    ResponseCode = 200,
                    Data = updatedDto
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<TruckDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }

        public async Task<ServerResult<TruckDto>> DeleteAsync(string id)
        {
            try
            {
                var objectId = new ObjectId(id);
                await _truckRepository.DeleteAsync(objectId);

                return new ServerResult<TruckDto>
                {
                    Success = true,
                    Message = "Truck deleted successfully",
                    ResponseCode = 200,
                    Data = null
                };
            }
            catch (InvalidOperationException ex)
            {
                return new ServerResult<TruckDto>
                {
                    Success = false,
                    Message = ex.Message,
                    ResponseCode = 404,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<TruckDto>
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    ResponseCode = 500,
                    Data = null
                };
            }
        }

        private async Task<TruckDto> MapAsync(Truck item)
        {
            var engine = await _engineRepository.GetByIdAsync(item.TechnicalData.Engine);
            var gearbox = await _gearboxRepository.GetByIdAsync(item.TechnicalData.Gearbox);

            return new TruckDto
            {
                Id = item.Id.ToString(),
                Model = item.Model,
                Manufacturer = item.Manufacturer,
                Condition = item.Condition,
                TechnicalData = new TechnicalDataDto
                {
                    Engine = item.TechnicalData.Engine,
                    Gearbox = item.TechnicalData.Gearbox,
                    Weight = item.TechnicalData.Weight,
                    FuelType = item.TechnicalData.FuelType,
                    Color = item.TechnicalData.Color,
                    Axle = item.TechnicalData.Axle,
                    Dimentions = new DimentionsDto
                    {
                        Length = item.TechnicalData.Dimentions.Length,
                        Height = item.TechnicalData.Dimentions.Height,
                        Width = item.TechnicalData.Dimentions.Width
                    },
                    WheelBase = item.TechnicalData.WheelBase,
                    EmissionClass = item.TechnicalData.EmissionClass
                },
                Price = item.Price,
                Location = item.Location,
                Description = item.Description
            };
        }

        private Truck Map(TruckDto item)
        {
            return new Truck
            {
                Id = string.IsNullOrWhiteSpace(item.Id) ? ObjectId.Empty : new ObjectId(item.Id),
                Model = item.Model,
                Manufacturer = item.Manufacturer,
                Condition = item.Condition,
                TechnicalData = new TechnicalData
                {
                    Engine = item.TechnicalData.Engine,
                    Gearbox = item.TechnicalData.Gearbox,
                    Weight = item.TechnicalData.Weight,
                    FuelType = item.TechnicalData.FuelType,
                    Color = item.TechnicalData.Color,
                    Axle = item.TechnicalData.Axle,
                    Dimentions = item.TechnicalData.Dimentions,
                    WheelBase = item.TechnicalData.WheelBase,
                    EmissionClass = item.TechnicalData.EmissionClass
                },
                Price = item.Price,
                Location = item.Location,
                Description = item.Description
            };
        }

    }
}
