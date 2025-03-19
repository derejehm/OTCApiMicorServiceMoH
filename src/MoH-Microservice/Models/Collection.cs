namespace MoH_Microservice.Models
{
    public class PCollections
    {
        public int CollectionId { get; set; }
        public string? CollectedBy { get; set; }
        public DateTime CollectedOn { get; set; }
        public decimal CollectedAmount { get; set; }
        public string  Casher {  get; set; } // Forign key from users
    }


    public class CollectionFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
