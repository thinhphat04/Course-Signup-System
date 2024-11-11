namespace API.Entities;

public class GradeType
{
    public int GradeTypeId { get; set; }
    public string Name { get; set; } // Midterm, Final, etc.
    public string Description { get; set; }

    // Relationships
    public virtual ICollection<Grade> Grades { get; set; }
}
