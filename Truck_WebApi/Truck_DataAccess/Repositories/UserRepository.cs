using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoClient client)
        {
            var database = client.GetDatabase("TruckDB");
            _collection = database.GetCollection<User>("User");
        }

        public async Task CreateAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }

        public async Task<User?> GetByUserNameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(item => item.UserName, username);
            var cursor = await _collection.FindAsync(filter);
            var result = await cursor.ToListAsync();

            if(result != null && result.Count > 0)
            {
                return result[0];
            }
            return null;
        }
    }
}
