namespace API.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Relationships
    public  ICollection<Subject> Subjects { get; set; }
}
