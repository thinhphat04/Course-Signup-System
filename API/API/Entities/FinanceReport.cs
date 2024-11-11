namespace API.Entities;

public class FinanceReport
{
    public int ReportId { get; set; }
    public string Type { get; set; } // Revenue, Tuition Fee, Salary Cost
    public DateTime DateCreated { get; set; }
    public string Data { get; set; } // JSON or string for report data
}
