namespace API.Services
{
    public interface IHashPasword
    {
        void CreateHashPassword(string password, out string HashPassword, out string PasswordSalt);
        bool VerifyHashPassword(string Password,  string HashPassword, string PasswordSalt);
    }
}
