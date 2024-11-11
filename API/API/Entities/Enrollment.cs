namespace API.Entities;

public class Enrollment
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Status { get; set; } // Active, Completed, Canceled

    // Relationships
    public virtual Student Student { get; set; }
    public virtual Class Class { get; set; }
    public virtual ICollection<Attendance> Attendances { get; set; }
    public virtual ICollection<Grade> Grades { get; set; }
}
