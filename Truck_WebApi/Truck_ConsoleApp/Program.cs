using MongoDB.Driver;
using Truck_BusnessLogic.Services;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Enums;

namespace Truck_ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var TruckRepository = new TruckRepository(mongoClient);
            var EngineRepository = new EngineRepository(mongoClient);
            var GearboxRepository = new GearboxRepository(mongoClient);
            var ManufacturerRepository = new ManufacturerRepository(mongoClient);

            var userRepository = new UserRepository(mongoClient);
            var userService = new UserService(userRepository);

            var newUser = new UserDto
            {
                UserName = "admin",
                Password = "admin",
                RegDate = DateTime.Now,
                Email = "admin@admin.com"
            };

            await userService.CreateAsync(newUser);

            var newEngine = new Engine
            {
                Model = "OB-650",
                Cilinders = 6,
                Power = 330,
                MaxTorque = 3000,
            };

            await EngineRepository.CreateAsync(newEngine);


            var newGearbox = new Gearbox
            {
                Model = "OP-650",
                Ratio = "14.6,12.7,10,9,8,6,5,3,2,1",
                MaxTorque = 3000,
                MaxSpeed = 110,
                MaxTemp = 110,
            };

            await GearboxRepository.CreateAsync(newGearbox);

            var newMan = new Manufacturer
            {
                Name = "Mersedes Benz"
            };

            await ManufacturerRepository.CreateAsync(newMan);

            var newTruck = new Truck
            {
                Manufacturer = newMan.Id.ToString(),
                Model = "Actros",
                ConstructDate = new DateOnly(2020, 5, 14),
                Condition = Condition.Used,
                TechnicalData = new TechnicalData
                {
                    Engine = newEngine.Id.ToString(),
                    Gearbox = newGearbox.Id.ToString(),
                    Weight = 3852,
                    FuelType = FuelType.Diesel,
                    Color = "White",
                    Axle = 4,
                    Dimentions = new DimentionsDto
                    {
                        Length = 5,
                        Height = 5,
                        Width = 5,
                    },
                    WheelBase = 3850,
                    EmissionClass = EmissionClass.Euro6
                },
                Price = 170000,
                Location = "Vilnius",
                Description = "MB Actros 2020"
            };

            await TruckRepository.CreateAsync(newTruck);

            
        }
    }
}
