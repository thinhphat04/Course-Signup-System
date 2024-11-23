namespace API.Dto.Student;


public class EnrollmentDto
{
    public int ClassId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Status { get; set; } // Active, Completed, Canceled
}
