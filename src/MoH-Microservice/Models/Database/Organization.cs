using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models.Database
{
    public class Organiztion : AuditFields
    {
        public long Id { get; set; }
        [Required, MaxLength(200)]
        public string Organization { get; set; }
        [Required, MaxLength(200)]
        public string Location { get; set; }
    }
    public class OrganiztionalUsers : AuditFields
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
        public string AssignedHospital { get; set; } // Hospiatl Name

        [Required, MaxLength(200), DataType(DataType.MultilineText)]
        public string WorkPlace { get; set; }
        public string UploadedBy { get; set; } 
        public DateTime UploadedOn { get; set; } = DateTime.Now;
    }

}
