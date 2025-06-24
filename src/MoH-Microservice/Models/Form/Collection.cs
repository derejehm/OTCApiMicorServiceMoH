using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models.Form
{
   
    public class CollectionFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CollectionReg
    {
        public string? CollectedBy { get; set; }
        public string? CollecterID { get; set; }
        public DateTime CollectedOn { get; set; } = DateTime.Now.Date;
        public decimal CollectedAmount { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now.Date;
        public DateTime ToDate { get; set; } = DateTime.Now.Date;
        public string? Casher { get; set; }

    }
    public class CollectionByDate
    {
        [Required(ErrorMessage = "StartDate is required!")]
        public DateTime? startDate { get; set; }

        [Required(ErrorMessage = "EndDate is required!")]
        public DateTime? endDate { get; set; }
        [Required(ErrorMessage = "User Filed is required!")]
        public string? user { get; set; }
        public int isCollected { get; set; }
    }
}
