using API.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace API.Repositories
{
    public class HashPasswordRepository : IHashPasword
    {
        public void CreateHashPassword(string password, out string HashPassword, out string PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = Convert.ToBase64String(hmac.Key);
                var HashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                HashPassword = Convert.ToBase64String(HashBytes);
            }
        }

        public bool VerifyHashPassword(string Password, string HashPassword, string PasswordSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(PasswordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));

                var passwordHashBytes = Convert.FromBase64String(HashPassword);

                return computedHash.SequenceEqual(passwordHashBytes);
            }
        }
    }
}
