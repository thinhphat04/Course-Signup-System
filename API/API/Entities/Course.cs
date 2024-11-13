namespace API.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Fee { get; set; }
    public string Promotion { get; set; }

    // Relationships
    public ICollection<Class> Classes { get; set; }
}
