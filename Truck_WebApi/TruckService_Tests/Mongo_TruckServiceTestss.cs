using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Truck_BusnessLogic.Services;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Enums;

namespace TruckService_Tests
{
    [TestClass]
    public class TruckServiceIntegrationTests
    {
        [TestMethod]
        public async Task CreateTruckTest_Create_Engine_Gearbox_Manufacturer_ReturnTrue()
        {
            // Arrange
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("TruckDB");

            var truckRepo = new TruckRepository(client);
            var engineRepo = new EngineRepository(client);
            var gearboxRepo = new GearboxRepository(client);
            var manufacturerRepo = new ManufacturerRepository(client);
            var service = new TruckService(truckRepo, gearboxRepo, engineRepo, manufacturerRepo);

            var engine = new Engine { Id = ObjectId.GenerateNewId(), Model = "MX-800" + Guid.NewGuid() };
            var gearbox = new Gearbox { Id = ObjectId.GenerateNewId(), Model = "XF-650" + Guid.NewGuid(), Ratio = "14,16,11,9,7,5,2,1", MaxSpeed = 100, MaxTorque = 700, MaxTemp = 90 };
            var manufacturer = new Manufacturer { Id = ObjectId.GenerateNewId(), Name = "DAF" + Guid.NewGuid() };

            await db.GetCollection<Engine>("Engine").InsertOneAsync(engine);
            await db.GetCollection<Gearbox>("Gearbox").InsertOneAsync(gearbox);
            await db.GetCollection<Manufacturer>("Manufacturer").InsertOneAsync(manufacturer);

            var dto = new TruckDto
            {
                Model = "XF" + Guid.NewGuid(),
                Manufacturer = manufacturer.Id.ToString(),
                TechnicalData = new TechnicalDataDto
                {
                    Engine = engine.Id.ToString(),
                    Gearbox = gearbox.Id.ToString(),
                    Dimentions = new DimentionsDto { Length = 1200, Width = 500, Height = 600 },
                    Weight = 17000,
                    FuelType = FuelType.Diesel,
                    Color = "Red",
                    Axle = 2,
                    WheelBase = 3000,
                    EmissionClass = EmissionClass.Euro6
                },
                Price = 123456,
                Location = "Vilnius",
                Description = "Created truck on MS test"
            };

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);

            var truckInDb = await db.GetCollection<Truck>("Truck")
                .Find(t => t.Model == dto.Model && t.Location == dto.Location)
                .FirstOrDefaultAsync();

            Assert.IsNotNull(truckInDb);
            Assert.AreEqual(dto.Description, truckInDb.Description);
            Assert.AreEqual(dto.TechnicalData.Color, truckInDb.TechnicalData.Color);
            Assert.AreEqual(dto.TechnicalData.Engine, truckInDb.TechnicalData.Engine);
        }
    
        [TestMethod]
        public async Task UpdateTruckTest_ReturnTrue()
            {
                // Arrange
                var client = new MongoClient("mongodb://localhost:27017");
                var db = client.GetDatabase("TruckDB");

                var truckRepo = new TruckRepository(client);
                var engineRepo = new EngineRepository(client);
                var gearboxRepo = new GearboxRepository(client);
                var manufacturerRepo = new ManufacturerRepository(client);
                var service = new TruckService(truckRepo, gearboxRepo, engineRepo, manufacturerRepo);

                var engine = new Engine { Id = ObjectId.GenerateNewId(), Model = "CG-600" + Guid.NewGuid() };
                var gearbox = new Gearbox { Id = ObjectId.GenerateNewId(), Model = "XD-500-" + Guid.NewGuid(), Ratio = "14,16,11,9,7,5,2,1", MaxSpeed = 100, MaxTorque = 800, MaxTemp = 85 };
                var manufacturer = new Manufacturer { Id = ObjectId.GenerateNewId(), Name = "MAN" + Guid.NewGuid() };

                await db.GetCollection<Engine>("Engine").InsertOneAsync(engine);
                await db.GetCollection<Gearbox>("Gearbox").InsertOneAsync(gearbox);
                await db.GetCollection<Manufacturer>("Manufacturer").InsertOneAsync(manufacturer);

                var dto = new TruckDto
                {
                    Model = "XC" + Guid.NewGuid(),
                    Manufacturer = manufacturer.Id.ToString(),
                    TechnicalData = new TechnicalDataDto
                    {
                        Engine = engine.Id.ToString(),
                        Gearbox = gearbox.Id.ToString(),
                        Dimentions = new DimentionsDto { Length = 1200, Width = 500, Height = 600 },
                        Weight = 17000,
                        FuelType = FuelType.Diesel,
                        Color = "White",
                        Axle = 2,
                        WheelBase = 3000,
                        EmissionClass = EmissionClass.Euro6
                    },
                    Price = 150000,
                    Location = "Kaunas",
                    Description = "Update truck Test"
                };

                // Insert truck
                var createResult = await service.CreateAsync(dto);
                Assert.IsTrue(createResult.Success);
                var truckId = createResult.Data.Id;

                // Modify data
                dto.Id = truckId;
                dto.Description = "Update truck Test Success";
                dto.TechnicalData.Color = "Red";
                dto.Price = 160000;

                // Act
                var updateResult = await service.UpdateAsync(truckId, dto);

                // Assert
                Assert.IsTrue(updateResult.Success);
                Assert.AreEqual("Update truck Test Success", updateResult.Data.Description);
                Assert.AreEqual("Red", updateResult.Data.TechnicalData.Color);
                Assert.AreEqual(160000, updateResult.Data.Price);
            }
        
    }
}