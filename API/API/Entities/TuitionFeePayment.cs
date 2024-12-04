using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities;
    public class TuitionFeePayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TuitionFeePaymentId { get; set; }

        public double Tuition { get; set; } // học phí

        public double Discount { get; set; }// giảm giá

        public double Surcharge { set; get; }// phí phụ thu

        public double EffectiveChargeRate {  get; set; }

        public string? Note { get; set; } = null!;
        
        public DateTime CreateAt { get; set; }

        public int TuitionTypeId { get; set; }
        public TuitionType TuitionType { get; set; } = null!;

        public int StudentClassId { get; set; }
        public StudentClass StudentClass { get; set; }= null!;
    }

