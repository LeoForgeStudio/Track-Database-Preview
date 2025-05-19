using MongoDB.Bson;
using Moq;
using Truck_BusnessLogic.Services;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Enums;

namespace TruckService_Tests
{
    [TestClass]
    public sealed class Moq_TruckServiceTests
    {
        private Mock<IRepository<Truck, TruckFilter>> _truckRepo;
        private Mock<IRepository<Engine, EngineFilter>> _engineRepo;
        private Mock<IRepository<Gearbox, GearboxFilter>> _gearboxRepo;
        private Mock<IRepository<Manufacturer, ManufacturerFilter>> _manufacturerRepo;
        private TruckService _service;

        [TestInitialize]
        public void Setup()
        {
            _truckRepo = new Mock<IRepository<Truck, TruckFilter>>();
            _engineRepo = new Mock<IRepository<Engine, EngineFilter>>();
            _gearboxRepo = new Mock<IRepository<Gearbox, GearboxFilter>>();
            _manufacturerRepo = new Mock<IRepository<Manufacturer, ManufacturerFilter>>();

            _service = new TruckService(
                _truckRepo.Object,
                _gearboxRepo.Object,     
                _engineRepo.Object,
                _manufacturerRepo.Object
            );
        }

        [TestMethod]
        public async Task CreateTruckEntryUsingMoqReturnsValid()
        {
            // Arrange
            var dto = new TruckDto
            {
                Model = "T-500",
                Manufacturer = "manufacturerId",
                TechnicalData = new TechnicalDataDto
                {
                    Engine = "engineId",
                    Gearbox = "gearboxId",
                    Dimentions = new DimentionsDto { Length = 1000, Width = 500, Height = 600 },
                    Weight = 5000,
                    FuelType = FuelType.Diesel,
                    Color = "Red",
                    Axle = 2,
                    WheelBase = 4000,
                    EmissionClass = EmissionClass.Euro6
                },
                Price = 100000,
                Location = "Kaunas",
                Description = "Test truck"
            };

            _manufacturerRepo.Setup(r => r.GetByIdAsync("manufacturerId"))
                .ReturnsAsync(new Manufacturer { Id = ObjectId.GenerateNewId(), Name = "MAN" });

            _engineRepo.Setup(r => r.GetByIdAsync("engineId"))
                .ReturnsAsync(new Engine { Id = ObjectId.GenerateNewId(), Model = "V8" });

            _gearboxRepo.Setup(r => r.GetByIdAsync("gearboxId"))
                .ReturnsAsync(new Gearbox { Id = ObjectId.GenerateNewId(), Model = "Auto" });

            _truckRepo.Setup(r => r.GetListAsync()).ReturnsAsync(new List<Truck>());

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(201, result.ResponseCode);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("T-500", result.Data.Model);
        }
    }
}
