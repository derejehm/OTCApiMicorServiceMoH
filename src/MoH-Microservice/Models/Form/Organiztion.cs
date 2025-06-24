using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MoH_Microservice.Models.Form
{
    public class OrganiztionalUsersReg
    {
        public List<string>? EmployeeID { get; set; }
        public List<string>? EmployeeName { get; set; }
        public List<string>? EmployeePhone { get; set; }
        public List<string>? EmployeeEmail { get; set; }
        public bool IsExtend { get; set; }
        public string Workplace { get; set; } // Hospiatl Name
        public string UploadedBy { get; set; }
    }
    public class OrganiztionReg
    {
        public string Organization { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
    }
    public class OrganiztionUpdate
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
    public class OrganiztionDelete
    {
        public int Id { get; set; }
        public string deletedBy { get; set; }
    }

    public class OrganizationalUserGet
    {
        public string EmployeeID { get; set; }
        public string LoggedInUser { get; set; }
    }
    public class OrganiztionalUsersUpdate
    {
        public long Id { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public bool IsExtend { get; set; }
        public string Workplace { get; set; } // Hospiatl Name
        public string LoggedInUser { get; set; }
    }


}
