namespace API.Entities;

public class TeacherAssignment
{
    public int AssignmentId { get; set; }
    public int TeacherId { get; set; }
    public int ClassId { get; set; }
    
    public DateTime AssignedDate { get; set; }

    // Relationships
    public Teacher Teacher { get; set; }
    public Class Class { get; set; }
}
