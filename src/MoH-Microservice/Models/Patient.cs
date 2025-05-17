using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MoH_Microservice.Models
{
    public class Patient
    {
        public long Id { get; set; }
        public string MRN { get; set; } // medical registration number
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string motherName { get; set; }
        public int age { get; set; }
        public DateTime PatientDOB { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string placeofbirth { get; set; }
        public string? multiplebirth { get; set; }
        public string? appointment { get; set; }
        public string address { get; set; }
        public string kinAddress { get; set; }
        public string? phonenumber { get; set; }
        public int? iscreadituser { get; set; }
        public int? iscbhiuser { get; set; }
        public string? type { get; set; }
        public string? EmployementID { get; set; }
        public string? occupation { get; set; }
        public string? department { get; set; }
        public string? educationlevel { get; set; }
        public string? maritalstatus { get; set; }
        public string? spouseFirstName { get; set; }
        public string? spouselastName { get; set; }
        public DateTime? visitDate { get; set; }
        public string createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }
    }
    public class PatientAddress
    {
        public long id { get; set; }
        public string? MRN { get; set; }
        public string? REFMRN { get; set; }
        public string? Region { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? HouseNo { get; set; }
        public string? AddressDetail { get; set; }
        public string? Phone { get; set; }
        public int? isNextOfKin { get; set;}
        public string createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }

    }
        public class PatientReg
        {
            public string? PatientCardNumber { get; set; } // medical registration number
            public string? PatientFirstName { get; set; }
            public string? PatientMiddleName { get; set; }
            public string? PatientLastName { get; set; }
            public string? PatientMotherName { get; set; }
            public int PatientAge { get; set; }
            public DateTime PatientDOB { get; set; }
            public string PatientGender { get; set; }
            public string? PatientReligion { get; set; }
            public string? PatientPlaceofbirth { get; set; }
            public string? Multiplebirth { get; set; }
            public string? Appointment { get; set; }
            public string? PatientAddress { get; set; }
            public string? PatientkinAddress { get; set; }
            public string? PatientPhoneNumber { get; set; }
            public int? iscreadituser { get; set; }
            public int? iscbhiuser { get; set; }
            public string? PatientEmployementID { get; set; }
            public string? PatientOccupation { get; set; }
            public string? Department { get; set; }
            public string? PatientEducationlevel { get; set; }
            public string? PatientMaritalstatus { get; set; }
            public string? PatientSpouseFirstName { get; set; }
            public string? PatientSpouselastName { get; set; }
            public string? PatientRegisteredBy { get; set; }
            public DateTime? PatientVisitingDate { get; set; }

            public string? PatientRegion { get; set; }
            public string? PatientWoreda { get; set; }
            public string? PatientKebele { get; set; }
            public string? PatientHouseNo { get; set; }
            public string? PatientAddressDetail { get; set; }
            public string? PatientPhone { get; set; }

            public string? PatientKinRegion { get; set; }
            public string? PatientKinWoreda { get; set; }
            public string? PatientKinKebele { get; set; }
            public string? PatientKinHouseNo { get; set; }
            public string? PatientKinAddressDetail { get; set; }
            public string? PatientKinPhone { get; set; }

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
            public string? PatientCardNumber { get; set; } // medical registration number
            public string? PatientFirstName { get; set; }
            public string? PatientMiddleName { get; set; }
            public string? PatientLastName { get; set; }
            public string? PatientMotherName { get; set; }
            public int PatientAge { get; set; }
            public string? PatientGender { get; set; }
            public string? PatientReligion { get; set; }
            public string? PatientPlaceofbirth { get; set; }
            public string? Multiplebirth { get; set; }
            public string? Appointment { get; set; }
            public string? PatientAddress { get; set; }
            public string? PatientkinAddress { get; set; }
            public string? PatientPhoneNumber { get; set; }
            public int? iscreadituser { get; set; }
            public bool? iscbhiuserUpdated { get; set; }
            public int? iscbhiuser { get; set; }
            public string? PatientEmployementID { get; set; }
            public string? PatientOccupation { get; set; }
            public string? Department { get; set; }
            public string? PatientEducationlevel { get; set; }
            public string? PatientMaritalstatus { get; set; }
            public string? PatientSpouseFirstName { get; set; }
            public string? PatientSpouselastName { get; set; }
            public string? PatientChangedBy { get; set; }
            public DateTime? PatientVisitingDate { get; set; }

            public string? PatientRegion { get; set; }
            public string? PatientWoreda { get; set; }
            public string? PatientKebele { get; set; }
            public string? PatientAddressDetail { get; set; }
            public string? PatientPhone { get; set; }

            public string? PatientKinRegion { get; set; }
            public string? PatientKinWoreda { get; set; }
            public string? PatientKinKebele { get; set; }
            public string? PatientKinAddressDetail { get; set; }
            public string? PatientKinPhone { get; set; }
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
            public string? Cashier { get; set; }

        }

        public class PatientViewDTO
        {
            public string? PatientCardNumber { get; set; } // medical registration number
            public string? PatientFirstName { get; set; }
            public string? PatientMiddleName { get; set; }
            public string? PatientLastName { get; set; }
            public string? PatientMotherName { get; set; }
            public int PatientAge { get; set; }
            public string? PatientGender { get; set; }
            public string? PatientReligion { get; set; }
            public string? PatientPlaceofbirth { get; set; }
            public string? Multiplebirth { get; set; }
            public string? Appointment { get; set; }
            public string? PatientAddress { get; set; }
            public string? PatientkinAddress { get; set; }
            public string? PatientPhoneNumber { get; set; }
            public int? iscreadituser { get; set; }
            public int? iscbhiuser { get; set; }
            public string? PatientEmployementID { get; set; }
            public string? PatientOccupation { get; set; }
            public string? Department { get; set; }
            public string? PatientEducationlevel { get; set; }
            public string? PatientMaritalstatus { get; set; }
            public string? PatientSpouseFirstName { get; set; }
            public string? PatientSpouselastName { get; set; }
            public string? PatientRegisteredBy { get; set; }
            public DateTime? PatientVisitingDate { get; set; }

            public string? PatientRegion { get; set; }
            public string? PatientWoreda { get; set; }
            public string? PatientKebele { get; set; }
            public string? PatientAddressDetail { get; set; }
            public string? PatientPhone { get; set; }

            public string? PatientKinRegion { get; set; }
            public string? PatientKinWoreda { get; set; }
            public string? PatientKinKebele { get; set; }
            public string? PatientKinAddressDetail { get; set; }
            public string? PatientKinPhone { get; set; }
        public long? Recoredid { get; set; }
            public string? Woreda { get; set; }
            public string? Kebele { get; set; }
            public string? Goth { get; set; }
            public string? IDNo { get; set; }
            public string? ReferalNo { get; set; }
            public string? letterNo { get; set; }
            public string? Examination { get; set; }
            public string? EmployeeID { get; set; }
            public string? CreditUserName { get; set; }
            public string? CreditUserPhone { get; set; }
            public string? CreditUserEmail { get; set; }
            public string? CreditUserOrganization { get; set; }
           
            public DateTime? RegisteredOn { get; set; }
            public string? RegistereddBy { get;set; }
        }
    }
