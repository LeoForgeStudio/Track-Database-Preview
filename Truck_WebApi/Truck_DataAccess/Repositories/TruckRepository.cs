using MongoDB.Bson;
using MongoDB.Driver;

using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public class TruckRepository : IRepository<Truck, TruckFilter>
    {
        private readonly IMongoCollection<Truck> _collection;

        public TruckRepository(IMongoClient client)
        {
            var database = client.GetDatabase("TruckDB");
            _collection = database.GetCollection<Truck>("Truck");
        }

        public async Task CreateAsync(Truck item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<Truck?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<Truck>
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

        public async Task<List<Truck>> GetListAsync()
        {
            var filter = Builders<Truck>
                .Filter
                .Empty;
            var cursor = await _collection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<List<Truck>> GetListAsync(TruckFilter args)
        {
            var builder = Builders<Truck>
                .Filter;
            var filters = new List<FilterDefinition<Truck>>();

            if (!string.IsNullOrWhiteSpace(args.Manufacturer))
            {
                var nameFilter = builder.Eq(item => item.Manufacturer, args.Manufacturer);
                filters.Add(nameFilter);

            }

            if (!string.IsNullOrWhiteSpace(args.Model))
            {
                var surenameFilter = builder.Eq(item => item.Model, args.Model);
                filters.Add(surenameFilter);

            }

            if (args.ConstructFrom.HasValue || args.ConstructTo.HasValue)
            {
                var from = args.ConstructFrom ?? DateOnly.MinValue;
                var to = args.ConstructTo ?? DateOnly.MaxValue;

                var dateFilter = builder.And(
                    builder.Gte(item => item.ConstructDate, from),
                    builder.Lte(item => item.ConstructDate, to)
                );

                filters.Add(dateFilter);
            }

            if (args.PriceFrom > 0 && args.PriceTo > 0)
            {
                var priceFilter = builder.And(
                    builder.Gte(item => item.Price, args.PriceFrom),
                    builder.Lte(item => item.Price, args.PriceTo)
                );
                filters.Add(priceFilter);
            }
            else if (args.PriceFrom > 0)
            {
                var priceFilter = builder.Gte(item => item.Price, args.PriceFrom);
                filters.Add(priceFilter);
            }
            else if (args.PriceTo > 0)
            {
                var priceFilter = builder.Lte(item => item.Price, args.PriceTo);
                filters.Add(priceFilter);
            }

            if (args.Condition.HasValue)
            {
                var surenameFilter = builder.Eq(item => item.Condition, args.Condition);
                filters.Add(surenameFilter);

            }

            if (!string.IsNullOrWhiteSpace(args.Location))
            {
                var nameFilter = builder.Eq(item => item.Location, args.Location);
                filters.Add(nameFilter);

            }

            FilterDefinition<Truck> finalFilter;
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

        public async Task UpdateAsync(Truck item)
        {
            var filter = Builders<Truck>
                .Filter
                .Eq(item => item.Id, item.Id);
            var result = await _collection.FindOneAndReplaceAsync(filter, item);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<Truck>
                .Filter
                .Eq(item => item.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            if (result.DeletedCount == 0)
            {
                throw new InvalidOperationException("Truck not found or already deleted.");
            }
        }
    }
}
