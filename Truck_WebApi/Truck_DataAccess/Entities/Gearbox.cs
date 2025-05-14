

using MongoDB.Bson.Serialization.Attributes;

namespace Truck_DataAccess.Entities
{
    public class Gearbox : BaseEntity
    {
        [BsonElement("Ratio")]
        public string Ratio { get; set; }

        [BsonElement("MaxTorque")]
        public int MaxTorque { get; set; }

        [BsonElement("MaxSpeed")]
        public int MaxSpeed { get; set; }

        [BsonElement("MaxTemp")]
        public int MaxTemp { get; set; }
    }
}
