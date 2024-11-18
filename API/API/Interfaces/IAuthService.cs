using API.Dto;

namespace API.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> AuthenticateAsync(string username, string password, string role);
    Task<bool> ResetPasswordAsync(int userId, string newPassword);
}