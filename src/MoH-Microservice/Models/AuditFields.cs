namespace MoH_Microservice.Models
{
    public class AuditFields
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
