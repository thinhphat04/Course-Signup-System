namespace API.Entities;

public class Class
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public string Schedule { get; set; } // Can be JSON or string format for schedule data
    public string Room { get; set; }

    // Relationships
    public  Course Course { get; set; }
    public  Teacher Teacher { get; set; }
    public  ICollection<Enrollment> Enrollments { get; set; }
    public  ICollection<TeacherAssignment> TeacherAssignments { get; set; } // Add this line

}
