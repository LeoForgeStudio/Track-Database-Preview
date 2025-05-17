using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Truck_DataAccess.Entities
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.Empty;

        [BsonElement("UserName")]
        public string UserName { get; set; }
        
        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("RegDate")]
        public DateTime RegDate { get; set; }

        [BsonElement("FirstNameEncrypted")]
        public byte[] FirstNameEncrypted { get; set; }

        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

    }
}
