namespace MoH_Microservice.Misc
{
    public class AppReportModel
    {
        public class PaymentReportDTO
        {
            public long RowId { get; set; }
            public string? ReferenceNo { get; set; }
            public string? Recipt { get; set; }
            public string? PatientCardNumber { get; set; }
            public string? HospitalName { get; set; }
            public string? Department { get; set; }
            public string? PaymentChannel { get; set; }
            public string? PaymentType { get; set; }
            public string? PatientName { get; set; }
            public string? PatientPhone { get; set; }
            public string? PatientAge { get; set; }
            public string? PatientAddress { get; set; }
            public string? PatientGender { get; set; }
            public DateTime? PatientVisiting { get; set; }
            public string? PatientType { get; set; }
            public string? PaymentVerifingID { get; set; }
            public string? PatientLoaction { get; set; }
            public string? PatientWorkingPlace { get; set; }
            public string? PatientWorkID { get; set; }
            public string? PatientWoreda { get; set; }
            public string? PatientKebele { get; set; }
            public string? PatientsGoth { get; set; }
            public string? PatientCBHI_ID { get; set; }
            public string? PatientReferalNo { get; set; }
            public string? PatientLetterNo { get; set; }
            public string? PatientExamination { get; set; }
            public string? PaymentReason { get; set; }
            public decimal? PaymentAmount { get; set; } // Use decimal for currency
            public string? PaymentDescription { get; set; }
            public int? PaymentIsCollected { get; set; }
            public bool? IsReversed { get; set; }
            public string? ReversedBy { get; set; }
            public DateTime? ReversedOn { get; set; }
            public string? ReversalReason { get; set; }
            public DateTime? accedentDate { get; set; }
            public string? policeName { get; set; }
            public string? policePhone { get; set; }
            public string? CarPlateNumber { get; set; }
            public string? CarCertificate { get; set; }

            public string? RegisteredBy { get; set; }
            public DateTime? RegisteredOn { get; set; }
        }
        public class PaymentSummaryReport
        {
            // Patient and General Information
            public string? ReferenceNumber { get; set; }
            public DateTime? TreatmentDate { get; set; }
            public string? CardNumber { get; set; }
            public string? Name { get; set; } // Patient Name
            public DateTime? VisitingDate { get; set; }
            public string? Age { get; set; }
            public string? Gender { get; set; }
            public string? Kebele { get; set; } // Patient's local administrative division
            public string? Goth { get; set; } // Patient's specific locality
            public string? ReferalNo { get; set; }
            public string? IDNo { get; set; } // Patient CBHI ID
            public string? PatientType { get; set; }
            public string? PaymentType { get; set; }
                         
            // Patient Wo?rk/CBHI Details
            public string? PatientWorkingPlace { get; set; }
            public string? PatientWorkID { get; set; }
            public string? CBHIProvider { get; set; } // Assuming PatientWoreda maps to this
                         
            // Accident D?etails (if applicable)
            public DateTime? AccedentDate { get; set; } // Nullable as it might not always apply
            public string? policeName { get; set; }
            public string? PolicePhone { get; set; }
            public string? CarPlateNumber { get; set; }

            // Payment Breakdowns
            public decimal? CardPaid { get; set; }
            public decimal? UnltrasoundPaid { get; set; }
            public decimal? ExaminationPaid { get; set; }
            public decimal? MedicinePaid { get; set; }
            public decimal? LaboratoryPaid { get; set; }
            public decimal? BedPaid { get; set; }
            public decimal? SurgeryPaid { get; set; }
            public decimal? Foodpaid { get; set; } // Corrected typo from "Foodpaid"
            public decimal? OtherPaid { get; set; }
            // Total Payment
            public decimal? TotalPaid { get; set; }
        }

        public class PatientViewDTO
        {
            public long? RowID { get; set; }
            public string? PatientCardNumber { get; set; } // medical registration number
            public string? PatientFirstName { get; set; }
            public string? PatientMiddleName { get; set; }
            public string? PatientLastName { get; set; }
            public string? PatientMotherName { get; set; }
            public string? PatientGender { get; set; }
            public int? PatientAge { get; set; }
            public DateTime PatientDOB { get; set; }
            public string? PatientReligion { get; set; }
            public string? PatientPlaceofbirth { get; set; }
            public string? Multiplebirth { get; set; }
            public string? Appointment { get; set; }
            public string? PatientAddress { get; set; }
            public string? PatientkinAddress { get; set; }
            public string? PatientPhoneNumber { get; set; }
            public bool? iscreadituser { get; set; }
            public bool? iscbhiuser { get; set; }
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
            public string? PatientHouseNo { get; set; }

            public string? PatientKinRegion { get; set; }
            public string? PatientKinWoreda { get; set; }
            public string? PatientKinKebele { get; set; }
            public string? PatientKinAddressDetail { get; set; }
            public string? PatientKinHouseNo { get; set; }
            public string? PatientKinPhone { get; set; }
            public string? PatientKinMobile { get; set; }

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

            public long? AccedentRecId { get; set; }
            public string? AcceedentAddress { get; set; }
            public DateTime? AccedentDate { get; set; }
            public string? PoliceName { get; set; }
            public string? PolicePhone { get; set; }
            public string? PlateNumber { get; set; }
            public string? CarCertificate { get; set; }

            public DateTime? RegisteredOn { get; set; }
            public string? RegistereddBy { get; set; }
        }

        public class AccidentGroupModelDTO
        {
            public long? AccedentRecId { get; set; }
            public DateTime? AccedentDate { get; set; }
            public string?  PatientCardNumber { get; set; }
            public string? PatientFirstName { get; set; }
            public string? PatientMiddleName { get; set; }
            public string? PatientLastName { get; set; }
            public DateTime? PatientDOB { get; set; }
            public int? PatientAge { get; set; }
            public string? AcceedentAddress { get; set; }
            public string? PlateNumber { get; set; }
            public string? CarCertificate { get; set; }
            public string? PoliceName { get; set; }
            public string? PolicePhone { get; set; }
        }
    }
}
