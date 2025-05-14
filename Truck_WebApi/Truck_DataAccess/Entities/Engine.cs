
using MongoDB.Bson.Serialization.Attributes;

namespace Truck_DataAccess.Entities
{
    public class Engine : BaseEntity
    {
        [BsonElement("Cilinders")]
        public int Cilinders { get; set; }

        [BsonElement("Power")]
        public int Power { get; set; }

        [BsonElement("MaxTorque")]
        public int MaxTorque { get; set; }
    }
}
