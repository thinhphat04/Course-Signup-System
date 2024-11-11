namespace API.Entities;

public class Teacher
{
    public int TeacherId { get; set; }
    public int UserId { get; set; }
    public string Specialization { get; set; }

    // Relationships
    public virtual User User { get; set; }
    public virtual ICollection<Class> Classes { get; set; }
    public virtual ICollection<Salary> Salaries { get; set; }
    public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; }
}
