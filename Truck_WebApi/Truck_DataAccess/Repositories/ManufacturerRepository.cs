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

        public async Task<Manufacturer?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Manufacturer>> GetListAsync()
        {
            throw new NotImplementedException();
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
