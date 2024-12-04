using API.Entities;

namespace API.Services
{
    public interface IGenerateService
    {
        Task<string> GenerateCodeAsync();
        Task<string> GenerateJwtToken(User user);
        Task<bool> SendEmail(string email); 
        Task<bool> VerificationToken(string email, string Token);
        Task<string> GenerateVerificationToken (string email);
    }
}
