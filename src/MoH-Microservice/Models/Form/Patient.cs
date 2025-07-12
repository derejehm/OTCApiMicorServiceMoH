using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
//using static MoH_Microservice.Models.AuditFields;

namespace MoH_Microservice.Models.Form
{
    public class PatientAccedentsReg
    {
        public long? id { get; set; }
        public string? PatientCardNumber { get; set; }
        public string? accAddress { get; set; }
        public string? accDate { get; set; }
        public string? policeName { get; set; }
        public string? policePhone { get; set; }
        public string? plateNumber { get; set; }
        public string? certificate { get; set; }
    }
    public class PatientRequestedServicesReg
    {

        public string? PatientCardNumber { get; set; }
        public List<int>? RequestedServices { get; set; }
        public string? purpose { get; set; }

    }
    public class PatientRequestedServicesViewAll
    {
        public bool? isComplete { get; set; }
        public string? loggedInUser { get; set; }

    }
    public class PatientRequestedServicesViewOne
    {

        public string? PatientCardNumber { get; set; }
        public string? groupID { get; set; }
        public bool isComplete { get; set; } = false;
    }
    public class PatientRequestedServicesDelete
    {
        public string? PatientCardNumber { get; set; }
        public string? groupID { get; set; }
        public string? purpose { get; set; }
    }
    public class PatientReg
    {
        public string? PatientCardNumber { get; set; } // medical registration number
        [Required(ErrorMessage = "PatientFirstName missing")]
        public string PatientFirstName { get; set; }
        [Required(ErrorMessage = "PatientMiddleName missing")]
        public string PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMotherName { get; set; }
        [Required(ErrorMessage = "PatientDOB missing")]
        public string PatientDOB { get; set; }
        [Required(ErrorMessage = "PatientGender missing")]
        public string PatientGender { get; set; }
        public string? PatientReligion { get; set; }
        public string? PatientPlaceofbirth { get; set; }
        public string? Multiplebirth { get; set; }
        [Required(ErrorMessage = "Appointment missing")]
        public string Appointment { get; set; }
        public string? PatientPhoneNumber { get; set; }
        public bool? iscreadituser { get; set; }
        public bool? iscbhiuser { get; set; }
        public string? PatientEmployementID { get; set; }
        public string? PatientOccupation { get; set; }
        [Required(ErrorMessage = "Department missing")]
        public string Department { get; set; }
        public string? PatientEducationlevel { get; set; }
        public string? PatientMaritalstatus { get; set; }
        public string? PatientSpouseFirstName { get; set; }
        public string? PatientSpouselastName { get; set; }
        [Required(ErrorMessage = "PatientRegisteredBy missing")]
        public string? PatientRegisteredBy { get; set; }
        public string? PatientVisitingDate { get; set; }
        public string? PatientType { get; set; }

        [Required(ErrorMessage = "PatientRegion missing")]
        public string PatientRegion { get; set; }

