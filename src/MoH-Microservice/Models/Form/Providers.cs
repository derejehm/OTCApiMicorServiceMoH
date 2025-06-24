using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MoH_Microservice.Models.Form
{
 
    public class ProvidersMapReg
    {
        [Required]
        public string? provider { get; set; }
        [Required]
        public string? service { get; set; } // link to payment puposes

        [AllowNull]
        public string? Kebele { get; set; }

        [AllowNull]
        public string? Goth { get; set; }

        [AllowNull]
        public string? IDNo { get; set; }
        [AllowNull]
        public string? ReferalNo { get; set; }
        [AllowNull]
        public string? letterNo { get; set; }
        [AllowNull]
        [DataType(DataType.MultilineText)]
        public string? Examination { get; set; }
        public string ExpDate { get; set; } // cbhi expire date
        [Required]
        public string CardNumber { get; set; }
    }
    public class ProvidersReg
    {
        [Required, MaxLength(100)]
        public string? provider { get; set; }
        [Required, MaxLength(100)]
        public string service { get; set; } // link to payment puposes
    }
    public class ProvidersUpdate

    {
        [Required]
        public long id { get; set; }
        [Required, MaxLength(100)]
        public string? provider { get; set; }
        [Required, MaxLength(100)]
        public string service { get; set; } // link to payment puposes
    }
    public class ProvidersDelete
    {
        public long id { get; set; }
    }
    public class ProvidersParam
    {
        [Required(ErrorMessage ="patientcardnumber can't be empty")]
        public string cardnumber { get; set; }
        [Required(ErrorMessage = "paymenttype can't be empty")]
        public string paymentType { get; set; }
    }

}
