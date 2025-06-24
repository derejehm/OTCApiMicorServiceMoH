
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.DTO;
using MoH_Microservice.Models.Form;
using static MoH_Microservice.Controllers.PaymentController;
using static MoH_Microservice.Misc.AppReportModel;
namespace MoH_Microservice.Query
{
    public class AppQuery
    {
        public readonly AppDbContext _dbContext; 
        public AppQuery(AppDbContext dbContext) {
            this._dbContext = dbContext;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PatientReuestServicesViewDTO> PatientServiceQuery()
        {
            var query = (from patients in this._dbContext.Patients
                         join patinetservices in this._dbContext.PatientRequestedServices on patients.MRN equals patinetservices.MRN into _serviceMap
                         from serviceMap in _serviceMap.DefaultIfEmpty()
                         join purposes in this._dbContext.PaymentPurposes on serviceMap.service equals purposes.Id into _purposeMap
                         from purposeMap in _purposeMap.DefaultIfEmpty()
                         select new PatientReuestServicesViewDTO
                         {
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientAge = DateTime.Now.Year - (patients.PatientDOB.Year),
                             RequestGroup = serviceMap.groupId,
                             PatientGender = patients.gender,
                             RquestedServices = purposeMap.Purpose,
                             RequestedReason = serviceMap.purpose,
                             Price = purposeMap.Amount,
                             RequestedBy = serviceMap.CreatedBy,
                             createdOn = serviceMap.CreatedOn,
                             Paid = serviceMap.isPaid == 0 ? false : serviceMap.isPaid == 1 ? true : null,
                             Complete = serviceMap.isComplete == 0 ? false : serviceMap.isComplete == 1 ? true : null

                         }).AsNoTracking();

            return query;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PatientReuestServicesViewDTO> NurseRequestsQuery()
        {
            int currentYear = DateTime.Now.Year;
            var query = (from patients in this._dbContext.Patients
                         join patinetservices in this._dbContext.NurseRequests on patients.MRN equals patinetservices.MRN into _serviceMap
                         from serviceMap in _serviceMap.DefaultIfEmpty()
                         select new PatientReuestServicesViewDTO
                         {
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientGender = patients.gender,
                             PatientAge = (int)(currentYear - patients.PatientDOB.Year),
                             RequestGroup = serviceMap.groupId,
                             RquestedServices = serviceMap.Service,
                             Price = serviceMap.Price,
                             RequestedBy = serviceMap.CreatedBy,
                             createdOn = serviceMap.CreatedOn,
                             Paid = serviceMap.isPaid == 0 ? false : serviceMap.isPaid == 1 ? true : null,
                             Complete = serviceMap.isComplete == 0 ? false : serviceMap.isComplete == 1 ? true : null
                         }).AsNoTracking();

            return query;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PaymentTypeDTO> PaymentTypeQuery()
        {
            var query = (from purpose in this._dbContext.PaymentTypes
                         join paymentPurposeDiscription in this._dbContext.PaymentTypeDiscriptions on purpose.Id equals paymentPurposeDiscription.PaymentTypeID into _serviceMap
                         from serviceMap in _serviceMap.DefaultIfEmpty()
                         join paymentLimit in this._dbContext.PaymentPurposeLimits on purpose.Id equals paymentLimit.type into _paymentLimit
                         from paymentLimits in _paymentLimit.DefaultIfEmpty()
                         select new PaymentTypeDTO
                         {
                             DescriptionId = serviceMap.Id,
                             TypeId= purpose.Id,
                             Type =purpose.type,
                             PriceLimit = paymentLimits.Amount,
                             TimeLimitInHours=paymentLimits.Time,
                             Description=serviceMap.Discription,
                             CreatedOn=purpose.CreatedOn
                         }).AsNoTracking();

            return query;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<AppReportModel.PaymentReportDTO> PaymentQuery()
        {
            var query = from payments in this._dbContext.Payments
                        join patients in this._dbContext.Patients on payments.MRN equals patients.MRN into rpt_patient
                        from report in rpt_patient.DefaultIfEmpty()
                        join cbhiInfo in this._dbContext.ProvidersMapPatient on payments.CBHIID equals cbhiInfo.Id into rpt_cbhiinfo
                        from cbhiusers in rpt_cbhiinfo.DefaultIfEmpty()
                        join workers in this._dbContext.OrganiztionalUsers on payments.PatientWorkID equals workers.EmployeeID into rpt_worker
                        from report_workers in rpt_worker.DefaultIfEmpty()
                        join accendets in this._dbContext.PatientAccedents on payments.AccedentID equals accendets.id into _rpt_accedents
                        from rpt_accedents in _rpt_accedents.DefaultIfEmpty()
                        select new AppReportModel.PaymentReportDTO
                        {
                            RowId = payments.id,
                            ReferenceNo = payments.RefNo,
                            Recipt = payments.ReceptNo,
                            PatientCardNumber = payments.MRN,
                            HospitalName = payments.HospitalName,
                            Department = payments.Department,
                            PaymentChannel = payments.Channel,
                            PaymentType = payments.Type,
                            PatientName = report.firstName == null ? report_workers.EmployeeName ?? "" : report.firstName + " " + report.middleName + " " + report.lastName,
                            PatientPhone = report.phonenumber ?? report_workers.EmployeePhone ?? "",
                            PatientAge = report.PatientDOB != null ? Math.Abs(EF.Functions.DateDiffYear(report.PatientDOB, DateTime.Now)).ToString() : "",
                            PatientGender = report.gender ?? "",
                            PatientVisiting = report.visitDate,
                            PatientType = report.type ?? "",
                            PaymentVerifingID = payments.PaymentVerifingID,
                            PatientWorkingPlace = report_workers.WorkPlace,
                            PatientWorkID = payments.PatientWorkID,

                            PatientWoreda = cbhiusers.provider,
                            PatientCBHI_ID = cbhiusers.IDNo,
                            PatientKebele = cbhiusers.Kebele,
                            PatientExamination = cbhiusers.Examination,
                            PatientLetterNo = cbhiusers.letterNo,
                            PatientReferalNo = cbhiusers.ReferalNo,
                            PatientsGoth = cbhiusers.Goth,
                            PaymentReason = payments.Purpose,
                            PaymentAmount = payments.Amount,
                            PaymentDescription = payments.Description,
                            PaymentIsCollected = payments.IsCollected,
                            accedentDate = rpt_accedents.accedentDate,
                            policeName = rpt_accedents.policeName,
                            policePhone = rpt_accedents.policePhone,
                            CarPlateNumber = rpt_accedents.plateNumber,
                            CarCertificate = rpt_accedents.certificate,
                            RegisteredBy = payments.CreatedBy,
                            RegisteredOn = payments.CreatedOn.Date,
                            IsReversed = payments.IsReversed !!=1 ? false : true,
                            ReversedBy = payments.Reversedby,
                            ReversedOn = payments.ReversedOn.Value.Date,
                            ReversalReason= payments.ReversedDescription
                        };
            return query;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<NurseRequestDTO2> NurseRequesteQuery()
        {
            var query = (from patients in this._dbContext.Patients
                         join patinetservices in this._dbContext.NurseRequests on patients.MRN equals patinetservices.MRN into _serviceMap
                         from serviceMap in _serviceMap.DefaultIfEmpty()
                         select new  NurseRequestDTO2
                         {
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,
                             RequestGroup = serviceMap.groupId,
                             PatientGender = patients.gender,
                             DischargeDate= serviceMap.DischargeDate,
                             AdmissionDate = serviceMap.AdmissionDate,
                             hasMedication = serviceMap.hasMedication?true:false,
                             PatientCondition = serviceMap.PatientCondition,
                             RquestedServices = serviceMap.Service,
                             Amount = serviceMap.Amount,
                             Price = serviceMap.Price,
                             RequestedBy = serviceMap.CreatedBy,
                             createdOn = serviceMap.CreatedOn,

                             Paid = serviceMap.isPaid == 0 ? false : serviceMap.isPaid == 1 ? true : null,
                             Complete = serviceMap.isComplete == 0 ? false : serviceMap.isPaid == 1 ? true : null
                         }).AsNoTracking();

            return query;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PatientViewDTO> PatientQuery()
        {
            var query = (from patients in this._dbContext.Patients
                         join cbhiuser in this._dbContext.ProvidersMapPatient on patients.cbhiId equals cbhiuser.Id into patientMap
                         from patientMapCbhi in patientMap.DefaultIfEmpty()
                         join workers in this._dbContext.OrganiztionalUsers on patients.EmployementID equals workers.EmployeeID into _creditUsers
                         from creditUsers in _creditUsers.DefaultIfEmpty()
                         join patientAddress in this._dbContext.PatientAddress on patients.MRN equals patientAddress.MRN into _patientAddresses
                         from address in _patientAddresses.DefaultIfEmpty()
                         join patientKinAddress in this._dbContext.PatientAddress on patients.MRN equals patientKinAddress.REFMRN into _patientKinAddresses
                         from kinAddress in _patientKinAddresses.DefaultIfEmpty()
                         join patientAccedent in this._dbContext.PatientAccedents on patients.MRN equals patientAccedent.MRN into _patientAccedents
                         from patientAccedents in _patientAccedents.DefaultIfEmpty()
                         select new PatientViewDTO
                         {
                             RowID = patients.Id,
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientGender = patients.gender,
                             PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,
                             PatientDOB = patients.PatientDOB,
                             PatientReligion = patients.religion,
                             PatientPlaceofbirth = patients.placeofbirth,
                             Multiplebirth = patients.multiplebirth,
                             Appointment = patients.appointment,
                             PatientPhoneNumber = patients.phonenumber,
                             iscreadituser = patients.iscreadituser,
                             iscbhiuser = patients.iscbhiuser,
                             PatientEmployementID = patients.EmployementID,
                             PatientOccupation = patients.occupation,
                             Department = patients.department,
                             PatientEducationlevel = patients.educationlevel,
                             PatientMaritalstatus = patients.maritalstatus,
                             PatientSpouseFirstName = patients.spouseFirstName,
                             PatientSpouselastName = patients.spouselastName,
                             PatientRegisteredBy = patients.CreatedBy,
                             PatientVisitingDate = patients.visitDate,

                             PatientRegion = address.Region,
                             PatientWoreda = address.Woreda,
                             PatientKebele = address.Kebele,
                             PatientAddressDetail = address.AddressDetail,
                             PatientHouseNo = address.HouseNo,
                             PatientPhone = address.Phone,

                             PatientKinRegion = kinAddress.Region,
                             PatientKinWoreda = kinAddress.Woreda,
                             PatientKinKebele = kinAddress.Kebele,
                             PatientKinAddressDetail = kinAddress.AddressDetail,
                             PatientKinHouseNo = kinAddress.HouseNo,
                             PatientKinPhone = kinAddress.Phone,
                             PatientKinMobile = kinAddress.Mobile,

                             Recoredid = patientMapCbhi.Id,
                             Woreda = patientMapCbhi.provider,
                             Kebele = patientMapCbhi.Kebele,
                             Goth = patientMapCbhi.Goth,
                             IDNo = patientMapCbhi.IDNo,
                             ReferalNo = patientMapCbhi.ReferalNo,
                             letterNo = patientMapCbhi.letterNo,
                             Examination = patientMapCbhi.Examination,
                             EmployeeID = creditUsers.EmployeeID,
                             CreditUserName = creditUsers.EmployeeName,
                             CreditUserEmail = creditUsers.EmployeeEmail,
                             CreditUserPhone = creditUsers.EmployeePhone,
                             CreditUserOrganization = creditUsers.WorkPlace,

                             AccedentRecId = patientAccedents.id,
                             AcceedentAddress = patientAccedents.accedentAddress,
                             AccedentDate = patientAccedents.accedentDate,
                             PoliceName = patientAccedents.policeName,
                             PolicePhone = patientAccedents.policePhone,
                             PlateNumber = patientAccedents.plateNumber,
                             CarCertificate = patientAccedents.certificate,

                             RegisteredOn = patients.CreatedOn,
                             RegistereddBy = patients.CreatedBy
                         }).AsNoTracking();
            return query;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PatientViewDTO> PatientQueryCBHI()
        {
            var query = (from patients in this._dbContext.Patients
                         join cbhiuser in this._dbContext.ProvidersMapPatient on patients.cbhiId equals cbhiuser.Id into patientMap
                         from patientMapCbhi in patientMap
                         join patientAddress in this._dbContext.PatientAddress on patients.MRN equals patientAddress.MRN into _patientAddresses
                         from address in _patientAddresses.DefaultIfEmpty()
                         join patientKinAddress in this._dbContext.PatientAddress on patients.MRN equals patientKinAddress.REFMRN into _patientKinAddresses
                         from kinAddress in _patientKinAddresses.DefaultIfEmpty()
                         select new PatientViewDTO
                         {
                             RowID = patients.Id,
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientGender = patients.gender,
                             PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,
                             PatientDOB = patients.PatientDOB,
                             PatientReligion = patients.religion,
                             PatientPlaceofbirth = patients.placeofbirth,
                             Multiplebirth = patients.multiplebirth,
                             Appointment = patients.appointment,
                             PatientPhoneNumber = patients.phonenumber,
                             iscreadituser = patients.iscreadituser,
                             iscbhiuser = patients.iscbhiuser,
                             PatientEmployementID = patients.EmployementID,
                             PatientOccupation = patients.occupation,
                             Department = patients.department,
                             PatientEducationlevel = patients.educationlevel,
                             PatientMaritalstatus = patients.maritalstatus,
                             PatientSpouseFirstName = patients.spouseFirstName,
                             PatientSpouselastName = patients.spouselastName,
                             PatientRegisteredBy = patients.CreatedBy,
                             PatientVisitingDate = patients.visitDate,

                             PatientRegion = address.Region,
                             PatientWoreda = address.Woreda,
                             PatientKebele = address.Kebele,
                             PatientAddressDetail = address.AddressDetail,
                             PatientHouseNo = address.HouseNo,
                             PatientPhone = address.Phone,

                             PatientKinRegion = kinAddress.Region,
                             PatientKinWoreda = kinAddress.Woreda,
                             PatientKinKebele = kinAddress.Kebele,
                             PatientKinAddressDetail = kinAddress.AddressDetail,
                             PatientKinHouseNo = kinAddress.HouseNo,
                             PatientKinPhone = kinAddress.Phone,
                             PatientKinMobile = kinAddress.Mobile,

                             Recoredid = patientMapCbhi.Id,
                             Woreda = patientMapCbhi.provider,
                             Kebele = patientMapCbhi.Kebele,
                             Goth = patientMapCbhi.Goth,
                             IDNo = patientMapCbhi.IDNo,
                             ReferalNo = patientMapCbhi.ReferalNo,
                             letterNo = patientMapCbhi.letterNo,
                             Examination = patientMapCbhi.Examination,
                             RegisteredOn = patients.CreatedOn,
                             RegistereddBy = patients.CreatedBy
                         }).AsNoTracking();
            return query;
        }
        public async Task<IEnumerable<PatientViewDTO>> patientJoin()
        {
            var la = await this._dbContext.ProvidersMapPatient
                .GroupBy(p => p.MRN)
                .Select(s => new { latestRecord = s.Max(s => s.Id) })
                .ToArrayAsync();

            // Perform the left join
            var patientInfo = (await this.PatientQuery().ToArrayAsync()) // Start with the conceptual "right" table
                .GroupJoin(
                    la,                             // The conceptual "left" table (now used as the right part of GroupJoin)
                    p => p.Recoredid,               // Key from the PatientQuery()
                    maxid => maxid.latestRecord,    // Key from 'la'
                    (p, maxids) => new { p, maxids } // Result selector for GroupJoin
                )
                .SelectMany(
                    x => x.maxids.DefaultIfEmpty(), // Project each maxid, or null if no match from 'la'
                    (x, maxid) => x.p // Combine both sides. maxid will be null if no match.
                );
            return patientInfo;

        }

        public IQueryable<ReportDTO> Report()
        {
            var query = (from report in this._dbContext.Reports
                         join reportfilter in this._dbContext.ReportFilters on report.uuid equals reportfilter.uuid into _reportWithFilter
                         from reportWithFilter in _reportWithFilter.DefaultIfEmpty()
                         join reportAccess in this._dbContext.ReportAccess on report.uuid equals reportAccess.Report into _reportWithAccess
                         from reportWithAccess in _reportWithAccess.DefaultIfEmpty()
                         select new ReportDTO
                         {
                             uuid = report.uuid ?? "",
                             ReportTitle = report.title ?? "",
                             ReportDescription = report.description ?? "",
                             ReportSummary = report.summary ?? "",
                             ReportCategory = report.category ?? "",
                             ReportPublisher = report.publisher ?? "",
                             ReportSource = report.source ?? "",
                             ReportColumns = report.Columns??"",
                             ReportFilters = report.filters ?? "",
                             datatype = reportWithFilter != null ? reportWithFilter.datatype ?? "" : "",
                             conditions = reportWithFilter != null ? reportWithFilter.conditions ?? "" : "",
                             Accessable = reportWithAccess.enabled !=null ? reportWithAccess.enabled:false,
                             grouped = report.grouped ,
                             enableCount = report.enableCount ,
                             Reportusers = reportWithAccess.users!=null? reportWithAccess.users:"",
                             Allowed = reportWithAccess.enabled !=null?reportWithAccess.enabled:false,
                             ReportCreatedBy = report.CreatedBy ?? "",
                             ReportCreatedOn = report.CreatedOn
                         }).AsNoTracking();

            return query;
        }
        public IQueryable<ReportStoreDTO> ReportStore()
        {
            var query = (from report in this._dbContext.Reports
                         join reportfilter in this._dbContext.ReportStore on report.uuid equals reportfilter.Report into _reportWithFilter
                         from reportWithStore in _reportWithFilter.DefaultIfEmpty()
                         select new ReportStoreDTO
                         {
                             uuid = report.uuid,
                             ReportTitle = report.title,
                             ReportDescription = report.description,
                             ReportSummary = report.summary,
                             ReportCategory = report.category,
                             ReportPublisher = report.publisher,
                             users = reportWithStore.users != null ? reportWithStore.users : ""
                         }).AsNoTracking();

            return query;
        }

        public IQueryable<ReportAccessDTO> ReportAccess()
        {
            var query = (from report in this._dbContext.Reports
                         join reportfilter in this._dbContext.ReportAccess on report.uuid equals reportfilter.Report into _reportWithAccess
                         from reportWithAcess in _reportWithAccess.DefaultIfEmpty()
                         select new ReportAccessDTO
                         {
                             uuid = report.uuid,
                             ReportTitle = report.title,
                             ReportDescription = report.description,
                             ReportSummary = report.summary,
                             ReportCategory = report.category,
                             ReportPublisher = report.publisher,
                             AllowedFor = reportWithAcess.users != null ? reportWithAcess.users : ""

                         }).AsNoTracking();

            return query;
        }


        public IQueryable<DoctorRequestDTO> DoctorRequesteQuery()
        {
            var query = (from patients in this._dbContext.Patients
                         join patinetservices in this._dbContext.DoctorRequests on patients.MRN equals patinetservices.MRN into _serviceMap
                         from serviceMap in _serviceMap
                         select new DoctorRequestDTO
                         {
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,

                             RequestGroup = serviceMap.groupId,
                             PatientGender = patients.gender,
                             RequestedServices = serviceMap.service,
                             RequestedCatagory = serviceMap.catagory,
                             duration = serviceMap.duration,
                             Instruction = serviceMap.instruction,
                             measurement = serviceMap.measurment,
                             RequestedDepartment = serviceMap.requestTo,
                             RequestingDepartment = serviceMap.requestFrom,
                             ProcedeureCount = serviceMap.count,
                             Price = serviceMap.price,
                             RequestedBy = serviceMap.CreatedBy,
                             CreatedOn = serviceMap.CreatedOn,
                             Requeststatus = serviceMap.status,
                         }).AsNoTracking();

            return query;
        }
        public IQueryable<DoctorRequestDTO> DoctorRequesteQueryPaid()
        {
            var query = (from patients in this._dbContext.Patients
                         join patinetservices in this._dbContext.DoctorRequests on patients.MRN equals patinetservices.MRN into _serviceMap
                         from serviceMap in _serviceMap
                         join payments  in this._dbContext.Payments on serviceMap.groupId equals payments.pharmacygroupid into _servicePayment
                         from payment in _servicePayment
                         select new DoctorRequestDTO
                         {
                             PatientCardNumber = patients.MRN,
                             PatientFirstName = patients.firstName,
                             PatientMiddleName = patients.middleName,
                             PatientLastName = patients.lastName,
                             PatientMotherName = patients.motherName,
                             PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,

                             RequestGroup = serviceMap.groupId,
                             PatientGender = patients.gender,
                             RequestedServices = serviceMap.service,
                             RequestedCatagory = serviceMap.catagory,
                             duration = serviceMap.duration,
                             Instruction = serviceMap.instruction,
                             measurement = serviceMap.measurment,
                             RequestedDepartment = serviceMap.requestTo,
                             RequestingDepartment = serviceMap.requestFrom,
                             ProcedeureCount = serviceMap.count,
                             Price = serviceMap.price,
                             RequestedBy = serviceMap.CreatedBy,
                             CreatedOn = serviceMap.CreatedOn,
                             Requeststatus = serviceMap.status,
                         }).AsNoTracking();

            return query;
        }

        public async Task<DoctorRequestDTO2[]> DoctorRequestSummary()
        {
            var request = await this.DoctorRequesteQuery().GroupBy(g => new
            {
                g.PatientCardNumber,
                g.PatientFirstName,
                g.PatientLastName,
                g.PatientMotherName,
                g.PatientAge,
                g.RequestGroup,
                g.RequestedDepartment,
                g.RequestingDepartment,
                g.RequestedBy,
                g.CreatedOn.Value.Date,
            }).Select(s => new DoctorRequestDTO2
            {
                PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                PatientFirstName = s.FirstOrDefault().PatientFirstName,
                PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                PatientLastName = s.FirstOrDefault().PatientLastName,
                PatientMotherName = s.FirstOrDefault().PatientMotherName,
                PatientAge = s.FirstOrDefault().PatientAge,
                PatientGender = s.FirstOrDefault().PatientGender,
                RequestGroup = s.FirstOrDefault().RequestGroup,
                RequestedDepartment = s.FirstOrDefault().RequestedDepartment,
                RequestingDepartment = s.FirstOrDefault().RequestingDepartment,
                RequestedItems = s.Select(sin =>
                new DoctorRequestDTOSummary
                {
                    RequestCatagory = sin.RequestedCatagory,
                    RequestedServices = sin.RequestedServices,
                    Price = sin.Price,
                    ProcedeureCount = sin.ProcedeureCount,
                    duration = sin.duration,
                    instruction = sin.Instruction,
                    measurement = sin.measurement,
                    Requeststatus = sin.Requeststatus == 0 ? "Ordered" :
                                        sin.Requeststatus == 1 ? "Paid" :
                                        sin.Requeststatus == 2 ? "Picked for Proccessing" :
                                        sin.Requeststatus == 3 ? "Proccessing" :
                                        sin.Requeststatus == 4 ? "Completed" :
                                        sin.Requeststatus == 0 ? "Failed" : 
                                        sin.Requeststatus == -3 ? "Canceled" : "Unkown"
                }).ToList(),
                RequestedBy = s.FirstOrDefault().RequestedBy,
                CreatedOn = s.FirstOrDefault().CreatedOn,
            }).ToArrayAsync();


            return request;
        }

        public async Task<DoctorRequestDTO2[]> DoctorRequestSummary_Pharma()
        {
            var request = await this.DoctorRequesteQuery()
            .Where(w => w.Requeststatus == 0 && w.RequestedDepartment.ToLower() == "pharmacy")
            .GroupBy(g => new
            {
                g.PatientCardNumber,
                g.PatientFirstName,
                g.PatientLastName,
                g.PatientMotherName,
                g.PatientAge,
                g.RequestGroup,
                g.RequestedDepartment,
                g.RequestingDepartment,
                g.RequestedBy,
                g.CreatedOn.Value.Date,
            }).Select(s => new DoctorRequestDTO2
            {
                PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                PatientFirstName = s.FirstOrDefault().PatientFirstName,
                PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                PatientLastName = s.FirstOrDefault().PatientLastName,
                PatientMotherName = s.FirstOrDefault().PatientMotherName,
                PatientAge = s.FirstOrDefault().PatientAge,
                PatientGender = s.FirstOrDefault().PatientGender,
                RequestGroup = s.FirstOrDefault().RequestGroup,
                RequestedDepartment = s.FirstOrDefault().RequestedDepartment,
                RequestingDepartment = s.FirstOrDefault().RequestingDepartment,
                RequestedItems = s.Select(sin =>
                new DoctorRequestDTOSummary
                {
                    RequestCatagory = sin.RequestedCatagory,
                    RequestedServices = sin.RequestedServices,
                    Price = sin.Price,
                    ProcedeureCount = sin.ProcedeureCount,
                    duration = sin.duration,
                    instruction = sin.Instruction,
                    measurement = sin.measurement,
                    Requeststatus = sin.Requeststatus == 0 ? "Ordered" :
                                        sin.Requeststatus == 1 ? "Paid" :
                                        sin.Requeststatus == 2 ? "Picked for Proccessing" :
                                        sin.Requeststatus == 3 ? "Proccessing" :
                                        sin.Requeststatus == 4 ? "Completed" :
                                        sin.Requeststatus < 0 ? "Failed" : "Unkown"
                }).ToList(),
                RequestedBy = s.FirstOrDefault().RequestedBy,
                CreatedOn = s.FirstOrDefault().CreatedOn,
            }).ToArrayAsync();


            return request;
        }
        public async Task<DoctorRequestDTO2[]> DoctorRequestSummary_Pharma_paid()
        {
            var request = await this.DoctorRequesteQueryPaid()
            .Where(w => w.Requeststatus == 1 && w.RequestedDepartment.ToLower().Contains("pharmacy"))
            .GroupBy(g => new
            {
                g.PatientCardNumber,
                g.PatientFirstName,
                g.PatientLastName,
                g.PatientMotherName,
                g.PatientAge,
                g.RequestGroup,
                g.RequestedDepartment,
                g.RequestingDepartment,
                g.RequestedBy,
                g.CreatedOn.Value.Date,
            }).Select(s => new DoctorRequestDTO2
            {
                PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                PatientFirstName = s.FirstOrDefault().PatientFirstName,
                PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                PatientLastName = s.FirstOrDefault().PatientLastName,
                PatientMotherName = s.FirstOrDefault().PatientMotherName,
                PatientAge = s.FirstOrDefault().PatientAge,
                PatientGender = s.FirstOrDefault().PatientGender,
                RequestGroup = s.FirstOrDefault().RequestGroup,
                RequestedDepartment = s.FirstOrDefault().RequestedDepartment,
                RequestingDepartment = s.FirstOrDefault().RequestingDepartment,
                RequestedItems = s.Select(sin =>
                new DoctorRequestDTOSummary
                {
                    RequestCatagory = sin.RequestedCatagory,
                    RequestedServices = sin.RequestedServices,
                    Price = sin.Price,
                    ProcedeureCount = sin.ProcedeureCount,
                    duration = sin.duration,
                    instruction = sin.Instruction,
                    measurement = sin.measurement,
                    Requeststatus = sin.Requeststatus == 0 ? "Ordered" :
                                        sin.Requeststatus == 1 ? "Paid" :
                                        sin.Requeststatus == 2 ? "Picked for Proccessing" :
                                        sin.Requeststatus == 3 ? "Proccessing" :
                                        sin.Requeststatus == 4 ? "Completed" :
                                        sin.Requeststatus < 0 ? "Failed" : "Unkown"
                }).ToList(),
                RequestedBy = s.FirstOrDefault().RequestedBy,
                CreatedOn = s.FirstOrDefault().CreatedOn,
            }).ToArrayAsync();
            return request;
        }
        public async Task<DoctorRequestDTO2[]> DoctorRequestSummary_lab_paid()
        {
            var request = await this.DoctorRequesteQueryPaid()
            .Where(w => w.Requeststatus == 1 && w.RequestedDepartment.ToLower().Contains("lab"))
            .GroupBy(g => new
            {
                g.PatientCardNumber,
                g.PatientFirstName,
                g.PatientLastName,
                g.PatientMotherName,
                g.PatientAge,
                g.RequestGroup,
                g.RequestedDepartment,
                g.RequestingDepartment,
                g.RequestedBy,
                g.CreatedOn.Value.Date,
            }).Select(s => new DoctorRequestDTO2
            {
                PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                PatientFirstName = s.FirstOrDefault().PatientFirstName,
                PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                PatientLastName = s.FirstOrDefault().PatientLastName,
                PatientMotherName = s.FirstOrDefault().PatientMotherName,
                PatientAge = s.FirstOrDefault().PatientAge,
                PatientGender = s.FirstOrDefault().PatientGender,
                RequestGroup = s.FirstOrDefault().RequestGroup,
                RequestedDepartment = s.FirstOrDefault().RequestedDepartment,
                RequestingDepartment = s.FirstOrDefault().RequestingDepartment,
                RequestedItems = s.Select(sin =>
                new DoctorRequestDTOSummary
                {
                    RequestCatagory = sin.RequestedCatagory,
                    RequestedServices = sin.RequestedServices,
                    Price = sin.Price,
                    ProcedeureCount = sin.ProcedeureCount,
                    duration = sin.duration,
                    instruction = sin.Instruction,
                    measurement = sin.measurement,
                    Requeststatus = sin.Requeststatus == 0 ? "Ordered" :
                                        sin.Requeststatus == 1 ? "Paid" :
                                        sin.Requeststatus == 2 ? "Picked for Proccessing" :
                                        sin.Requeststatus == 3 ? "Proccessing" :
                                        sin.Requeststatus == 4 ? "Completed" :
                                        sin.Requeststatus < 0 ? "Failed" : "Unkown"
                }).ToList(),
                RequestedBy = s.FirstOrDefault().RequestedBy,
                CreatedOn = s.FirstOrDefault().CreatedOn,
            }).ToArrayAsync();
            return request;
        }
        public async Task<DoctorRequestDTO2[]> DoctorRequestSummary(string requestedBy)
        {
            var request = await this.DoctorRequesteQuery()
                .Where(w => w.RequestingDepartment.ToLower() == requestedBy.ToLower())
                .GroupBy(g => new
                {
                    g.PatientCardNumber,
                    g.PatientFirstName,
                    g.PatientLastName,
                    g.PatientMotherName,
                    g.PatientAge,
                    g.RequestGroup,
                    g.RequestedDepartment,
                    g.RequestingDepartment,
                    g.RequestedBy,
                    g.CreatedOn.Value.Date,
                }).Select(s => new DoctorRequestDTO2
                {
                    PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                    PatientFirstName = s.FirstOrDefault().PatientFirstName,
                    PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                    PatientLastName = s.FirstOrDefault().PatientLastName,
                    PatientMotherName = s.FirstOrDefault().PatientMotherName,
                    PatientAge = s.FirstOrDefault().PatientAge,
                    PatientGender = s.FirstOrDefault().PatientGender,
                    RequestGroup = s.FirstOrDefault().RequestGroup,
                    RequestedDepartment = s.FirstOrDefault().RequestedDepartment,
                    RequestingDepartment = s.FirstOrDefault().RequestingDepartment,
                    RequestedItems = s.Select(sin =>
                    new DoctorRequestDTOSummary
                    {
                        RequestCatagory = sin.RequestedCatagory,
                        RequestedServices = sin.RequestedServices,
                        Price = sin.Price,
                        duration = sin.duration,
                        instruction = sin.Instruction,
                        ProcedeureCount = sin.ProcedeureCount,
                        measurement = sin.measurement,
                        Requeststatus = sin.Requeststatus == 0 ? "Ordered" :
                                        sin.Requeststatus == 1 ? "Paid" :
                                        sin.Requeststatus == 2 ? "Picked for Proccessing" :
                                        sin.Requeststatus == 3 ? "Proccessing" :
                                        sin.Requeststatus == 4 ? "Completed" :
                                        sin.Requeststatus < 0 ? "Failed" : "Unkown"
                    }).ToList(),
                    RequestedBy = s.FirstOrDefault().RequestedBy,
                    CreatedOn = s.FirstOrDefault().CreatedOn,
                }).ToArrayAsync();


            return request;
        }
        public async Task<DoctorRequestDTO2[]> DoctorRequestSummaryPayable(int status)
        {
            var request = await this.DoctorRequesteQuery()
                .Where(w => w.Requeststatus == status)
                .GroupBy(g => new
                {
                    g.PatientCardNumber,
                    g.PatientFirstName,
                    g.PatientLastName,
                    g.PatientMotherName,
                    g.PatientAge,
                    g.RequestGroup,
                    g.RequestedDepartment,
                    g.RequestingDepartment,
                    g.RequestedBy,
                    g.CreatedOn.Value.Date,
                }).Select(s => new DoctorRequestDTO2
                {
                    PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                    PatientFirstName = s.FirstOrDefault().PatientFirstName,
                    PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                    PatientLastName = s.FirstOrDefault().PatientLastName,
                    PatientMotherName = s.FirstOrDefault().PatientMotherName,
                    PatientAge = s.FirstOrDefault().PatientAge,
                    PatientGender = s.FirstOrDefault().PatientGender,
                    RequestGroup = s.FirstOrDefault().RequestGroup,
                    RequestedDepartment = s.FirstOrDefault().RequestedDepartment,
                    RequestingDepartment = s.FirstOrDefault().RequestingDepartment,
                    RequestedItems = s.Select(sin =>
                    new DoctorRequestDTOSummary
                    {
                        RequestCatagory = sin.RequestedCatagory,
                        RequestedServices = sin.RequestedServices,
                        Price = sin.Price,
                        duration = sin.duration,
                        instruction = sin.Instruction,
                        ProcedeureCount = sin.ProcedeureCount,
                        measurement = sin.measurement,
                        Requeststatus = sin.Requeststatus == 0 ? "Ordered" :
                                        sin.Requeststatus == 1 ? "Paid" :
                                        sin.Requeststatus == 2 ? "Picked for Proccessing" : 
                                        sin.Requeststatus == 3 ? "Proccessing" :
                                        sin.Requeststatus == 4 ? "Completed" :
                                        sin.Requeststatus <0 ? "Failed":"Unkown"

                    }).ToList(),
                    RequestedBy = s.FirstOrDefault().RequestedBy,
                    CreatedOn = s.FirstOrDefault().CreatedOn,
                }).ToArrayAsync();


            return request;
        }
    }
}
