using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models
{
    public class PaymentCollectors
    {
        public long Id { get; set; }
        [Required,MaxLength(200),DataType(DataType.Text)]
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
        [Required, MaxLength(200), DataType(DataType.MultilineText),DefaultValue("Email")]
        public string ContactMethod { get; set; }
        public DateTime AssignedOn { get; set; }
        [Required, MaxLength(200), DataType(DataType.Text)]
        public string AssignedBy { get; set; }
    }

    public class PaymentCollectorsReg
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public string AssignedLocation { get; set; }
        public string AssignedAs { get; set; } // collectors or supervisor
        public string AssignedBy { get; set; }
        public string ContactMethod { get; set; }
        public string User { get;set; }
    }
    public class PaymentCollectorsGetReq
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string User { get; set; }
    }

    public class PaymentCollectorsGet
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public string AssignedLocation { get; set; }
    }

    public class PaymentCollectorsRegArray
    {
        public List<string> EmployeeID { get; set; }
        public List<string> EmployeeName { get; set; }
        public List<string> EmployeePhone { get; set; }
        public List<string> EmployeeEmail { get; set; }
        public List<string> AssignedAs { get; set; } // collectors or supervisor
        public List<string> AssignedBy { get; set; }
        public List<string> ContactMethod { get; set; }
        public string User { get; set; }
    }
}
