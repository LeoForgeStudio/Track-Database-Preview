using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Truck_Shared.Helpers
{
    public static class PasswordHasher
    {
        public static byte[] GenerateSalt(int saltSize = 16)
        {
            var salt = new byte[saltSize];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(salt);
            return salt;
        }

        public static byte[] GenerateHash(
            string password,
            byte[] salt,
            int iteration = 47114,
            int hashByteSize = 32)
        {
            using var generator = new Rfc2898DeriveBytes(password, salt, iteration, HashAlgorithmName.SHA512);
            return generator.GetBytes(hashByteSize);
        }
    }
}
