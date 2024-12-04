namespace API.DTO
{
    public class EmployeeSalaryDTO
    {
        public int? EmployeeSalaryId { get; set; }

        public double Salary { get; set; }

        public string? AllowanceName { get; set; } = null!;

        public double? Allowance { get; set; }

        public double SalaryReal { get; set; }

        public bool IsClose { get; set; }

        public string? Note { get; set; }

        public string UserId { get; set; } = null!;
    }
}
