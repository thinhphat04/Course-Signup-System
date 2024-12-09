using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class TuitionType 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TuitionTypeId { get; set; }

        public string TuitionName { get; set; } = null!;

        public ICollection<TuitionFeePayment> TuitionFeePayments { get; set; } = new List<TuitionFeePayment>();
    }

