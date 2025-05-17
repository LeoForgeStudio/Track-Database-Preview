using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck_DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection _userRepository;
    }
}
