using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public class GearboxRepository : IRepository<Gearbox, GearboxFilter>
    {
        private readonly IMongoCollection<Gearbox> _collection;

        public GearboxRepository(IMongoClient client)
        {
            var database = client.GetDatabase("TruckDB");
            _collection = database.GetCollection<Gearbox>("Gearbox");
        }
        public async Task CreateAsync(Gearbox item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<Gearbox>
                .Filter
                .Eq(item => item.Id, id);
            var result = _collection.DeleteOneAsync(filter);
        }

        public async Task<Gearbox?> GetAsync(string id)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<Gearbox>
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

        public async Task<List<Gearbox>> GetListAsync()
        {
            var filter = Builders<Gearbox>
                .Filter
                .Empty;
            var cursor = await _collection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<List<Gearbox>> GetListAsync(GearboxFilter args)
        {
            var builder = Builders<Gearbox>
               .Filter;
            var filters = new List<FilterDefinition<Gearbox>>();

            if (args.MaxTorque > 0)
            {
                var dateFilter = builder.And(
                    builder.Gte(item => item.MaxTorque, 0)
                );

                filters.Add(dateFilter);
            }

            if (args.MaxSpeed > 0)
            {
                var dateFilter = builder.And(
                    builder.Gte(item => item.MaxSpeed, 0)
                );

                filters.Add(dateFilter);
            }

            if (args.MaxTemp > 0)
            {
                var dateFilter = builder.And(
                    builder.Gte(item => item.MaxTemp, 0)
                );

                filters.Add(dateFilter);
            }


            FilterDefinition<Gearbox> finalFilter;
            if (filters.Count > 0)
            {
                finalFilter = builder.And(filters);
            }

            else
            {
                finalFilter = null;
            }

            var cursor = await _collection.FindAsync(finalFilter);
            return await cursor.ToListAsync();
        }

        public async Task UpdateAsync(Gearbox item)
        {
            var filter = Builders<Gearbox>
                .Filter
                .Eq(item => item.Id, item.Id);
            var result = await _collection.FindOneAndReplaceAsync(filter, item);
        }
    }
}
