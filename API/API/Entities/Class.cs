namespace API.Entities;

public class Class
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public string Schedule { get; set; } // Can be JSON or string format for schedule data
    public string Room { get; set; }

    // Relationships
    public virtual Course Course { get; set; }
    public virtual Teacher Teacher { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } // Add this line

}
