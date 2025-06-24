using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models.Database
{
    public class Payment : AuditFields
    {
        public long id { get; set; }

        [MaxLength(200)]
        public string RefNo { get; set; }
        public string? ReceptNo { get; set; } // Manually setted report. 
        [MaxLength(100)]
        public string Type { get; set; } // CASH,
        [MaxLength(200)]
        public string MRN { get; set; }
        [MaxLength(100)]
        public string HospitalName { get; set; } = string.Empty; // if the system is hosted with us.
        [MaxLength(100)]
        public string? Department { get; set; } = string.Empty; // Card, paharmacy, bank, Hospital

        [MaxLength(100)]
        public string? Channel { get; set; } = string.Empty; // TeleBirr, MobileBanking,Other etc                                                  // 
        [MaxLength(100)]
        public string? PaymentVerifingID { get; set; } = string.Empty; // If the payment is done using Other than cash method eg. telebirr to bank account
        [MaxLength(100)]
        public string? PatientWorkID { get; set; } = string.Empty;
        public long? CBHIID { get; set; }
        public long? AccedentID { get; set; }
        public string? groupId { get; set; } // patient requested services
        public string? NurseReqGroupId { get; set; } // Nurse requested services
        public string? pharmacygroupid { get; set; } // Nurse requested services
        [MaxLength(200)]
        public string? Purpose { get; set; } = string.Empty; // CARD, CBHI, MEDICEN, LAB [ Could be as services provided by the hospital] 
        public decimal? Amount { get; set; }
        public int? PaymentDescriptionId { get; set; }
        public string? Description { get; set; }
        public string? ReversedDescription { get; set; }
        public int? IsCollected { get; set; }
        public int? IsReversed { get; set; }
        public int? CollectionID { get; set; } // forign key from collection
        [MaxLength(200)]
        public string? Reversedby { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReversedOn { get; set; }
    }
    public class PaymentChannel : AuditFields
    {
        public int Id { get; set; }
        public string? Channel { get; set; }
    }

    public class PaymentPurpose : AuditFields
    {
        public int Id { get; set; }
        public string? shortCodes { get; set; }
        public string? group { get; set; }
        public string? subgroup { get; set; }
        public string? Purpose { get; set; }
        public decimal? Amount { get; set; }
    }
    public class PaymentTypeLimit : AuditFields
    {
        public int Id { get; set; }
        [Required]
        public int? type { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }

        [DataType(DataType.Currency)]
        public int? Time { get; set; } // in hrs
    }
    public class PaymentType : AuditFields
    {

        public int Id { get; set; }
        public string type { get; set; }
    }
    public class PaymentTypeDiscription : AuditFields
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? PaymentTypeID { get; set; }
        public string? DiscriptionId { get; set; }
        [DataType(DataType.Text)]
        public string? Discription { get; set; }
    }
}
