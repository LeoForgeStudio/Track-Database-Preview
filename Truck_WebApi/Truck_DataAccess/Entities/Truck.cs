using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Truck_Shared.Enums;
using Truck_Shared.Dto;

namespace Truck_DataAccess.Entities
{
    public class Truck : BaseEntity
    {
        [BsonElement("Manufacturer")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Manufacturer { get; set; }

        [BsonElement("ConstructDate")]
        public DateOnly ConstructDate { get; set; }

        [BsonElement("Condition")]
        public Condition Condition { get; set; }

        [BsonElement("TechnicalData")]
        public required TechnicalData TechnicalData { get; set; }

        [BsonElement("Price")]
        public int Price { get; set; }

        [BsonElement("Location")]
        public string Location { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
