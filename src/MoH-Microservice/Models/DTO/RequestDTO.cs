namespace MoH_Microservice.Models.DTO
{
    public class DoctorRequestDTO
    {
        public string? PatientCardNumber { get; set; } = string.Empty;// medical registration number
        public string? PatientFirstName { get; set; }=string.Empty;
        public string? PatientMiddleName { get; set; } = string.Empty;
        public string? PatientLastName { get; set; }=string.Empty;
        public string? PatientMotherName { get; set; } = string.Empty;
        public int? PatientAge { get; set; } = 0;
        public string? PatientGender { get; set; } = string.Empty;
        public string RequestGroup { get; set; }=string.Empty;
        public string? RequestedDepartment { get; set; } = string.Empty;
        public string? RequestingDepartment { get; set; } = string.Empty;
        public string? RequestedServices { get; set; } = string.Empty;
        public string? RequestedCatagory { get; set; } = string.Empty;
        public string? duration { get; set; } = string.Empty;
        public string? Instruction { get; set; } = string.Empty;
        public string? measurement { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public string? ProcedeureCount { get; set; } = string.Empty;
        public int Requeststatus { get; set; } = -2;
        public string? RequestedBy { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set;}
    }

    public class DoctorRequestDTO2
    {
        public string? PatientCardNumber { get; set; } = string.Empty;// medical registration number
        public string? PatientFirstName { get; set; } = string.Empty;
        public string? PatientMiddleName { get; set; } = string.Empty;
        public string? PatientLastName { get; set; } = string.Empty;
        public string? PatientMotherName { get; set; } = string.Empty;
        public int? PatientAge { get; set; } = 0;
        public string? PatientGender { get; set; } = string.Empty;
        public string RequestGroup { get; set; } = string.Empty;
        public string? RequestedDepartment { get; set; } = string.Empty;
        public string? RequestingDepartment { get; set; } = string.Empty;
        public List<DoctorRequestDTOSummary>? RequestedItems { get; set; }
        public string? RequestedBy { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
    }

    public class DoctorRequestDTOSummary
    {
        public long? Id { get; set; }
        public string? RequestCatagory { get; set; } = string.Empty;
        public string? RequestedServices { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public string? ProcedeureCount { get; set; } = string.Empty;
        public string? measurement { get; set; } = string.Empty;
        public string? Requeststatus { get; set; } = "Ordered";
        public string? duration { get; set;} = string.Empty;
        public string? instruction { get; set; }=string.Empty;
    }
}
