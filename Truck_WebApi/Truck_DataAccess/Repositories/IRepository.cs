using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public interface IRepository<TEntity, Tfilter>
    {
        Task CreateAsync(TEntity item);
        Task<TEntity?> GetAsync(string id);
        Task<List<TEntity>> GetListAsync();
        Task<List<TEntity>> GetListAsync(Tfilter args);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(ObjectId id);
    }
}
