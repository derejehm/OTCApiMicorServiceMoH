using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Models;
using System.Drawing;
using System.Net.NetworkInformation;
using static MoH_Microservice.Models.PatientAddress;

namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _dbContext;

        public PatientController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext payment)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = payment;
        }

        [HttpPost("add-patient-info")]
        public async Task<IActionResult> addGetPatientInfo([FromBody] PatientReg patient)
        {
            try
            {
                    var user = await this._userManager.FindByNameAsync(patient.PatientRegisteredBy);
                if (user == null)
                    return NotFound("User not found");
                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length > 0)
                {
                    return BadRequest(new { msg = "Patient information exisits." });
                }
            
                Patient Patient = new Patient
                {
                    MRN = patient?.PatientCardNumber,
                    firstName = patient.PatientFirstName,
                    middleName = patient.PatientMiddleName,
                    lastName = patient?.PatientLastName,
                    motherName = patient?.PatientMotherName,
                    PatientDOB = Convert.ToDateTime(patient.PatientDOB),// DateTime.ParseExact(patient.PatientDOB.ToString(), "yyyy-MMM-dd  HH:mm:ss 'GMT'K", null),
                    gender = patient?.PatientGender,
                    religion = patient?.PatientReligion,
                    placeofbirth = patient?.PatientPlaceofbirth,
                    multiplebirth = patient?.Multiplebirth,
                    appointment = patient?.Appointment,
                    phonenumber = patient?.PatientPhoneNumber,
                    iscreadituser = patient?.iscreadituser,
                    iscbhiuser = patient?.iscbhiuser,
                    EmployementID = patient?.iscreadituser == 1 ? patient?.PatientEmployementID : null,
                    occupation = patient?.PatientOccupation,
                    department = patient?.Department,
                    educationlevel = patient?.PatientEducationlevel,
                    maritalstatus = patient?.PatientMaritalstatus,
                    spouseFirstName = patient?.PatientSpouseFirstName,
                    spouselastName = patient?.PatientSpouselastName,
                    createdBy = patient?.PatientRegisteredBy,
                    type = patient?.PatientType,
                    visitDate = patient?.PatientVisitingDate,
                    createdOn = DateTime.Now.Date,
                    updatedBy = null,
                    updatedOn = null
                };
                PatientAddress patientAddress = new PatientAddress
                {
                    MRN = patient?.PatientCardNumber,
                    Region = patient?.PatientRegion,
                    Woreda = patient?.PatientWoreda,
                    Kebele = patient?.PatientKebele,
                    HouseNo=patient?.PatientHouseNo,
                    Mobile=patient?.PatientPhoneNumber,
                    AddressDetail = patient?.PatientAddressDetail,
                    Phone = patient?.PatientPhone,
                    createdBy = patient?.PatientRegisteredBy,
                    createdOn = DateTime.Now.Date,
                };
                
                PatientAddress patientKinAddress = new PatientAddress
                {
                    REFMRN = patient?.PatientCardNumber,
                    Region = patient?.PatientKinRegion,
                    Woreda = patient?.PatientKinWoreda,
                    Kebele = patient?.PatientKinKebele,
                    HouseNo= patient?.PatientKinHouseNo,
                    Mobile= patient?.PatientKinMobile,
                    AddressDetail = patient?.PatientKinAddressDetail,
                    Phone = patient?.PatientKinPhone,
                    isNextOfKin=1,
                    createdBy=patient?.PatientRegisteredBy,
                    createdOn=DateTime.Now.Date,
                };

                

                if (patient.iscbhiuser == 1)
                {
                    ProvidersMapUsers provider = new ProvidersMapUsers
                    {
                        MRN = Patient.MRN,
                        provider = patient.Woreda,
                        Kebele = patient.Kebele,
                        Goth = patient.Goth,
                        IDNo = patient.IDNo,
                        letterNo = patient.letterNo,
                        Examination = patient.Examination,
                        service = "CBHI",
                        Createdby = patient.PatientRegisteredBy,
                        CreatedOn = DateTime.Now,
                        ReferalNo = patient.ReferalNo,

                    };
                    // if the user is a CBHI User
                    await this._dbContext.AddAsync<ProvidersMapUsers>(provider);
                    await this._dbContext.SaveChangesAsync();
                }
                else
                {
                    patient.Woreda = patient.Kebele = patient.IDNo = patient.letterNo = patient.Examination = patient.Woreda= patient.ReferalNo = null;
                }
                await this._dbContext.AddAsync(Patient);
                await this._dbContext.AddAsync(patientAddress);
                await this._dbContext.AddAsync(patientKinAddress);
                await this._dbContext.SaveChangesAsync();

                return Created("/", patient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg=$"Error: Insert PatientData failed! Reason: {ex.Message}" });
            }
        }
        [HttpPost("add-patient-request")]
        public async Task<IActionResult> addPatientRequestInfo([FromBody] PatientRequestedServicesReg PatientRequest)
        {
            try
            {
                var user = await this._userManager.FindByNameAsync(PatientRequest.createdBy);
                if (user == null)
                    return NotFound("User not found");
                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == PatientRequest.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                    return BadRequest(new { msg = "Patient information does not exisits." });
                }
                
                var groupid =  $"{user.UserType.ToLower()}-{Guid.NewGuid()}"; // groupid for identification purposes

                foreach (var service in PatientRequest.RequestedServices)
                {
                    PatientRequestedServices services = new PatientRequestedServices
                    {
                        groupId = groupid,
                        MRN = PatientRequest.PatientCardNumber,
                        service = service,
                        purpose = PatientRequest.purpose,
                        createdBy = PatientRequest.createdBy,
                        createdOn = DateTime.Now,
                        isPaid = 0,
                        isComplete=0,
                        updatedBy = null,
                        updatedOn = null
                    };
                    await this._dbContext.PatientRequestedServices.AddAsync(services);
                }
                await this._dbContext.SaveChangesAsync();

                return Ok(new { data= PatientRequest ,groupid=groupid});
            }
            catch (Exception ex)
            {
                return BadRequest($" Requesting services to patient failed : {ex.StackTrace}");
            }
        }
        [HttpPut("get-patient-request")]
        public async Task<IActionResult> getOnePatientRequestInfo([FromBody] PatientRequestedServicesViewOne patient)
        {
            try
            {
                var user = await this._userManager.FindByNameAsync(patient.loggedInUser);
                if (user == null)
                    return NotFound("User not found");

                var patientServ = this
                                .PatientServiceQuery()
                                .Where(e => e.Complete == patient.isComplete)
                                .GroupBy(e => new
                                {
                                    e.PatientCardNumber,
                                    e.RequestGroup,
                                    e.Paid,
                                    e.Complete,
                                    e.PatientFirstName,
                                    e.PatientLastName,
                                    e.PatientMiddleName,
                                    e.PatientAge,
                                    e.PatientGender,
                                    e.RequestedReason,
                                    e.RequestedBy,
                                    e.createdOn.Date
                                }).Select(s => new PatientReuestServicesDisplayDTO
                                {
                                    RequestGroup = s.FirstOrDefault().RequestGroup,
                                    PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                                    PatientFirstName = s.FirstOrDefault().PatientFirstName,
                                    PatientLastName = s.FirstOrDefault().PatientLastName,
                                    PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                                    PatientAge = s.FirstOrDefault().PatientAge,
                                    PatientGender = s.FirstOrDefault().PatientGender,
                                    Paid = s.FirstOrDefault().Paid,
                                    IsCompleted = s.FirstOrDefault().Complete,
                                    TotalPrice = s.Sum(s => s.Price),
                                    NoRequestedServices = s.Select(s => s.RquestedServices).Count(),
                                    RquestedServices = s.Select(s => new patientRequestServiceOut { service = s.RquestedServices, amount = s.Price }).ToList(),
                                    RequestedReason = s.FirstOrDefault().RequestedReason,
                                    RequestedBy = s.FirstOrDefault().RequestedBy,
                                    createdOn = s.FirstOrDefault().createdOn.Date

                                })
                .OrderByDescending(o => o.Paid);

                var result = await patientServ.Where(e => e.IsCompleted == patient.isComplete).ToArrayAsync();

                if (patient.PatientCardNumber != null && patient.groupID != null)
                {
                    result = await patientServ
                    .Where(e => e.PatientCardNumber.ToLower() == patient.PatientCardNumber.ToLower()
                                && e.IsCompleted == patient.isComplete
                                && e.RequestGroup == patient.groupID).ToArrayAsync();
                }

                if (result.Length <= 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($" fetching services to patient failed : {ex.StackTrace}");
            }
        }

        [HttpPut("get-patient-request-cashier")]
        public async Task<IActionResult> getPatientRequestInfoCashier([FromBody] PatientRequestedServicesViewOne patient)
        {
            try
            {
                var user = await this._userManager.FindByNameAsync(patient.loggedInUser);
                if (user == null)
                    return NotFound("User not found");

                var patientServ = await this
                    .PatientServiceQuery()
                    .Where(e => e.Paid==false)
                    .GroupBy(e => new {
                        e.PatientCardNumber,
                        e.Paid,
                        e.Complete,
                        e.PatientFirstName,
                        e.PatientLastName,
                        e.PatientMiddleName,
                        e.PatientAge,
                        e.PatientGender,
                        e.RequestedBy,
                        e.createdOn.Date
                    }).Select(s => new PatientReuestServicesDisplayDTO
                    {
                        PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                        PatientFirstName = s.FirstOrDefault().PatientFirstName,
                        PatientLastName = s.FirstOrDefault().PatientLastName,
                        PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                        PatientAge = s.FirstOrDefault().PatientAge,
                        PatientGender = s.FirstOrDefault().PatientGender,
                        Paid = s.FirstOrDefault().Paid,
                        IsCompleted = s.FirstOrDefault().Complete,
                        RequestedCatagories = s.GroupBy(g=> new { RequestGroup=g.RequestGroup, RequestedReason=g.RequestedReason, Paid=g.Paid})
                        .Select(c => new patientRequestOut {
                                groupID=c.FirstOrDefault().RequestGroup,
                                amount=c.Sum(s=>s.Price),
                                purpose= c.FirstOrDefault().RequestedReason,
                                isPaid= c.FirstOrDefault().Paid,
                        }).ToList(),
                        TotalPrice = s.Sum(s => s.Price),
                        NoRequestedServices = s.Select(s => s.RquestedServices).Count(),
                        RquestedServices = s.Select(s => new patientRequestServiceOut { service = s.RquestedServices, amount = s.Price,catagory=s.RequestedReason }).ToList(),
                        RequestedBy = s.FirstOrDefault().RequestedBy,
                        createdOn = s.FirstOrDefault().createdOn.Date
                    }).OrderByDescending(o => o.Paid)
                      
                      .ToArrayAsync();

                if (patientServ.Length <= 0)
                    return NoContent();

                return Ok(patientServ);
            }
            catch (Exception ex)
            {
                return BadRequest($" fetching services to patient failed : {ex.StackTrace}");
            }
        }

        [HttpPut("complete-patient-request")]
        public async Task<IActionResult> updatePatientRequestInfo([FromBody] PatientRequestedServicesViewOne patient)
        {
            try
            {
                var user = await this._userManager.FindByNameAsync(patient.loggedInUser);
                if (user == null)
                    return NotFound("User not found");
                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                    return BadRequest(new { msg = "Patient information does not exisits." });
                }

                var patientServ = await this._dbContext
                        .PatientRequestedServices
                        .Where(w=>w.isPaid==1 && w.isComplete==0 && w.MRN==patient.PatientCardNumber && w.groupId==patient.groupID)
                        .ExecuteUpdateAsync(e=> e.SetProperty(p=>p.isComplete,1));
                if (patientServ >0)
                {
                    return Ok(new { msg = "Action successfull!" });
                }
                return BadRequest(new { msg = "Request completion failed!"});
            }catch(Exception ex)
            {
                return BadRequest(new { msg=$"[.ex.] failed to complete request.{ex}"});
            }
        }

        [HttpPut("update-patient-info")]
        public async Task<IActionResult> ChangePatientInfo([FromBody] PatientUpdate patient)
        {
            var user = await this._userManager.FindByNameAsync(patient.PatientChangedBy);
            if (user == null)
                return NotFound("User not found");

            try
            {
                var Patient = await this._dbContext
                           .Patients
                           .Where(e => e.MRN == patient.PatientCardNumber)
                           .ExecuteUpdateAsync(e =>
                           e.SetProperty(p => p.firstName, patient.PatientFirstName)
                            .SetProperty(p => p.middleName, patient.PatientMiddleName)
                            .SetProperty(p => p.lastName, patient.PatientLastName)
                            .SetProperty(p => p.motherName, patient.PatientMotherName)
                            .SetProperty(p => p.gender, patient.PatientGender)
                            .SetProperty(p => p.religion, patient.PatientReligion)
                            .SetProperty(p => p.placeofbirth, patient.PatientPlaceofbirth)
                            .SetProperty(p => p.multiplebirth, patient.Multiplebirth)
                            .SetProperty(p => p.appointment, patient.Appointment)
                            .SetProperty(p => p.phonenumber, patient.PatientPhoneNumber)
                            .SetProperty(p => p.iscreadituser, patient.iscreadituser)
                            .SetProperty(p => p.iscbhiuser, patient.iscbhiuser)
                            .SetProperty(p => p.EmployementID, patient.iscreadituser == 1 ? patient.PatientEmployementID : null)
                            .SetProperty(p => p.occupation, patient.PatientOccupation)
                            .SetProperty(p => p.department, patient.Department)
                            .SetProperty(p => p.educationlevel, patient.PatientEducationlevel)
                            .SetProperty(p => p.maritalstatus, patient.PatientMaritalstatus)
                            .SetProperty(p => p.spouseFirstName, patient.PatientSpouseFirstName)
                            .SetProperty(p => p.spouselastName, patient.PatientSpouselastName)
                            .SetProperty(p => p.updatedBy, patient.PatientChangedBy) // Corrected to updatedBy
                            .SetProperty(p => p.updatedOn, DateTime.Now.Date) // Corrected to updatedOn
                           );
                var PatientAddress = await this._dbContext
                           .PatientAddress
                           .Where(e => e.MRN == patient.PatientCardNumber)
                           .ExecuteUpdateAsync(e =>
                           e.SetProperty(e => e.Region, patient.PatientRegion)
                            .SetProperty(e => e.Woreda, patient.PatientWoreda)
                            .SetProperty(e => e.Kebele, patient.PatientKebele)
                            .SetProperty(e => e.AddressDetail, patient.PatientAddressDetail)
                            .SetProperty(e => e.Phone, patient.PatientPhone)
                            .SetProperty(p => p.updatedBy, patient.PatientChangedBy)
                            .SetProperty(p => p.updatedOn, DateTime.Now.Date)
                           );

                var PatientKinAddress = await this._dbContext
                           .PatientAddress
                           .Where(e => e.REFMRN == patient.PatientCardNumber && e.isNextOfKin==1)
                           .ExecuteUpdateAsync(e =>
                           e.SetProperty(e => e.Region, patient.PatientKinRegion)
                            .SetProperty(e => e.Woreda, patient.PatientKinWoreda)
                            .SetProperty(e => e.Kebele, patient.PatientKinKebele)
                            .SetProperty(e => e.AddressDetail, patient.PatientKinAddressDetail)
                            .SetProperty(e => e.Phone, patient.PatientKinPhone)
                            .SetProperty(p => p.updatedBy, patient.PatientChangedBy)
                            .SetProperty(p => p.updatedOn, DateTime.Now.Date)
                           );

                if (patient.iscbhiuser == 1 && patient.iscbhiuserUpdated == true)
                {
                    // if the user is a CBHI User
                    ProvidersMapUsers provider = new ProvidersMapUsers
                    {
                        MRN = patient.PatientCardNumber,
                        provider = patient.Woreda,
                        Kebele = patient.Kebele,
                        Goth = patient.Goth,
                        IDNo = patient.IDNo,
                        letterNo = patient.letterNo,
                        Examination = patient.Examination,
                        service = "CBHI",
                        Createdby = patient.PatientChangedBy,
                        CreatedOn = DateTime.Now,
                        ReferalNo = patient.ReferalNo,
                    };
                    await this._dbContext.AddAsync<ProvidersMapUsers>(provider);
                    await this._dbContext.SaveChangesAsync();
                }
                else
                {
                    patient.Woreda = null;
                    patient.Kebele = null;
                    patient.Goth = null;
                    patient.IDNo = null;
                    patient.letterNo = null;
                    patient.Examination = null;
                    patient.Woreda = null;
                    patient.ReferalNo = null;
                }
                await this._dbContext.SaveChangesAsync();
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Insert PatientData failed! Reason: {ex.StackTrace}");
            }
        }
        [HttpPut("get-patient-info")]
        public async Task<IActionResult> GetPatientInfo([FromBody] PatientView patient)
        {
            try
            {
                var user = await this._userManager.FindByNameAsync(patient.Cashier);
                if (user == null)
                    return NotFound("User not found");

                var patientInfo = await this.PatientQuery()
                                .Where(e =>
                                    e.PatientCardNumber == patient.PatientCardNumber ||
                                    e.PatientFirstName == patient.PatientFirstName ||
                                    e.PatientMiddleName == patient.PatientMiddleName ||
                                    e.PatientLastName == patient.PatientLastName ||
                                    e.PatientPhoneNumber == patient.PatientPhone ||
                                    e.PatientPhone == patient.PatientPhone
                                    )
                                .ToArrayAsync();

                if (patientInfo == null)
                    return BadRequest(new { msg = "No patient with this card number!" });

                var CurrentCBHID = await this._dbContext.ProvidersMapPatient
                        .Where(e => e.MRN == patient.PatientCardNumber)
                        .GroupBy(g => new { g.MRN })
                        .Select(s => new { currentRecordID = s.Max(id => id.Id) })
                        .ToArrayAsync();

                if (CurrentCBHID.Length > 0)
                {
                    patientInfo = await this.PatientQuery()
                                .Where(e => (
                                    e.PatientCardNumber == patient.PatientCardNumber ||
                                    e.PatientFirstName == patient.PatientFirstName ||
                                    e.PatientMiddleName == patient.PatientMiddleName ||
                                    e.PatientLastName == patient.PatientLastName ||
                                    e.PatientPhoneNumber == patient.PatientPhone ||
                                    e.PatientPhone == patient.PatientPhone) &&
                                    e.Recoredid == CurrentCBHID[0].currentRecordID)
                                .ToArrayAsync();
                }

                return Ok(patientInfo);
            }catch(Exception e)
            {
                return BadRequest(new { message = "[.exp.] : Fetching patient information failed!" });
            }   
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
                             PatientAge = DateTime.Now.Year-patients.PatientDOB.Year,
                             RequestGroup = serviceMap.groupId,
                             PatientGender = patients.gender,
                             RquestedServices = purposeMap.Purpose,
                             RequestedReason = serviceMap.purpose,
                             Price = purposeMap.Amount,
                             RequestedBy = serviceMap.createdBy,
                             createdOn= serviceMap.createdOn,
                             Paid = serviceMap.isPaid == 0 ? false : serviceMap.isPaid == 1 ? true : null,
                             Complete = serviceMap.isComplete == 0 ? false : serviceMap.isPaid == 1 ? true : null
                         });

            return query;
        }
            [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PatientViewDTO> PatientQuery()
        {
            var query = from patients in this._dbContext.Patients
                        join cbhiuser in this._dbContext.ProvidersMapPatient on patients.MRN equals cbhiuser.MRN into patientMap
                        from patientMapCbhi in patientMap.DefaultIfEmpty()
                        join workers in this._dbContext.OrganiztionalUsers on patients.EmployementID equals workers.EmployeeID into _creditUsers
                        from creditUsers in _creditUsers.DefaultIfEmpty()
                        join patientAddress in this._dbContext.PatientAddress on patients.MRN equals patientAddress.MRN into _patientAddresses
                        from address in _patientAddresses.DefaultIfEmpty()
                        join patientKinAddress in this._dbContext.PatientAddress on patients.MRN equals patientKinAddress.REFMRN into _patientKinAddresses
                        from kinAddress in _patientKinAddresses.DefaultIfEmpty()
                        select new PatientViewDTO
                        {
                            PatientCardNumber = patients.MRN,
                            PatientFirstName = patients.firstName,
                            PatientMiddleName = patients.middleName,
                            PatientLastName = patients.lastName,
                            PatientMotherName = patients.motherName,
                            PatientGender = patients.gender,
                            PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,
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
                            PatientRegisteredBy = patients.createdBy,
                            PatientVisitingDate = patients.visitDate,

                            PatientRegion = address.Region,
                            PatientWoreda = address.Woreda,
                            PatientKebele = address.Kebele,
                            PatientAddressDetail = address.AddressDetail,
                            PatientPhone = address.Phone,

                            PatientKinRegion = kinAddress.Region,
                            PatientKinWoreda = kinAddress.Woreda,
                            PatientKinKebele = kinAddress.Kebele,
                            PatientKinAddressDetail = kinAddress.AddressDetail,
                            PatientKinPhone = kinAddress.Phone,

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
                            RegisteredOn=patients.createdOn,
                            RegistereddBy=patients.createdBy
                        };
            return query;
        }
    }
}
