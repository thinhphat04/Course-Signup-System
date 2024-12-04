namespace API.Entities;
    public class GradeColumn
    {
        public int Id { get; set; }
        public double Score { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; } = null!;
    }

