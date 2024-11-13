namespace API.Entities;

public class Attendance
{
    public int AttendanceId { get; set; }
    public int EnrollmentId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } // Present, Absent

    // Relationships
    public  Enrollment Enrollment { get; set; }
}
