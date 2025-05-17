using Truck_DataAccess.Entities;

namespace Truck_DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User?> GetByUserNameAsync(string username);
        
    }
}