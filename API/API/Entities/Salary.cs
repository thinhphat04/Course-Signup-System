namespace API.Entities;

public class Salary
{
    public int SalaryId { get; set; }
    public int TeacherId { get; set; }
    public DateTime MonthYear { get; set; } // Represents the month and year
    public decimal Amount { get; set; }

    // Relationships
    public Teacher Teacher { get; set; }
}
