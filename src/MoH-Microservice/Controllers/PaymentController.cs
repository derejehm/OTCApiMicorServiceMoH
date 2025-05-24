
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models;
using MoH_Microservice.Query;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;



namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _payment;
        private readonly AppQuery _query;
        private TokenValidate _tokenValidate;

        public PaymentController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
            ) {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._payment = payment;
            this._query = new AppQuery(payment);
            this._tokenValidate = new TokenValidate(userManager);
        }

        [HttpPut("Get-all-Payment")]
        public async Task<IActionResult> GetPaymentByDate([FromBody] PaymentbyDate payment, [FromHeader] string Authorization)
        {
            
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                if (user.UserType != "Supervisor")
                {

                    var data = await this.PaymentQuery().Where(e => e.RegisteredBy.ToLower() == user.UserName.ToLower() 
                                        && e.RegisteredOn.Value.Date >= payment.startDate 
                                        && e.RegisteredOn.Value.Date <= payment.endDate).ToArrayAsync();

                    if (data.Length <= 0)
                        return NoContent();
                    return Ok(new JsonResult(data).Value);
                }
                else
                {
                    var data = await this.PaymentQuery().Where(e=>e.RegisteredOn.Value.Date >=payment.startDate 
                                                            && e.RegisteredOn.Value.Date<=payment.endDate).ToArrayAsync();

                    if (data.Length <= 0)
                        return NoContent();

                    return Ok(new JsonResult(data).Value);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }
        [HttpPut("rpt-all-Payment")] // report excluding accedents treatments
        public async Task<IActionResult> RptPaymentByDate([FromBody] PaymentbyDate payment, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                if (user.UserType.ToLower() != "supervisor")
                {

                    var data = await this.PaymentQuery()
                                         .Where(e => e.RegisteredBy.ToLower() == user.UserName.ToLower() &&
                                                     e.RegisteredOn.Value.Date >= payment.startDate &&
                                                     e.RegisteredOn.Value.Date <= payment.endDate)
                                         .GroupBy(g => new {
                                             g.PatientCardNumber,
                                             g.PatientName,
                                             g.PatientVisiting,
                                             g.PatientAge,
                                             g.PatientGender,
                                             g.PatientKebele,
                                             g.PatientsGoth,
                                             g.PatientReferalNo,
                                             g.PatientCBHI_ID,
                                             g.PatientType,
                                             g.PaymentReason,
                                             g.PaymentType,
                                             g.PatientWorkingPlace,
                                             g.PatientWorkID,
                                             g.PatientWoreda,
                                             g.accedentDate,
                                             g.policeName,
                                             g.policePhone,
                                             g.CarPlateNumber,
                                             g.RegisteredBy

                                         })
                                         .Select(s => new
                                         {
                                             CardNumber = s.FirstOrDefault().PatientCardNumber,
                                             Name = s.FirstOrDefault().PatientName,
                                             VisitingDate = s.FirstOrDefault().PatientVisiting,
                                             Age = s.FirstOrDefault().PatientAge,
                                             Gender = s.FirstOrDefault().PatientGender,
                                             Kebele = s.FirstOrDefault().PatientKebele,
                                             Goth = s.FirstOrDefault().PatientsGoth,
                                             ReferalNo = s.FirstOrDefault().PatientReferalNo,
                                             IDNo = s.FirstOrDefault().PatientCBHI_ID,
                                             PatientType = s.FirstOrDefault().PatientType,
                                             PaymentType = s.FirstOrDefault().PaymentType,

                                             PatientWorkingPlace =s.FirstOrDefault().PatientWorkingPlace,
                                             PatientWorkID = s.FirstOrDefault().PatientWorkID,
                                             CBHIProvider = s.FirstOrDefault().PatientWoreda,

                                             AccedentDate = s.FirstOrDefault().accedentDate,
                                             policeName = s.FirstOrDefault().policeName,
                                             PolicePhone = s.FirstOrDefault().policePhone,
                                             CarPlateNumber = s.FirstOrDefault().CarPlateNumber,

                                             CardPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("card") ? s.PaymentAmount : 0),
                                             UnltrasoundPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("ultrasound") ? s.PaymentAmount : 0),
                                             ExaminationPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("exam") ? s.PaymentAmount : 0),
                                             MedicinePaid = s.Sum(s => s.PaymentReason.ToLower().Contains("medi") ? s.PaymentAmount : 0),
                                             LaboratoryPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("lab") ? s.PaymentAmount : 0),
                                             BedPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("bed") ? s.PaymentAmount : 0),
                                             SurgeryPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("Surgery") ? s.PaymentAmount : 0),
                                             Foodpaid = s.Sum(s => s.PaymentReason.ToLower().Contains("food") ? s.PaymentAmount : 0),
                                             OtherPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("other") ? s.PaymentAmount : 0),

                                             TotalPaid = s.Sum(s => s.PaymentAmount),

                                         }).ToArrayAsync();

                    if (data.Length <= 0)
                        return NoContent();
                    return Ok(new JsonResult(data).Value);
                }
                else
                {
                    var data = await this.PaymentQuery()
                                           .Where(e=> e.RegisteredOn.Value.Date >= payment.startDate &&
                                                     e.RegisteredOn.Value.Date <= payment.endDate)
                                         .GroupBy(g => new {
                                             g.PatientCardNumber,
                                             g.PatientName,
                                             g.PatientVisiting,
                                             g.PatientAge,
                                             g.PatientGender,
                                             g.PatientKebele,
                                             g.PatientsGoth,
                                             g.PatientReferalNo,
                                             g.PatientCBHI_ID,
                                             g.PatientType,
                                             g.PaymentReason,
                                             g.PaymentType,
                                             g.PatientWorkingPlace,
                                             g.PatientWorkID,
                                             g.PatientWoreda,
                                             g.accedentDate,
                                             g.policeName,
                                             g.policePhone,
                                             g.CarPlateNumber,
                                             g.RegisteredBy

                                         })
                                         .Select(s => new
                                         {
                                             CardNumber = s.FirstOrDefault().PatientCardNumber,
                                             Name = s.FirstOrDefault().PatientName,
                                             VisitingDate = s.FirstOrDefault().PatientVisiting,
                                             Age = s.FirstOrDefault().PatientAge,
                                             Gender = s.FirstOrDefault().PatientGender,
                                             Kebele = s.FirstOrDefault().PatientKebele,
                                             Goth = s.FirstOrDefault().PatientsGoth,
                                             ReferalNo = s.FirstOrDefault().PatientReferalNo,
                                             IDNo = s.FirstOrDefault().PatientCBHI_ID,
                                             PatientType = s.FirstOrDefault().PatientType,
                                             PaymentType = s.FirstOrDefault().PaymentType,

                                             PatientWorkingPlace = s.FirstOrDefault().PatientWorkingPlace,
                                             PatientWorkID = s.FirstOrDefault().PatientWorkID,
                                             CBHIProvider = s.FirstOrDefault().PatientWoreda,

                                             AccedentDate = s.FirstOrDefault().accedentDate,
                                             policeName = s.FirstOrDefault().policeName,
                                             PolicePhone = s.FirstOrDefault().policePhone,
                                             CarPlateNumber = s.FirstOrDefault().CarPlateNumber,

                                             CardPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("card") ? s.PaymentAmount : 0),
                                             UnltrasoundPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("ultrasound") ? s.PaymentAmount : 0),
                                             ExaminationPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("exam") ? s.PaymentAmount : 0),
                                             MedicinePaid = s.Sum(s => s.PaymentReason.ToLower().Contains("medi") ? s.PaymentAmount : 0),
                                             LaboratoryPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("lab") ? s.PaymentAmount : 0),
                                             BedPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("bed") ? s.PaymentAmount : 0),
                                             SurgeryPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("Surgery") ? s.PaymentAmount : 0),
                                             Foodpaid = s.Sum(s => s.PaymentReason.ToLower().Contains("food") ? s.PaymentAmount : 0),
                                             OtherPaid = s.Sum(s => s.PaymentReason.ToLower().Contains("other") ? s.PaymentAmount : 0),

                                             TotalPaid = s.Sum(s => s.PaymentAmount),

                                         }).ToArrayAsync();

                    if (data.Length <= 0)
                        return NoContent();

                    return Ok(new JsonResult(data).Value);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }

        [HttpPut("payment-by-refno")]
        public async Task<IActionResult> GetPaymentInfoByRefNo([FromBody] PaymentInfo payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this.PaymentQuery().Where(x => x.ReferenceNo == payment.paymentId).ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }

        [HttpPut("payment-by-RefNoInstitution")]
        public async Task<IActionResult> GetPaymentInfoByRefNo([FromBody] PaymentDetailByInstitution payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this.PaymentQuery()
                    .Where(x => x.ReferenceNo == payment.paymentId && x.HospitalName == payment.hospital)
                    .ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();
                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }
        [HttpPut("payment-by-cashier")]

        public async Task<IActionResult> GetPaymentInfoByCashier([FromHeader] string Authorization)
        { 
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this.PaymentQuery()
                    .Where(x => x.RegisteredBy == user.UserName && x.RegisteredOn.Value.Date== DateTime.Now.Date)
                    .ToArrayAsync();
                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch(Exception ex)
            {
                   return BadRequest(ex.Message);
            }  
        }

        [HttpPut("payment-by-cardNumber")]
        public async Task<IActionResult> GetPaymentInfoByCardNumber([FromBody] PaymentDetailByCardNo payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this.PaymentQuery().Where(x => x.PatientCardNumber == payment.PatientCardNumber).ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("payment-by-phonenumber")]
        public async Task<IActionResult> getPaymentByPphone([FromBody] PaymentDetailByPhone payment, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this.PaymentQuery()
                                            .Where(e => e.PatientPhone == payment.phone)
                                            .ToArrayAsync();

                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }

        }
        [HttpPut("payment-by-patientname")]
        public async Task<IActionResult> getPaymentByPname([FromBody] PaymentDetailByName payment, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var PymentInfo = await this.PaymentQuery().Where(e => e.PatientName == payment.patient).ToArrayAsync();
                if (PymentInfo.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(PymentInfo).Value);
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
        
        [HttpPost("add-payment")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> InsertPaymentInfo([FromBody] PaymentReg payment,[FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                string worker = null;
                long CurrentCBHID = 0l;
                long AccedentID = 0l;

                if (user.UserType.ToLower() != "cashier")
                    throw new Exception("YOU CAN'T PERFORM PAYMENT");

                // check if the patient has been registed. 
                var doesPatientExisit = await this._payment.Patients.Where(e => e.MRN == payment.CardNumber).ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                    throw new Exception($"PATIENT IS NOT REGISTEED / ታካሚው አልተመዘገበም !{doesPatientExisit.ToList().Count()}");

                // fetch the max card payment date
                // this will be usefull to check the last time payment has been made for a card / MRN

                var MaxCardDate = await this._payment.Payments.Where(e => e.MRN == payment.CardNumber && e.Purpose.ToLower() == "card")
                        .GroupBy(e => new { e.MRN })
                        .Select(e => new { maxregdate = e.Max(e => e.CreatedOn) })
                        .ToArrayAsync();
                // check if payment for card has been paid for.

                //if (MaxCardDate.Length <= 0 && !payment.Amount.Any(e=>e.Purpose.ToLower()=="card"))
                //    throw new Exception("CARD PAYMENT REQUIRED / የካርድ ክፍያ አልተከናወነም!");

                
                // check for the worker if credit payment issued 
                if (payment.PaymentType.ToLower() == "credit")
                {
                    // check if the credit patient has been registered.
                    var result = await this._payment.OrganiztionalUsers
                                        .Where(e => e.EmployeeID.ToLower() == payment.PatientWorkID.ToLower()
                                                 && e.AssignedHospital.ToLower() == user.Hospital.ToLower()
                                                 && e.WorkPlace.ToLower() == payment.organization).ToArrayAsync();
                    
                    if (!result.Any())
                        throw new Exception($" CREADIT USER  [ EmployeeID : {payment.PatientWorkID} ] IS NOT ASSIGNED TO THIS HOSPITAL");
                    
                    worker = result.FirstOrDefault().EmployeeID;
                }
                     
                // if cbhi payment issued check if the user is registed for cbhi service
                if (payment.PaymentType.ToLower() == "cbhi")
                {
                    // get the current valid CBHI info
                    var result  = await this._payment.ProvidersMapPatient
                            .Where(e => e.MRN == payment.CardNumber)
                            .GroupBy(g => new { g.MRN })
                            .Select(s => new { 
                                currentRecordID = s.Max(id => id.Id), 
                                ExpDate = s.Max(s=>s.ExpDate)
                            })
                            .ToArrayAsync();
                    if (!result.Any())
                        throw new Exception("PLEASE REGISTER CBHI INFORMATION!");

                    CurrentCBHID = result.FirstOrDefault().currentRecordID;
                }

                // if traffic payment issued check  if the user is registed and the time limit has not expired.
                if (payment.PaymentType.ToLower() == "traffic")
                {
                    // get the current valid Accdents info

                    var result = await this._payment.PatientAccedents
                            .Where(e => e.MRN == payment.CardNumber)
                            .GroupBy(g => new { g.MRN })
                            .Select(s => new {
                                LastAccedentID = s.Max(s => s.id),
                                LastAccedentDate = s.Max(s => s.accedentDate),
                                TreatedSince = EF.Functions.DateDiffHour(s.Max(s => s.createdOn), DateTime.Now)
                            }).ToArrayAsync();

                    if (!result.Any())
                        throw new Exception("PLEASE REGISTER ACCEDENT INFORMATION!");

                    if (result.FirstOrDefault().TreatedSince >= 24)
                        throw new Exception($"24hr HAS PASSED : USE OTHER PAYMENT SYSTEM <::> CurrentTimeDifferenceInHours :{result.FirstOrDefault().TreatedSince}");

                    AccedentID = result.FirstOrDefault().LastAccedentID;
                }
                // generate a refno
                var RefNo = $"{user.Hospital.Trim().Substring(0, 2).ToUpper()}{payment.CardNumber}{payment.PaymentType.ToUpper().Replace(" ", "").Trim().Substring(0, 4)}{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}{DateTime.Now.Microsecond}{new Random().Next(1000,9999)}";
                // list of paid services :  output
                List<PatientReuestServicesViewDTO[]> groupPayment = new List<PatientReuestServicesViewDTO[]>();
                
                if (payment.Amount.Count() <= 0)
                    throw new Exception("THERE IS NO PAYMENT : AMOUNT IS EMPTY");

                    foreach (var items in payment.Amount)
                    {

                        if (MaxCardDate.Length > 0
                            && (DateTime.Now - MaxCardDate.FirstOrDefault().maxregdate).Value.Days <= 15
                            && items.Purpose.ToLower() == "card")
                        {
                            throw new Exception($"DAYS PASSED SINCE LAST REGISTRATION ( {(DateTime.Now - MaxCardDate[0].maxregdate).Value.Days} )\nLAST REGISTRATION DATE : {MaxCardDate[0].maxregdate}");
                        }
                        
                        Payment data = new Payment()
                        {
                            RefNo = RefNo,
                            HospitalName = user.Hospital,
                            Createdby = user.UserName,
                            Department = user.Departement,
                            MRN = payment.CardNumber,
                            Type = payment.PaymentType.ToUpper(),
                            PaymentVerifingID = payment.PaymentVerifingID, //  digital payment id
                            Channel = payment.Channel,
                            Description = payment.Description,
                            PatientWorkID = worker, // creadit users
                            CBHIID = CurrentCBHID==0l?null:CurrentCBHID, // cbhi users
                            AccedentID = AccedentID==0l?null: AccedentID, // accedent registration
                            groupId = items.groupID,
                            Purpose = items.Purpose.ToUpper(), //
                            Amount = items.Amount,
                        };
                        if (!items.groupID.IsNullOrEmpty()) {

                        // check if the payment has aleardy been completed!
                        var groupPaymentExists = await this._query.PatientServiceQuery()
                             .Where(e =>  e.RequestGroup == items.groupID
                                 && e.PatientCardNumber == payment.CardNumber
                                 && e.RequestedReason == items.Purpose
                                 && e.Paid==true
                                 ).ToArrayAsync();
                        groupPayment.Add(groupPaymentExists);
                        if(groupPaymentExists.Length <=0)
                        {
                            await this._payment.PatientRequestedServices
                                .Where(e => (e.isPaid == 0 || e.isPaid ==null)
                                    && e.groupId == items.groupID
                                    && e.MRN == payment.CardNumber
                                    && e.purpose == items.Purpose)
                                .ExecuteUpdateAsync(u => u.SetProperty(p => p.isPaid, items.isPaid == true ? 1 : null));
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (!items.groupID.IsNullOrEmpty() && (items.isPaid == false || items.isPaid==null))
                    {
                        await this._payment.SaveChangesAsync();
                        continue;
                    }
                    await this._payment.AddAsync(data);
                    await this._payment.SaveChangesAsync();
                }

                var paymentDetails = await this.PaymentQuery().Where(e => e.ReferenceNo == RefNo).ToArrayAsync();
               
                return Created("/", new {   RefNo = RefNo, 
                                            data = paymentDetails, 
                                            grp_exisiting_payment=groupPayment});
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"PAYMENT FAILED !! :: {ex.Message}" });
            }
        }


        [HttpPost("add-service-provider")]
        public async Task<IActionResult> addGetProviderInfo([FromBody] ProvidersMapReg providers, [FromHeader] string Authorization)
        {
            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var doesPatientExisit = await this._payment.Patients.Where(e => e.MRN == providers.CardNumber).ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                    throw new Exception("PATIENT IS NOT REGISTEED / ታካሚው አልተመዘገበም !");

                ProvidersMapUsers provider = new ProvidersMapUsers
                {
                    MRN = providers.CardNumber,
                    provider = providers.provider,
                    Kebele = providers.Kebele,
                    Goth = providers.Goth,
                    IDNo = providers.IDNo,
                    letterNo = providers.letterNo,
                    Examination = providers.Examination,
                    service = providers.service,
                    Createdby = user.UserName,
                    CreatedOn = DateTime.Now,
                    ReferalNo = providers.ReferalNo,
                    ExpDate=Convert.ToDateTime(providers.ExpDate.ToString()),
                };
                await this._payment.AddAsync<ProvidersMapUsers>(provider);
                await this._payment.SaveChangesAsync();

                return Created("/", provider);
            }
            catch (Exception ex)
            {
                return BadRequest($"CBHI REGISTRATION FAILD : {ex}");
            }
        }

        [HttpGet("get-service-provider")]
        public async Task<IActionResult> GetAllProviderInfo([FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                var patientInfo = await this._payment.ProvidersMapPatient
                    .GroupBy(g => g.MRN)
                    .Select(s => new { latestRecId = s.Max(s => s.Id) })
                    .Join(this._payment.ProvidersMapPatient,maxid=>maxid.latestRecId,p=>p.Id,(maxid,p)=>p)
                    .ToArrayAsync();

                if (patientInfo.Length <= 0)
                {
                    return NoContent();
                }
                return Ok(patientInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }


        }

        [HttpPut("get-service-provider")]
        public async Task<IActionResult> GetOneProviderInfo([FromBody] ProvidersParam providers, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                var CurrentCBHID = await this._payment.ProvidersMapPatient
                                .Where(e => e.MRN == providers.cardnumber)
                                .GroupBy(g => new { g.MRN })
                                .Select(s => new { currentRecordID = s.Max(id => id.Id) })
                                .ToArrayAsync();
                if (CurrentCBHID.Length > 0)
                {
                    var patientInfo = await this._payment.ProvidersMapPatient
                                .Where(e => e.MRN == providers.cardnumber && e.Id == CurrentCBHID[0].currentRecordID)
                                .ToArrayAsync();
                    return Ok(patientInfo);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }


        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<PaymentReportDTO> PaymentQuery()
        {
            var query = from payments in this._payment.Payments
                        join patients in this._payment.Patients on payments.MRN equals patients.MRN into rpt_patient
                        from report in rpt_patient.DefaultIfEmpty()
                        join cbhiInfo in this._payment.ProvidersMapPatient on payments.CBHIID equals cbhiInfo.Id into rpt_cbhiinfo
                        from cbhiusers in rpt_cbhiinfo.DefaultIfEmpty()
                        join workers in this._payment.OrganiztionalUsers on payments.PatientWorkID equals workers.EmployeeID into rpt_worker
                        from report_workers in rpt_worker.DefaultIfEmpty()
                        join accendets  in this._payment.PatientAccedents on payments.AccedentID equals accendets.id into _rpt_accedents
                        from rpt_accedents in _rpt_accedents.DefaultIfEmpty()
                        select new PaymentReportDTO
                        {
                            RowId = payments.id,
                            ReferenceNo = payments.RefNo,
                            PatientCardNumber = payments.MRN,
                            HospitalName = payments.HospitalName,
                            Department = payments.Department,
                            PaymentChannel = payments.Channel,
                            PaymentType=payments.Type,
                            PatientName = report.firstName == null ? report_workers.EmployeeName ?? "" : report.firstName +" "+ report.middleName+" "+ report.lastName,
                            PatientPhone = report.phonenumber ?? report_workers.EmployeePhone ?? "",
                            PatientAge = report.PatientDOB !=null ? EF.Functions.DateDiffYear(DateTime.Now, report.PatientDOB).ToString() : "",
                            PatientGender = report.gender ?? "",
                            PatientVisiting = report.visitDate,
                            PatientType = report.type ?? "",
                            PaymentVerifingID = payments.PaymentVerifingID,
                            PatientWorkingPlace = report_workers.WorkPlace,
                            PatientWorkID = payments.PatientWorkID,
                            PatientWoreda = cbhiusers.provider,
                            PatientCBHI_ID=cbhiusers.IDNo,
                            PatientKebele=cbhiusers.Kebele,
                            PatientExamination=cbhiusers.Examination,
                            PatientLetterNo=cbhiusers.letterNo,
                            PatientReferalNo=cbhiusers.ReferalNo,
                            PatientsGoth=cbhiusers.Goth,
                            PaymentReason = payments.Purpose,
                            PaymentAmount = payments.Amount,
                            PaymentDescription = payments.Description,
                            PaymentIsCollected = payments.IsCollected,
                            accedentDate = rpt_accedents.accedentDate,
                            policeName = rpt_accedents.policeName,
                            policePhone = rpt_accedents.policePhone,
                            CarPlateNumber = rpt_accedents.plateNumber,
                            CarCertificate = rpt_accedents.certificate,
                            RegisteredBy = payments.Createdby,
                            RegisteredOn = payments.CreatedOn, 
                        };
            return query;
        }
        private class BankLinkList
        {
            
            public string? Institution { get; set; }
            public string? QRLink { get; set; }
        }
        public class PaymentReportDTO
        {
            public long RowId { get; set; }
            public string? ReferenceNo { get; set; }
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
            public string? PatientKebele{ get; set; }
            public string? PatientsGoth { get; set; }
            public string? PatientCBHI_ID { get; set; }
            public string? PatientReferalNo { get; set; }
            public string? PatientLetterNo { get; set; }
            public string? PatientExamination { get; set; }
            public string? PaymentReason { get; set; }
            public decimal? PaymentAmount { get; set; } // Use decimal for currency
            public string? PaymentDescription { get; set; }
            public int? PaymentIsCollected { get; set; }
            public DateTime? accedentDate { get; set; }
            public string? policeName { get; set; }
            public string? policePhone { get; set; }
            public string? CarPlateNumber { get; set; }
            public string? CarCertificate { get; set; }

            public string? RegisteredBy { get; set; }
            public DateTime? RegisteredOn { get; set; }
        }

    }
}
