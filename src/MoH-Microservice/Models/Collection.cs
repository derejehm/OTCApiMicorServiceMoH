using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models
{
    public class PCollections
    {
        public int CollectionId { get; set; }
        public string? CollectedBy { get; set; }
        public string? CollecterID { get; set; }
        public DateTime CollectedOn { get; set; }
        public decimal CollectedAmount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string?  Casher {  get; set; } // Forign key from users
    }


    public class CollectionFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CollectionReg
    {
        public string? CollectedBy { get; set; }
        public string? CollecterID { get; set; }
        public DateTime CollectedOn { get; set; }=DateTime.Now.Date;
        public decimal CollectedAmount { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now.Date;
        public DateTime ToDate { get; set; } = DateTime.Now.Date;
        public string? Casher { get; set; }

    }
    public class CollectionByDate
    {
        [Required (ErrorMessage = "StartDate is required!")]
        public DateTime? startDate { get; set; }

        [Required (ErrorMessage = "EndDate is required!")]
        public DateTime? endDate { get; set; }
        [Required (ErrorMessage = "User Filed is required!")]
        public string? user { get; set; }
        public int isCollected { get; set; }
    }
}
