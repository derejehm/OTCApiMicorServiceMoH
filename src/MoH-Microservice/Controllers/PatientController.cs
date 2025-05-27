using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models;
using System.Collections.Generic;
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
        private TokenValidate _tokenValidate;

        public PatientController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext payment)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = payment;
            this._tokenValidate = new TokenValidate(userManager);
        }

        [HttpPost("add-patient-info")]
        public async Task<IActionResult> addGetPatientInfo([FromBody] PatientReg patient, [FromHeader] string Authorization)
        {
            try
            {
               
               var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
       
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
                    visitDate =  Convert.ToDateTime(patient.PatientVisitingDate),
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
        [HttpPut("update-patient-info")]
        public async Task<IActionResult> ChangePatientInfo([FromBody] PatientUpdate patient, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

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
                            .SetProperty(p => p.updatedBy, user.UserName)
                            .SetProperty(p => p.updatedOn, DateTime.Now.Date)
                            .SetProperty(p => p.PatientDOB, Convert.ToDateTime(patient.PatientDOB))
                            .SetProperty(p => p.visitDate, Convert.ToDateTime(patient.PatientVisitingDate))
                           );
                var PatientAddress = await this._dbContext
                           .PatientAddress
                           .Where(e => e.MRN == patient.PatientCardNumber)
                           .ExecuteUpdateAsync(e =>
                           e.SetProperty(e => e.Region, patient.PatientRegion)
                            .SetProperty(e => e.Woreda, patient.PatientWoreda)
                            .SetProperty(e => e.Kebele, patient.PatientKebele)
                            .SetProperty(e => e.AddressDetail, patient.PatientAddressDetail)
                            .SetProperty(e => e.HouseNo, patient.PatientHouseNo)
                            .SetProperty(e => e.Phone, patient.PatientPhone)
                            .SetProperty(p => p.updatedBy, user.UserName)
                            .SetProperty(p => p.updatedOn, DateTime.Now.Date)
                           );

                var PatientKinAddress = await this._dbContext
                           .PatientAddress
                           .Where(e => e.REFMRN == patient.PatientCardNumber && e.isNextOfKin == 1)
                           .ExecuteUpdateAsync(e =>
                           e.SetProperty(e => e.Region, patient.PatientKinRegion)
                            .SetProperty(e => e.Woreda, patient.PatientKinWoreda)
                            .SetProperty(e => e.Kebele, patient.PatientKinKebele)
                            .SetProperty(e => e.AddressDetail, patient.PatientKinAddressDetail)
                            .SetProperty(e => e.HouseNo, patient.PatientKinHouseNo)
                            .SetProperty(e => e.Phone, patient.PatientKinPhone)
                            .SetProperty(e => e.Mobile, patient.PatientKinMobile)
                            .SetProperty(p => p.updatedBy, user.UserName)
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
                        Createdby = user.UserName,
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
                return BadRequest(new { msg = $"PATIENT REGISTRATION FAILED : {ex.Message}" });
            }
        }
        [HttpPut("get-patient-info")]
        public async Task<IActionResult> GetPatientInfo([FromBody] PatientView patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
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

                IEnumerable<PatientViewDTO> SearchResult = patientInfo;

                if (patient.PatientLastName.IsNullOrEmpty() &
                    patient.PatientMiddleName.IsNullOrEmpty() &
                    patient.PatientFirstName.IsNullOrEmpty() &
                    patient.PatientCardNumber.IsNullOrEmpty() &
                    patient.PatientPhone.IsNullOrEmpty())
                {
                    SearchResult.OrderByDescending(o=>o.Recoredid).Take(1000).ToList();
                }

                if (!patient.PatientCardNumber.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e =>e.PatientCardNumber.ToLower().Contains(patient.PatientCardNumber.ToLower()));
                }
                if (!patient.PatientFirstName.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientFirstName.ToLower().Contains(patient.PatientFirstName.ToLower()));
                }
                if (!patient.PatientMiddleName.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientMiddleName.ToLower().Contains(patient.PatientMiddleName.ToLower()));
                }
                if (!patient.PatientLastName.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientLastName.ToLower().Contains(patient.PatientLastName.ToLower()));
                }
                if (!patient.PatientPhone.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientPhone.Contains(patient.PatientPhone) || e.PatientPhoneNumber.Contains(patient.PatientPhone));
                }

                if (patientInfo == null)
                    throw new Exception("PATIENT DOES NOT EXIST");


                return Ok(new { data = SearchResult, TotalPatient = patientInfo.Count() });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = $"FETCHING PATIENT FAILED : {e.Message}" });
            }
        }
        [HttpPut("get-one-patient-info")]
        public async Task<IActionResult> GetOnePatientInfo([FromBody] PatientViewGetOne patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var patientInfo = await this.PatientQuery()
                                .Where(e => e.PatientCardNumber == patient.PatientCardNumber)
                                .OrderByDescending(e => e.PatientCardNumber)
                                .Take(1000)
                                .ToArrayAsync();

                if (patientInfo.Length <= 0)
                    throw new Exception("PATIENT DOES NOT EXIST");

                var CurrentCBHID = await this._dbContext.ProvidersMapPatient
                        .Where(e => e.MRN == patient.PatientCardNumber)
                        .GroupBy(g => new { g.MRN })
                        .Select(s => new { currentRecordID = s.Max(id => id.Id) })
                        .ToArrayAsync();

                if (CurrentCBHID.Length > 0)
                {
                    patientInfo = await this.PatientQuery()
                                .Where(e => (
                                    e.PatientCardNumber == patient.PatientCardNumber &&
                                    e.Recoredid == CurrentCBHID[0].currentRecordID))
                                .ToArrayAsync();
                }
                return Ok(patientInfo[0]);
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = $"FETCHING PATIENT FAILED : {e.Message}" });
            }
        }
        [HttpPost("add-patient-request")]
        public async Task<IActionResult> addPatientRequestInfo([FromBody] PatientRequestedServicesReg PatientRequest, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == PatientRequest.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                   throw new Exception( "PATIENT DOES NOT EXIST" );
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
                        createdBy = user.UserName,
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
                return BadRequest(new { msg = $"FAILED TO ASSIGN SERVICES : {ex.Message}" });
            }
        }

        [HttpPut("get-patient-request")]
        public async Task<IActionResult> getOnePatientRequestInfo([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                // check the user form token and verify its eistnce in db
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

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
                                    RquestedServices = s.Select(s => new patientRequestServiceOut { service = s.RquestedServices, amount = s.Price,catagory=s.RequestedReason }).ToList(),
                                    RequestedReason = s.FirstOrDefault().RequestedReason,
                                    RequestedBy = s.FirstOrDefault().RequestedBy,
                                    createdOn = s.FirstOrDefault().createdOn.Date

                                })
                .OrderByDescending(o => o.Paid); ;

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
                return BadRequest( new { msg = $"FAILD TO FEATCH PATIENT INFORMATION: {ex.Message}" });
            }
        }

        [HttpPut("get-patient-request-cashier")]
        public async Task<IActionResult> getPatientRequestInfoCashier([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var patientServ = await this
                    .PatientServiceQuery()
                    .Where(e => (e.Paid==false || e.Paid == null) && e.Complete==false)
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
                                isPaid= !c.FirstOrDefault().Paid,
                        }).ToList(),
                        TotalPrice = s.Sum(s => s.Price),
                        NoRequestedServices = s.Select(s => s.RquestedServices).Count(),
                        RquestedServices = s.Select(s => new patientRequestServiceOut { service = s.RquestedServices, amount = s.Price,catagory=s.RequestedReason }).ToList(),
                        RequestedBy = s.FirstOrDefault().RequestedBy,
                        createdOn = s.FirstOrDefault().createdOn.Date
                    }).OrderByDescending(o => o.Paid).ToArrayAsync();

                if (patientServ.Length <= 0)
                    return NoContent();

                return Ok(patientServ);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $" FETCHING PATIENT INFORMATION FAILD : {ex.Message}" });
            }
        }

        [HttpPut("complete-patient-request")]
        public async Task<IActionResult> updatePatientRequestInfo([FromBody] PatientRequestedServicesViewOne patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                    throw new Exception("PATIENT DOES NOT EXIST.");
                }
                var patientServ = await this._dbContext
                        .PatientRequestedServices
                        .Where(w=>w.isPaid==1 && w.isComplete==0 && w.MRN==patient.PatientCardNumber && w.groupId==patient.groupID)
                        .ExecuteUpdateAsync(e=> 
                        e.SetProperty(p => p.isComplete,1)
                         .SetProperty(p => p.updatedBy,user.UserName)
                         .SetProperty(p => p.updatedOn,DateTime.Now)
                        );
                if (patientServ >0)
                    return Ok(new { msg = "REQUEST COMPLETED SUCCESSFUL!" });
                else
                    throw new Exception("REQUEST FAILD TO UPDATE  :: REQUEST IS YET TO BE PAID");

            }catch(Exception ex)
            {
                return BadRequest(new { msg=$"FAILED TO COMLETE REQUEST : {ex.Message}"});
            }
        }
        [HttpDelete("cancel-patient-request")]
        public async Task<IActionResult> deletePatientRequestInfo([FromBody] PatientRequestedServicesDelete patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                    throw new Exception("PATIENT DOES NOT EXIST.");
                }
                var patientServ = await this._dbContext
                        .PatientRequestedServices
                        .Where(w => (w.isPaid == 0 || w.isPaid==null) && 
                        w.isComplete == 0 && 
                        w.MRN == patient.PatientCardNumber && 
                        w.groupId == patient.groupID &&
                        w.purpose == patient.purpose).ExecuteDeleteAsync();

                if (patientServ > 0)
                    return Ok(new { msg = "REQUEST CANCELED" });
                else
                    // the reqiest can only be canceled if it is not paid.
                    throw new Exception("REQUEST FAILD TO CANCEL  :: REQUEST NOT FOUND");
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"FAILED TO COMLETE REQUEST : {ex.Message}" });
            }
        }
        [HttpPost("add-patient-accedent")]
        public async Task<IActionResult> addPatientAccedent([FromBody] PatientAccedentsReg patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                    throw new Exception("PATIENT DOES NOT EXIST.");
                }

                PatientAccedent accedents = new PatientAccedent
                {
                    MRN = patient.PatientCardNumber,
                    accedentAddress = patient.accAddress,
                    accedentDate = Convert.ToDateTime(patient.accDate),
                    policeName = patient.policeName,
                    policePhone = patient.policePhone,
                    plateNumber = patient.plateNumber,
                    certificate = patient.certificate,
                    createdOn = DateTime.Now,
                    createdBy = user.UserName
                };
                await this._dbContext.AddAsync(accedents);
                await this._dbContext.SaveChangesAsync();
                return Ok(accedents);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"FAILED TO COMLETE REQUEST : {ex.Message}" });
            }
        }

        [HttpPut("get-patient-accedent")]
        public async Task<IActionResult> getPatientAccedent([FromBody] PatientView patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();


                var accedents = this.PatientQuery();
                IEnumerable<PatientViewDTO> SearchResult = accedents.Where(w=>w.AccedentRecId!=null);

                if (patient.PatientLastName.IsNullOrEmpty() &
                    patient.PatientMiddleName.IsNullOrEmpty() &
                    patient.PatientFirstName.IsNullOrEmpty() &
                    patient.PatientCardNumber.IsNullOrEmpty() &
                    patient.PatientPhone.IsNullOrEmpty())
                {
                    SearchResult.OrderByDescending(o => o.Recoredid).Take(1000).ToList();
                }

                if (!patient.PatientCardNumber.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientCardNumber.ToLower().Contains(patient.PatientCardNumber.ToLower()));
                }
                if (!patient.PatientFirstName.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientFirstName.ToLower().Contains(patient.PatientFirstName.ToLower()));
                }
                if (!patient.PatientMiddleName.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientMiddleName.ToLower().Contains(patient.PatientMiddleName.ToLower()));
                }
                if (!patient.PatientLastName.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientLastName.ToLower().Contains(patient.PatientLastName.ToLower()));
                }
                if (!patient.PatientPhone.IsNullOrEmpty())
                {
                    SearchResult = SearchResult.Where(e => e.PatientPhone.Contains(patient.PatientPhone));
                }
                var Output = SearchResult.GroupBy(accedents => new
                {
                    accedents.AccedentRecId,
                    accedents.AccedentDate,
                    accedents.PatientCardNumber,
                    accedents.PatientFirstName,
                    accedents.PatientMiddleName,
                    accedents.PatientLastName,
                    accedents.PatientDOB,
                    accedents.PatientAge,
                    accedents.AcceedentAddress,
                    accedents.PlateNumber,
                    accedents.CarCertificate,
                    accedents.PoliceName,
                    accedents.PolicePhone,
                }).Select(accedents =>
                            new
                            {
                                accedents.FirstOrDefault().AccedentRecId,
                                accedents.FirstOrDefault().AccedentDate,
                                accedents.FirstOrDefault().PatientCardNumber,
                                accedents.FirstOrDefault().PatientFirstName,
                                accedents.FirstOrDefault().PatientMiddleName,
                                accedents.FirstOrDefault().PatientLastName,
                                accedents.FirstOrDefault().PatientDOB,
                                accedents.FirstOrDefault().PatientAge,
                                accedents.FirstOrDefault().AcceedentAddress,
                                accedents.FirstOrDefault().PlateNumber,
                                accedents.FirstOrDefault().CarCertificate,
                                accedents.FirstOrDefault().PoliceName,
                                accedents.FirstOrDefault().PolicePhone,
                            });
                return Ok(new { data= Output });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"FAILED TO COMLETE REQUEST : {ex.Message}" });
            }
        }

        [HttpPut("change-patient-accedent")]
        public async Task<IActionResult> UpdatePatientAccedent([FromBody] PatientAccedentsReg patient, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                {
                    throw new Exception("PATIENT DOES NOT EXIST.");
                }

                var accedents = await this._dbContext.PatientAccedents
                    .Where(w => w.id == patient.id)
                    .ExecuteUpdateAsync(u => u
                     .SetProperty(p => p.MRN, patient.PatientCardNumber)
                     .SetProperty(p => p.accedentAddress, patient.accAddress)
                     .SetProperty(p => p.accedentDate, Convert.ToDateTime(patient.accDate))
                     .SetProperty(p => p.policeName, patient.policeName)
                     .SetProperty(p => p.policePhone, patient.policePhone)
                     .SetProperty(p => p.plateNumber, patient.plateNumber)
                     .SetProperty(p => p.certificate, patient.certificate)
                     .SetProperty(p => p.updatedOn, DateTime.Now)
                     .SetProperty(p => p.updatedBy, user.UserName)
                    );

                if (accedents > 0)
                {
                    await this._dbContext.SaveChangesAsync();
                    return Ok(new { msg = "ACCEDENT CHANGED SUCCESSFULY!" });
                }
                else
                    throw new Exception("COULDN'T FIND THE ACCEDENT REGISTRATION.");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"FAILED TO COMLETE ACTION : {ex.Message}" });
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
                         }).AsNoTracking();

            return query;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PatientViewDTO> PatientQuery()
        {
            var query = (from patients in this._dbContext.Patients
                        join cbhiuser in this._dbContext.ProvidersMapPatient on patients.MRN equals cbhiuser.MRN into patientMap
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
                            RowID=patients.Id,
                            PatientCardNumber = patients.MRN,
                            PatientFirstName = patients.firstName,
                            PatientMiddleName = patients.middleName,
                            PatientLastName = patients.lastName,
                            PatientMotherName = patients.motherName,
                            PatientGender = patients.gender,
                            PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,
                            PatientDOB= patients.PatientDOB,
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
                            PatientHouseNo=address.HouseNo,
                            PatientPhone = address.Phone,

                            PatientKinRegion = kinAddress.Region,
                            PatientKinWoreda = kinAddress.Woreda,
                            PatientKinKebele = kinAddress.Kebele,
                            PatientKinAddressDetail = kinAddress.AddressDetail,
                            PatientKinHouseNo = kinAddress.HouseNo,
                            PatientKinPhone = kinAddress.Phone,
                            PatientKinMobile= kinAddress.Mobile,

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

                            AccedentRecId= patientAccedents.id,
                            AcceedentAddress = patientAccedents.accedentAddress,
                            AccedentDate = patientAccedents.accedentDate,
                            PoliceName  = patientAccedents.policeName,
                            PolicePhone = patientAccedents.policePhone,
                            PlateNumber = patientAccedents.plateNumber,
                            CarCertificate = patientAccedents.certificate,

                            RegisteredOn =patients.createdOn,
                            RegistereddBy=patients.createdBy
                        }).AsNoTracking();
            return query;
        }
    }
}
