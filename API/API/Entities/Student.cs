namespace API.Entities;

public class Student
{
    public int StudentId { get; set; }
    public int UserId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }

    // Relationships
    public User User { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}
