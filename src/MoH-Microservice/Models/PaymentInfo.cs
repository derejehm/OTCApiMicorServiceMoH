using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models
{
    public class PaymentInfo
    {
        [Required(ErrorMessage ="Payment RefNo is required!")]
        public string? paymentId { get; set; }
        [Required(ErrorMessage = "User Filed is required!")]
        public string? user { get; set; }
    }

    public class PaymentbyDate
    {
        [Required(ErrorMessage = "StartDate is required!")]
        public DateTime? startDate { get; set; }

        [Required(ErrorMessage = "EndDate is required!")]
        public DateTime? endDate { get; set; }
        [Required(ErrorMessage = "User Filed is required!")]
        public string? user { get; set; }
    }



    public class PaymentDetailByCardNo
    {
        [Required(ErrorMessage ="Patient Card Number is Required")]
        public string? code { get; set; }
        [Required(ErrorMessage = "User name is Required")]
        public string? name { get; set; }
    }
    public class PaymentDetailByInstitution
    {
        [Required(ErrorMessage = "Payment RefNo is required!")]
        public string? paymentId { get; set; }
        [Required(ErrorMessage = "User Filed is required!")]
        public string? user { get; set; }
        [Required(ErrorMessage = "Hospital is required!")]
        public string? hospital {  get; set; }
    }
    public class PaymentReg
    {

        [MaxLength(100)]
        [Required(ErrorMessage = "Payment Type is required / የክፍያ አይነት ያስፈልጋል !")]
        public string? PaymentType { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Payment Type is required / የታማሚ ካርድ ቁጥር ያስፈልጋል!")]
        public string? CardNumber { get; set; }

        [MaxLength(100)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        public string? Hospital { get; set; } = string.Empty;


        [Required(ErrorMessage = "Payment Amount is required / የክፍያ መጠን ያስፈልጋል !")]
        [DataType(DataType.Currency)]
        public List<PurposeAmountMap> Amount { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "UserName Is required / ሂሳብ ያዥ ስም !")]
        public string? Createdby { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Payment Channel is required !")]
        public string? Channel { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Users Department is required !")]
        public string? Department { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Payment Verifiation ID is required !")]
        public string? PaymentVerifingID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Payment Channel is required !")]
        public string? PatientLocation { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Payment Channel is required !")]
        public string? PatientWorkingPlace { get; set; }
        public string? UserType { get; set; }
    }

    public class PaymentTypeReg
    {
        public string? type { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class PaymentTypeUpdate
    {
        public int id { get; set; }
        public string? type { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
    public class PaymentChannelReg
    {
        public string? Channel { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class PaymentChannelUpdate
    {
        public int id { get; set; }
        public string? Channel { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }=DateTime.Now;
    }
    public class PaymentPurposeReg
    {
        public string? Purpose { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class PaymentPurposeUpdate
    {
        public int id { get; set; }
        public string? Purpose { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }

    public class PaymentCollection
    {
        public string? Hospital { get; set; }
        public string? Casher { get; set; }
        public string? Type { get; set; }
        public string? Purpose { get; set; }
        public decimal? Amount { get; set; }
        
    }
    public class PaymentTypeDelete
    {
        public int id { get; set; }
        public string? deletedBy { get; set; }
    }
    public class PaymentChannelDelete
    {
        public int id { get; set; }
        public string? deletedBy { get; set; }
    }
    public class PaymentPurposeDelete
    {
        public int id { get; set; }
        public string? deletedBy { get; set; }
    }
    public class PurposeAmountMap
    {
        //public string? refno { get; set; }
        public decimal? Amount { get;set;}
        [MaxLength(200)]
        [Required(ErrorMessage = "Payment Purpose is required / የክፍያ ምከንያት ያስፈልጋል !")]
        public string? Purpose { get; set;}
    }
}
