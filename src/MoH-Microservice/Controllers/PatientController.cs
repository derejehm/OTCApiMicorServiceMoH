using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Models;
using System.Drawing;
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
                    MRN = patient.PatientCardNumber,
                    firstName = patient.PatientFirstName,
                    middleName = patient.PatientMiddleName,
                    lastName = patient.PatientLastName,
                    motherName = patient.PatientMotherName,
                    age = patient.PatientAge,
                    PatientDOB = patient.PatientDOB,
                    gender = patient.PatientGender,
                    religion = patient.PatientReligion,
                    placeofbirth = patient.PatientPlaceofbirth,
                    multiplebirth = patient.Multiplebirth,
                    appointment = patient.Appointment,
                    address = patient.PatientAddress,
                    kinAddress = patient.PatientkinAddress,
                    phonenumber = patient.PatientPhoneNumber,
                    iscreadituser = patient.iscreadituser,
                    iscbhiuser = patient.iscbhiuser,
                    EmployementID = patient.iscreadituser == 1 ? patient.PatientEmployementID : null,
                    occupation = patient.PatientOccupation,
                    department = patient.Department,
                    educationlevel = patient.PatientEducationlevel,
                    maritalstatus = patient.PatientMaritalstatus,
                    spouseFirstName = patient.PatientSpouseFirstName,
                    spouselastName = patient.PatientSpouselastName,
                    createdBy = patient.PatientRegisteredBy,
                    type = patient.PatientType,
                    visitDate = patient.PatientVisitingDate,
                    createdOn = DateTime.Now.Date,
                    updatedBy = null,
                    updatedOn = null
                };
                PatientAddress patientAddress = new PatientAddress
                {
                    MRN = patient.PatientCardNumber,
                    Region = patient.PatientRegion,
                    Woreda = patient.PatientWoreda,
                    Kebele = patient.PatientKebele,
                    HouseNo=patient.PatientHouseNo,
                    Mobile=patient.PatientPhoneNumber,
                    AddressDetail = patient.PatientAddressDetail,
                    Phone = patient.PatientPhone,
                    createdBy = patient.PatientRegisteredBy,
                    createdOn = DateTime.Now.Date,
                };
                
                PatientAddress patientKinAddress = new PatientAddress
                {
                    REFMRN = patient.PatientCardNumber,
                    Region = patient.PatientKinRegion,
                    Woreda = patient.PatientKinWoreda,
                    Kebele = patient.PatientKinKebele,
                    HouseNo= patient.PatientKinHouseNo,
                    Mobile= patient.PatientKinMobile,
                    AddressDetail = patient.PatientKinAddressDetail,
                    Phone = patient.PatientKinPhone,
                    isNextOfKin=1,
                    createdBy=patient.PatientRegisteredBy,
                    createdOn=DateTime.Now.Date,
                };

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

                if (patient.iscbhiuser == 1)
                {
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
                return BadRequest($"Error: Insert PatientData failed! Reason: {ex.StackTrace}");
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
                            .SetProperty(p => p.age, patient.PatientAge)
                            .SetProperty(p => p.gender, patient.PatientGender)
                            .SetProperty(p => p.religion, patient.PatientReligion)
                            .SetProperty(p => p.placeofbirth, patient.PatientPlaceofbirth)
                            .SetProperty(p => p.multiplebirth, patient.Multiplebirth)
                            .SetProperty(p => p.appointment, patient.Appointment)
                            .SetProperty(p => p.address, patient.PatientAddress)
                            .SetProperty(p => p.kinAddress, patient.PatientkinAddress)
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
            var user = await this._userManager.FindByNameAsync(patient.Cashier);
            if (user == null)
                return NotFound("User not found");

            var patientInfo = await this.PatientQuery()
                            .Where(e => 
                                e.PatientCardNumber == patient.PatientCardNumber || 
                                e.PatientFirstName == patient.PatientFirstName ||
                                e.PatientMiddleName== patient.PatientMiddleName ||
                                e.PatientLastName == patient.PatientLastName ||
                                e.PatientPhoneNumber==patient.PatientPhone ||
                                e.PatientPhone==patient.PatientPhone
                                )
                            .ToArrayAsync();

            if (patientInfo == null)
                return Ok("No patient with this card number!");

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
                            .ToArrayAsync();             }

            return Ok(patientInfo);
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
                            PatientAge = patients.age,
                            PatientGender = patients.gender,
                            PatientReligion = patients.religion,
                            PatientPlaceofbirth = patients.placeofbirth,
                            Multiplebirth = patients.multiplebirth,
                            Appointment = patients.appointment,
                            PatientAddress = patients.address,
                            PatientkinAddress = patients.kinAddress,
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
