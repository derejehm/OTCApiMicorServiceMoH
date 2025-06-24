using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MoH_Microservice.Models.Database
{
    public class PaymentCollectors : AuditFields
    {
        public long Id { get; set; }
        [Required, MaxLength(200), DataType(DataType.Text)]
        public string EmployeeID { get; set; }
        [Required, MaxLength(200), DataType(DataType.Text)]
        public string EmployeeName { get; set; }
        [Required, MaxLength(200), DataType(DataType.Text)]
        public string EmployeePhone { get; set; }
        [Required, MaxLength(200), DataType(DataType.Text)]
        public string EmployeeEmail { get; set; }
        [Required, MaxLength(200), DataType(DataType.MultilineText)]
        public string AssignedLocation { get; set; } // Hospiatl Name
        [Required, MaxLength(200), DataType(DataType.MultilineText)]
        public string AssignedAs { get; set; }
        [Required, MaxLength(200), DataType(DataType.MultilineText), DefaultValue("Email")]
        public string ContactMethod { get; set; }
        public DateTime AssignedOn { get; set; }
        [Required, MaxLength(200), DataType(DataType.Text)]
        public string AssignedBy { get; set; }
    }
}