        [Required(ErrorMessage = "PatientWoreda missing")]
        public string PatientWoreda { get; set; }
        [Required(ErrorMessage = "PatientKebele missing")]
        public string PatientKebele { get; set; }
        public string? PatientHouseNo { get; set; }
        public string? PatientAddressDetail { get; set; }
        public string? PatientPhone { get; set; }
        [Required(ErrorMessage = "PatientKinRegion missing")]
        public string PatientKinRegion { get; set; }
        [Required(ErrorMessage = "PatientKinWoreda missing")]
        public string PatientKinWoreda { get; set; }
        [Required(ErrorMessage = "PatientKinKebele missing")]
        public string PatientKinKebele { get; set; }
        public string? PatientKinHouseNo { get; set; }
        public string? PatientKinAddressDetail { get; set; }
        public string? PatientKinPhone { get; set; }
        public string? PatientKinMobile { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? Goth { get; set; }
        public string? IDNo { get; set; }
        public string? ReferalNo { get; set; }
        public string? letterNo { get; set; }
        public string? Examination { get; set; }
    }
    public class PatientUpdate
    {
        [Required(ErrorMessage = "PatientCardNumber missing")]
        public string PatientCardNumber { get; set; } // medical registration number
        [Required(ErrorMessage = "PatientFirstName missing")]
        public string PatientFirstName { get; set; }
        [Required(ErrorMessage = "PatientMiddleName missing")]
        public string PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMotherName { get; set; }
        [Required(ErrorMessage = "PatientDOB missing")]
        public string PatientDOB { get; set; }
        [Required(ErrorMessage = "PatientGender missing")]
        public string PatientGender { get; set; }
        public string? PatientReligion { get; set; }
        public string? PatientPlaceofbirth { get; set; }
        public string? Multiplebirth { get; set; }
        [Required(ErrorMessage = "Appointment missing")]
        public string Appointment { get; set; }
        public string? PatientPhoneNumber { get; set; }
        public bool? iscreadituser { get; set; }
        public bool? iscbhiuserUpdated { get; set; }
        public bool? iscbhiuser { get; set; }
        public string? PatientEmployementID { get; set; }
        public string? PatientOccupation { get; set; }
        [Required(ErrorMessage = "Department missing")]
        public string Department { get; set; }
        public string? PatientEducationlevel { get; set; }
        public string? PatientMaritalstatus { get; set; }
        public string? PatientSpouseFirstName { get; set; }
        public string? PatientSpouselastName { get; set; }
        public string? PatientVisitingDate { get; set; }
        public string? PatientType { get; set; }
        [Required(ErrorMessage = "PatientRegion missing")]
        public string PatientRegion { get; set; }
        [Required(ErrorMessage = "PatientWoreda missing")]
        public string PatientWoreda { get; set; }
        [Required(ErrorMessage = "PatientKebele missing")]
        public string PatientKebele { get; set; }
        public string? PatientHouseNo { get; set; }
        public string? PatientAddressDetail { get; set; }
        public string? PatientPhone { get; set; }
        [Required(ErrorMessage = "PatientKinRegion missing")]
        public string PatientKinRegion { get; set; }
        [Required(ErrorMessage = "PatientKinWoreda missing")]
        public string PatientKinWoreda { get; set; }
        [Required(ErrorMessage = "PatientKinKebele missing")]
        public string PatientKinKebele { get; set; }
        public string? PatientKinHouseNo { get; set; }
        public string? PatientKinAddressDetail { get; set; }
        public string? PatientKinPhone { get; set; }
        public string? PatientKinMobile { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? Goth { get; set; }
        public string? IDNo { get; set; }
        public string? ReferalNo { get; set; }
        public string? letterNo { get; set; }
        public string? Examination { get; set; }
    }
    public class PatientView
    {
        public string? PatientCardNumber { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMiddleName { get; set; }
        public string? PatientPhone { get; set; }
    }
    public class PatientViewGetOne
    {
        public string PatientCardNumber { get; set; }
    }
    public class PatientReuestServicesViewDTO
    {
        public string? PatientCardNumber { get; set; } // medical registration number
        public string? PatientFirstName { get; set; }
        public string? PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMotherName { get; set; }
        public int? PatientAge { get; set; }
        public string? PatientGender { get; set; }
        public string? RequestGroup { get; set; }
        public string? RquestedServices { get; set; }
        public decimal? Price { get; set; }
        public string? RequestedReason { get; set; }
        public bool? Paid { get; set; }
        public bool? Complete { get; set; }
        public string? RequestedBy { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class PatientReuestServicesDisplayDTO
    {
        public string? PatientCardNumber { get; set; } // medical registration number
        public string? PatientFirstName { get; set; }
        public string? PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMotherName { get; set; }
        public int? PatientAge { get; set; }
        public string? PatientGender { get; set; }
        public string? RequestGroup { get; set; } = string.Empty;
        public int? NoRequestedServices { get; set; }
        public List<patientRequestServiceOut>? RquestedServices { get; set; }
        public List<patientRequestOut>? RequestedCatagories { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? RequestedReason { get; set; }
        public bool? Paid { get; set; }
        public bool? IsCompleted { get; set; }
        public string? RequestedBy { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class patientRequestOut
    {
        public string? groupID { get; set; }
        public decimal? amount { get; set; }
        public string? purpose { get; set; }
        public bool? isPaid { get; set; }
    }
    public class patientRequestServiceOut
    {
        public decimal? amount { get; set; }
        public string? catagory { get; set; }
        public string? service { get; set; }
    }
    public class NurseRequestReg
    {
        public string PatientCardNumber { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool hasMedication { get; set; }
        public string PatientCondition { get; set; }
        public List<NurseRequestRegList> Services { get; set; }
    }
    public class NurseRequestRegList
    {
        public string? Service { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
    }
    public class NurseRequestupdate : NurseRequestReg
    {
        public int id { get; set; }
    }
    public class NurseRequestdelete
    {
        public int id { get; set; }
    }
    public class NurseRequestGetOne
    {

        public string? PatientCardNumber { get; set; }
        public string? groupID { get; set; }
        public bool isComplete { get; set; }
    }
    public class NurseRequestDTO
    {
        public string? PatientCardNumber { get; set; } // medical registration number
        public string? PatientFirstName { get; set; }
        public string? PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMotherName { get; set; }
        public int? PatientAge { get; set; }
        public string? PatientGender { get; set; }
        public string? RequestGroup { get; set; } = string.Empty;
        public int? NoRequestedServices { get; set; }
        public List<RequestedServices>? RquestedServices { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? RequestedReason { get; set; }
        public bool? Paid { get; set; }
        public bool? IsCompleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class RequestedServices
    {
        public string? groupID { get; set; }
        public string? services { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
    }
    public class NurseRequestDTO2
    {
        public string? PatientCardNumber { get; set; } // medical registration number
        public string? PatientFirstName { get; set; }
        public string? PatientMiddleName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientMotherName { get; set; }
        public int? PatientAge { get; set; }
        public string? PatientGender { get; set; }
        public string RequestGroup { get; set; }
        public string? RquestedServices { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public bool? Paid { get; set; }
        public bool? Complete { get; set; }
        public string? RequestedBy { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool hasMedication { get; set; }
        public string PatientCondition { get; set; }
    }
    public class DoctorRequest
    {
        public string patientCardnumber { get; set; } = string.Empty;
        public string reqestedTo { get; set; } = string.Empty;
        public string catagory { get; set; } = string.Empty;
        public List<DoctorRequestItem> requestItems { get; set; }=new List<DoctorRequestItem>();
    }
    public class DoctorRequestItem
    {
        public string? prodedures { get; set; } = string.Empty;
        public string? prodeduresCount { get; set; } = string.Empty;
        public decimal? price { get; set; } = 0;
        public string? measurment { get; set; } = string.Empty;
        public string? duration { get; set; } = string.Empty;
        public string? instruction { get; set;} = string.Empty;
    }
}
