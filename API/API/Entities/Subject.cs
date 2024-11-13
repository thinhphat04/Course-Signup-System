namespace API.Entities;

public class Subject
{
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public int Credits { get; set; }

    // Relationships
    public Department Department { get; set; }
}
