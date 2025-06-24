namespace MoH_Microservice.Models.Database
{
    public class PCollections : AuditFields
    {
        public int CollectionId { get; set; }
        public string? CollectedBy { get; set; }
        public string? CollecterID { get; set; }
        public DateTime CollectedOn { get; set; }
        public decimal CollectedAmount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Casher { get; set; } // Forign key from users
    }
}
