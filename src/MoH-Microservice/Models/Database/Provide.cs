using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models.Database
{
    public class ProvidersMapUsers : AuditFields
    {
        public long Id { get; set; }
        [MaxLength(100)] public string? provider { get; set; }
        [MaxLength(100)] public string service { get; set; } // link to payment puposes
        [MaxLength(100)] public string MRN { get; set; } // link to payment puposes
        [MaxLength(100)] public string? Kebele { get; set; }
        [MaxLength(100)] public string? Goth { get; set; }
        [MaxLength(100)] public string? IDNo { get; set; }
        [MaxLength(100)] public string? ReferalNo { get; set; }
        [MaxLength(100)] public string? letterNo { get; set; }
        [MaxLength(100)] public string? Examination { get; set; }
        [MaxLength(100)] public DateTime ExpDate { get; set; } // cbhi expire date
    }
    public class Providers : AuditFields
    {
        public long Id { get; set; }
        public string? provider { get; set; }
        public string service { get; set; } // link to payment puposes
    }
}
