namespace API.Entities;

public class Grade
{
    public int GradeId { get; set; }
    public int EnrollmentId { get; set; }
    public int GradeTypeId { get; set; }
    public double Score { get; set; }
    public DateTime DateRecorded { get; set; }

    // Relationships
    public virtual Enrollment Enrollment { get; set; }
    public virtual GradeType GradeType { get; set; }
}
