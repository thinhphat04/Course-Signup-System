using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities;
    public class TuitionFeePayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TuitionFeePaymentId { get; set; }

        public double Tuition { get; set; } 

        public double Discount { get; set; }// coupon

        public double Surcharge { set; get; }

        public double EffectiveChargeRate {  get; set; }

        public string? Note { get; set; } = null!;
        
        public DateTime CreateAt { get; set; }

        public int TuitionTypeId { get; set; }
        public TuitionType TuitionType { get; set; } = null!;

        public int StudentClassId { get; set; }
        public StudentClass StudentClass { get; set; }= null!;
    }

