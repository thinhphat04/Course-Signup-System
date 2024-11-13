namespace API.Entities;

public class Enrollment
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Status { get; set; } // Active, Completed, Canceled

    // Relationships
    public  Student Student { get; set; }
    public  Class Class { get; set; }
    public  ICollection<Attendance> Attendances { get; set; }
    public  ICollection<Grade> Grades { get; set; }
}
