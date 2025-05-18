using MongoDB.Bson;
using MongoDB.Driver;
using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public class ManufacturerRepository : IRepository<Manufacturer, ManufacturerFilter>
    {
        private readonly IMongoCollection<Manufacturer> _collection;

        public ManufacturerRepository(IMongoClient client)
        {
            var database = client.GetDatabase("TruckDB");
            _collection = database.GetCollection<Manufacturer>("Manufacturer");
        }

        public async Task CreateAsync(Manufacturer item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public async Task<Manufacturer?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<Manufacturer>
                .Filter
                .Eq(item => item.Id, objectId);
            var cursor = await _collection.FindAsync(filter);
            var result = await cursor.ToListAsync();

            if (result != null && result.Count > 0)
            {
                return result[0];
            }

            return null;
        }

        public async Task<List<Manufacturer>> GetListAsync()
        {
            var filter = Builders<Manufacturer>
                .Filter
                .Empty;
            var cursor = await _collection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task UpdateAsync(Manufacturer item)
        {
            throw new NotImplementedException();
        }

        Task<List<Manufacturer>> IRepository<Manufacturer, ManufacturerFilter>.GetListAsync(ManufacturerFilter args)
        {
            throw new NotImplementedException();
        }
    }
}
