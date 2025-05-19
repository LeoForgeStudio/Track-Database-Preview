using MongoDB.Bson;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Repositories;
using Truck_Shared.Dto;
using Truck_Shared.Entities;
using Truck_Shared.Helpers;

namespace Truck_BusnessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServerResult> CreateAsync(UserDto newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.UserName) || string.IsNullOrWhiteSpace(newUser.Password))
            {
                return new ServerResult
                {
                    Success = false,
                    ResponseCode = 400,
                    Message = "Username and password are required"
                };
            }

            var existingUser = await _repository.GetByUserNameAsync(newUser.UserName);
            if (existingUser != null)
            {
                return new ServerResult
                {
                    Success = false,
                    ResponseCode = 409,
                    Message = "User with this username already exists"
                };
            }

            var item = Map(newUser);
            item.RegDate = DateTime.Now;
            item.PasswordSalt = PasswordHasher.GenerateSalt();
            item.PasswordHash = PasswordHasher.GenerateHash(newUser.Password, item.PasswordSalt);

            await _repository.CreateAsync(item);

            return new ServerResult
            {
                Success = true,
                ResponseCode = 200,
                Message = "User created successfully"
            };
        }

        private UserDto Map(User item)
        {
            return new UserDto
            {
                Id = item.Id.ToString(),
                UserName = item.UserName,
                Email = item.Email,
                RegDate = item.RegDate,
            };
        }

        private User Map(UserDto item)
        {
            var result = new User
            {
                UserName = item.UserName,
                Email = item.Email,
                RegDate = item.RegDate,
            };

            if (!string.IsNullOrWhiteSpace(item.Id))
            {
                result.Id = new ObjectId(item.Id);
            }

            return result;


        }
    }
}
