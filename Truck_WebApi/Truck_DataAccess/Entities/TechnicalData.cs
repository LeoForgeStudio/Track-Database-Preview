using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Truck_Shared.Dto;
using Truck_Shared.Enums;

namespace Truck_DataAccess.Entities
{
    public class TechnicalData
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Engine { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Gearbox { get; set; }
        public int Weight { get; set; }
        public FuelType FuelType { get; set; }
        public string Color { get; set; }
        public int Axle { get; set; }
        public DimentionsDto Dimentions { get; set; }
        public int WheelBase { get; set; }
        public EmissionClass EmissionClass { get; set; }
    }
}
