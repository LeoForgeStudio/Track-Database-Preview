using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public interface IRepository
    {
        Task CreateAsync(Truck item);
        Task<Truck?> GetAsync(string id);
        Task<List<Truck>> GetListAsync();
        Task<List<Truck>> GetListAsync(TruckFilter args);
        Task UpdateAsync(Truck item);
        Task DeleteAsync(ObjectId id);
    }
}
