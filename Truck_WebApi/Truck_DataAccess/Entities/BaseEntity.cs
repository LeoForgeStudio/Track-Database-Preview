using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace Truck_DataAccess.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Model")]
        public string Model { get; set; }
    }
}
