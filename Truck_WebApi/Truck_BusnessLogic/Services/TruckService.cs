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

        public async Task<ServerResult<TruckDto>> GetByIdAsync(string id)
        {
            
            var result = await _truckRepository.GetAsync(id);
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

        private async Task<TruckDto> MapAsync(Truck item)
        {
            var engine = await _engineRepository.GetAsync(item.TechnicalData.Engine);
            var gearbox = await _gearboxRepository.GetAsync(item.TechnicalData.Gearbox);

            return new TruckDto
            {
                Id = item.Id.ToString(),
                Model = item.Model,
                Manufacturer = item.Manufacturer,
                Condition = item.Condition,
                TechnicalData = new TechnicalDataDto
                {
                    Engine = new EngineDto
                    {
                        Id = engine.Id.ToString(),
                        Model = engine.Model,
                        Cilinders = engine.Cilinders,
                        Power = engine.Power
                    },
                    Gearbox = new GearboxDto
                    {
                        Id = gearbox.Id.ToString(),
                        Model = gearbox.Model,
                        Ratio = gearbox.Ratio,
                        MaxSpeed = gearbox.MaxSpeed,
                        MaxTemp = gearbox.MaxTemp,
                        MaxTorque = gearbox.MaxTorque
                    },
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
                Id = new ObjectId(item.Id),
                Model = item.Model,
                Manufacturer = item.Manufacturer,
                Condition = item.Condition,
                TechnicalData = new TechnicalData
                {
                    Engine = item.TechnicalData.Engine.Id,
                    Gearbox = item.TechnicalData.Gearbox.Id,
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
                    EmissionClass = item.TechnicalData.EmissionClass,
                },
                Price = item.Price,
                Location = item.Location,
                Description = item.Description
            };
        }
    }
}
