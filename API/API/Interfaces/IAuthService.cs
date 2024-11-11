using API.Entities;

namespace API.Interfaces;

public interface IAuthService
{
    Task<User> AuthenticateAdminAsync(string username, string password);
    Task<User> AuthenticateTeacherAsync(string username, string password);
    Task<User> AuthenticateStudentAsync(string username, string password);
    Task LogoutAsync(int userId);
    Task<bool> ResetPasswordAsync(int userId, string newPassword);
}