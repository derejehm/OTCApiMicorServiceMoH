using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models.Form
{
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
    }
    public class PaymentCollectorsGetReq
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
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
    }
}
