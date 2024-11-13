namespace API.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // Admin, Teacher, Student
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    // Relationships
    public Student Student { get; set; }
    public Teacher Teacher { get; set; }
}
