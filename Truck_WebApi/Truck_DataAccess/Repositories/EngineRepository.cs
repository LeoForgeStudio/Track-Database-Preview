using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Entities.Filters;

namespace Truck_DataAccess.Repositories
{
    public class EngineRepository : IRepository<Engine,EngineFilter>
    {
        private readonly IMongoCollection<Engine> _collection;

        public EngineRepository(IMongoClient client)
        {
            var database = client.GetDatabase("TruckDB");
            _collection = database.GetCollection<Engine>("Engine");
        }
        public async Task CreateAsync(Engine item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<Engine>
                .Filter
                .Eq(item => item.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            if (result.DeletedCount == 0)
            {
                throw new InvalidOperationException("Engine not found or already deleted.");
            }
        }

        public async Task<Engine?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<Engine>
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

        public async Task<List<Engine>> GetListAsync()
        {
            var filter = Builders<Engine>
                .Filter
                .Empty;
            var cursor = await _collection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<List<Engine>> GetListAsync(EngineFilter args)
        {
            var builder = Builders<Engine>
               .Filter;
            var filters = new List<FilterDefinition<Engine>>();

            if (args.Cilinders > 0)
            {
                var dateFilter = builder.And(
                    builder.Gte(item => item.Cilinders, 0)
                );

                filters.Add(dateFilter);
            }

            if (args.MaxPower > 0)
            {
                var dateFilter = builder.And(
                    builder.Gte(item => item.Power, 0)
                );

                filters.Add(dateFilter);
            }

            if (args.MaxTorque > 0)
            {
                var dateFilter = builder.And(
                    builder.Gte(item => item.MaxTorque, 0)
                );

                filters.Add(dateFilter);
            }

            FilterDefinition<Engine> finalFilter;
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

        public async Task UpdateAsync(Engine item)
        {
            var filter = Builders<Engine>
                .Filter
                .Eq(item => item.Id, item.Id);
            var result = await _collection.FindOneAndReplaceAsync(filter, item);
        }
    }
}
