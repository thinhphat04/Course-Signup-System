using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class EmployeeSalary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmployeeSalaryId { get; set; }

        public double Salary { get; set; }
        [StringLength(150)]
        public string? AllowanceName { get; set; } = null!;

        public double? Allowance { get; set; }

        public double SalaryReal { get; set; }
        public double TotalSalary { get; set; }
        public bool IsClose { get; set; }
        [StringLength(250)]
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserId { get; set; } = null!;
        public Teacher Teacher { get; set; }= null!;
    }

