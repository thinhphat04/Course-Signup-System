using API.Entities;

namespace API.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(int userId);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> UpdateUserAsync(int userId, User user);
    Task<bool> DeleteUserAsync(int userId);
}