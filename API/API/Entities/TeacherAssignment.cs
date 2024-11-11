namespace API.Entities;

public class TeacherAssignment
{
    public int AssignmentId { get; set; }
    public int TeacherId { get; set; }
    public int ClassId { get; set; }
    
    public DateTime AssignedDate { get; set; }

    // Relationships
    public virtual Teacher Teacher { get; set; }
    public virtual Class Class { get; set; }
}
