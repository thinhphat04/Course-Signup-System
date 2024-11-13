namespace API.Entities;

public class Teacher
{
    public int TeacherId { get; set; }
    public int UserId { get; set; }
    public string Specialization { get; set; }

    // Relationships
    public User User { get; set; }
    public ICollection<Class> Classes { get; set; }
    public ICollection<Salary> Salaries { get; set; }
    public ICollection<TeacherAssignment> TeacherAssignments { get; set; }
}
