namespace API.Dto.Teacher;

public class TeacherAssignmentDto
{
    public int TeacherId { get; set; }
    public int ClassId { get; set; }
    public DateTime AssignedDate { get; set; }
}