namespace MoH_Microservice.Models.Database
{
    public class Patient : AuditFields

    {
        public long Id { get; set; }
        public string MRN { get; set; } // medical registration number
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string? lastName { get; set; }
        public string? motherName { get; set; }
        public DateTime PatientDOB { get; set; }
        public string gender { get; set; }
        public string? religion { get; set; }
        public string? placeofbirth { get; set; }
        public string? multiplebirth { get; set; }
        public string appointment { get; set; }
        public string? phonenumber { get; set; }
        public bool? iscreadituser { get; set; }
        public bool? iscbhiuser { get; set; }
        public long?cbhiId { get; set; }
        public long?credituser {  get; set; }
        public string? type { get; set; }
        public string? EmployementID { get; set; }
        public string? occupation { get; set; }
        public string department { get; set; }
        public string? educationlevel { get; set; }
        public string? maritalstatus { get; set; }
        public string? spouseFirstName { get; set; }
        public string? spouselastName { get; set; }
        public DateTime? visitDate { get; set; }
    }
    public class PatientAddress : AuditFields
    {
        public long id { get; set; }
        public string? MRN { get; set; }
        public string? REFMRN { get; set; }
        public string Region { get; set; }
        public string Woreda { get; set; }
        public string Kebele { get; set; }
        public string? HouseNo { get; set; }
        public string? AddressDetail { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public int? isNextOfKin { get; set; }
    }
    public class PatientRequestedServices : AuditFields
    {
        public long id { get; set; }
        public string groupId { get; set; }
        public string? MRN { get; set; }
        public int? service { get; set; }
        public string? purpose { get; set; }
        public int? isPaid { get; set; }
        public int? isComplete { get; set; } // status to check if the service has already been given
    }
    public class NurseRequest : AuditFields
    {
        public long id { get; set; }
        public string groupId { get; set; }
        public string? MRN { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool hasMedication { get; set; }
        public string PatientCondition { get; set; }
        public string? Service { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
        public int? isPaid { get; set; }
        public int? isComplete { get; set; } // status to check if the service has already been given
    }
    public class PatientAccedent : AuditFields
    {
        public long id { get; set; }
        public string? MRN { get; set; }
        public string? accedentAddress { get; set; }
        public DateTime accedentDate { get; set; }
        public string? policeName { get; set; }
        public string? policePhone { get; set; }
        public string? plateNumber { get; set; }
        public string? certificate { get; set; }

    }

    public class DoctorRequest : AuditFields
    {
        public long id { get; set; }
        public string groupId { get; set; } = string.Empty;
        public string MRN { get; set; } = string.Empty;
        public string requestTo { get; set; } = string.Empty;
        public string requestFrom { get; set; } = string.Empty;
        public string service { get; set; } = string.Empty;
        public string? count { get; set; } = string.Empty;
        public decimal price { get; set; } = 0;
        public string measurment { get; set; } = string.Empty;
        public string catagory { get; set; } = string.Empty;
        public string? duration { get; set; } = string.Empty;
        public string? instruction { get; set; } = string.Empty;
        /**
         * status
         *  0: ordered
         *  1: paid
         *  2: picked
         *  3: processed
         *  4: completed
         *  negetive status
         *  -1: equipment failed  
         *  -2: unkown
         *  -3: canceled
         * **/
        public int status { get; set; } = 0;
    }
}
