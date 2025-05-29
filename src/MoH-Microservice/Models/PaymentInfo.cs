using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models
{
    public class PaymentInfo
    {
        [Required(ErrorMessage = "Payment RefNo is required!")]
        public string? paymentId { get; set; }
    }

    public class PaymentbyDate
    {
        [Required(ErrorMessage = "StartDate is required!")]
        public DateTime? startDate { get; set; }

        [Required(ErrorMessage = "EndDate is required!")]
        public DateTime? endDate { get; set; }
    }

    public class PaymentDetailByCardNo
    {
        [Required(ErrorMessage = "Patient Card Number is Required")]
        public string? PatientCardNumber { get; set; }
    }
    public class PaymentDetailByPhone
    {
        [Required(ErrorMessage = "Patient Card Number is Required")]
        public string? phone { get; set; }
    }

    public class PaymentDetailByName
    {
        [Required(ErrorMessage = "Patient Card Number is Required")]
        public string? patient { get; set; }
    }
        public class PaymentDetailByInstitution
        {
            [Required(ErrorMessage = "Payment RefNo is required!")]
            public string? paymentId { get; set; }
            [Required(ErrorMessage = "Hospital is required!")]
            public string? hospital { get; set; }
        }
        public class PaymentReg
        {

            [MaxLength(100)]
            [Required(ErrorMessage = "Payment Type is required / የክፍያ አይነት ያስፈልጋል !")]
            public string PaymentType { get; set; } = string.Empty;

            [MaxLength(200)]
            [Required(ErrorMessage = "Patient Cardnumber is required / የታማሚ ካርድ ቁጥር ያስፈልጋል!")]
            public string CardNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "Payment Amount is required / የክፍያ መጠን ያስፈልጋል !")]
            [DataType(DataType.Currency)]
            public List<PurposeAmountMap> Amount { get; set; }
            public string? Description { get; set; } = string.Empty;
            public string? Channel { get; set; } = string.Empty;
            public string? organization { get; set; } = string.Empty;
            public string? PaymentVerifingID { get; set; } = string.Empty;
            public string? PatientWorkID { get; set; } = string.Empty;
            public string? PaymentRefNo { get; set; } = string.Empty;
        }

        public class PaymentTypeReg
        {
            public string? type { get; set; }
        }

        public class PaymentTypeUpdate
        {
            public int id { get; set; }
            public string? type { get; set; }
            public DateTime UpdatedOn { get; set; } = DateTime.Now;
        }
        public class PaymentChannelReg
        {
            public string? Channel { get; set; }
        }
        public class PaymentChannelUpdate
        {
            public int id { get; set; }
            public string? Channel { get; set; }
            public DateTime UpdatedOn { get; set; } = DateTime.Now;
        }
        public class PaymentPurposeReg
        {
            public List<string>? Purpose { get; set; }
            public List<decimal>? Amount { get; set; }
        }
        public class PaymentPurposeUpdate
        {
            public int id { get; set; }
            public string? Purpose { get; set; }
            public DateTime UpdatedOn { get; set; } = DateTime.Now;
        }

        public class PaymentCollection
        {
            public string? Hospital { get; set; }
            public string Casher { get; set; }
            public string? Type { get; set; }
            public string? Purpose { get; set; }
            public decimal? Amount { get; set; }

        }
        public class PaymentTypeDelete
        {
            public int id { get; set; }
        }
        public class PaymentChannelDelete
        {
            public int id { get; set; }
        }
        public class PaymentPurposeDelete
        {
            public int id { get; set; }
        }
        public class PurposeAmountMap
        {
            public decimal? Amount { get; set; }
            [MaxLength(200)]
            [Required(ErrorMessage = "Payment Purpose is required / የክፍያ ምከንያት ያስፈልጋል !")]
            public string? Purpose { get; set; }
            public string? groupID { get; set; }
            public bool? isPaid { get; set; }
        }
    }
