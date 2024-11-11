using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class AuthService : IAuthService
{
    private readonly CourseSystemContext _context;

    public AuthService(CourseSystemContext context)
    {
        _context = context;
    }

    public async Task<User> AuthenticateAdminAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Role == "Admin");
        return user?.Password == password ? user : null;
    }

    public async Task<User> AuthenticateTeacherAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Role == "Teacher");
        return user?.Password == password ? user : null;
    }

    public async Task<User> AuthenticateStudentAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Role == "Student");
        return user?.Password == password ? user : null;
    }

    public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Password = newPassword;
        _context.Users.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task LogoutAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return;
    }
}
