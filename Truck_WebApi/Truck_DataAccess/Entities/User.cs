using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Truck_DataAccess.Entities
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }

    }
}
