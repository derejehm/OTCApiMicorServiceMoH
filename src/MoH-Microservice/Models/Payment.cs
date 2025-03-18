using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MoH_Microservice.Models
{
    public class Payment
    {
        public int id {  get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Payment RefNo Is required / የክፍያ መለዮ ቁጥር ያስፈልጋል !")]
        public string? RefNo { get; set; }
        
        [MaxLength(100)]
        [Required(ErrorMessage = "Payment Type is required / የክፍያ አይነት ያስፈልጋል !")]
        public string? Type { get; set; } // CASH,
        
        [MaxLength(200)]
        public string CardNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string HospitalName { get; set; } = string.Empty; // if the system is hosted with us.

        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string Department { get; set; } = string.Empty; // Card, paharmacy, bank, Hospital

        [MaxLength(100)]
        [DataType(DataType.Text)]
        [DefaultValue("In person")]
        public string Channel { get; set; } = string.Empty; // TeleBirr, MobileBanking,Other etc
                                                            // 
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [AllowNull]
        public string PaymentVerifingID { get; set; } = string.Empty; // If the payment is done using Other that cash method eg. telebirr to bank account

        [MaxLength(100)]
        [DataType(DataType.Text)]
        [DefaultValue("Unkown")]
        public string PatientLoaction { get; set; } = string.Empty; // Address such as woreda
        
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [DefaultValue("Unkown")]
        public string PatientWorkingPlace { get; set; } = string.Empty; // Address such as woreda

        [MaxLength(200)]
        [Required(ErrorMessage = "Payment Purpose is required / የክፍያ ምከንያት ያስፈልጋል !")]
        public string Purpose { get; set; } = string.Empty; // CARD, CBHI, MEDICEN, LAB
        
        [Required(ErrorMessage = "Payment Amount is required / የክፍያ መጠን ያስፈልጋል !")]
        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "UserName Is required / ሂሳብ ያዥ ስም !")]
        public string? Createdby { get; set; }

        [DefaultValue(0)]
        public int? IsCollected { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set;}= DateTime.Now;
        
    }
    public class PaymentChannel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Channel { get; set; }

        [MaxLength(100)]
        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }

    public class PaymentPurpose
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Purpose { get; set; }

        [MaxLength(100)]
        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
    public class PaymentType
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? type { get; set; }

        [MaxLength(100)]
        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }

}
