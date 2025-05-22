
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models;
using MoH_Microservice.Query;
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

                    var data = await this.PaymentQuery().Where(e => e.RegisteredBy.ToLower() == user.UserName.ToLower()).ToArrayAsync();

                    if (data.Length <= 0)
                        return NoContent();
                    return Ok(new JsonResult(data).Value);
                }
                else
                {
                    var data = await this.PaymentQuery().ToArrayAsync();

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
        [HttpPut("rpt-all-Payment")]
        public async Task<IActionResult> RptPaymentByDate([FromBody] PaymentbyDate payment, [FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();
                if (user.UserType.ToLower() != "supervisor")
                {

                    var data = await this.PaymentQuery()
                                         .Where(e => e.RegisteredBy.ToLower() == user.UserName.ToLower())
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
                                             //g.PaymentReason
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
                                             g.PaymentReason
                                         })
                                         .Select(s => new
                                         {
                                             CardNumber = s.Select(s => s.PatientCardNumber),
                                             Name = s.Select(s => s.PatientName),
                                             VisitingDate = s.Select(s => s.PatientVisiting),
                                             Age = s.Select(s => s.PatientAge),
                                             Gender = s.Select(s => s.PatientGender),
                                             Kebele = s.Select(s => s.PatientKebele),
                                             Goth = s.Select(s => s.PatientsGoth),
                                             ReferalNo = s.Select(s => s.PatientReferalNo),
                                             IDNo = s.Select(s => s.PatientCBHI_ID),
                                             PatientType = s.Select(s => s.PatientType),
                                             PaymentReason = s.Select(s => s.PaymentReason),
                                             TotalPaid = s.Sum(s => s.PaymentAmount)
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
        //[Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> InsertPaymentInfo([FromBody] PaymentReg payment,[FromHeader] string Authorization)
        {

            try
            {
                var user = await this._tokenValidate.setToken(Authorization.Split(" ")[1]).db_recorded();

                if (user.UserType.ToLower() != "cashier")
                    throw new Exception("YOU CAN'T PERFORM PAYMENT");

                // check if the patient has been registed. 
                var doesPatientExisit = await this._payment.Patients.Where(e => e.MRN == payment.CardNumber).ToArrayAsync();
                if (doesPatientExisit.Length <= 0)
                    throw new Exception("PATIENT IS NOT REGISTEED / ታካሚው አልተመዘገበም !");

                // fetch the max card payment date
                // this will be usefull to check the last time payment has been made for a card / MRN

                var MaxCardDate = await this._payment.Payments.Where(e => e.MRN == payment.CardNumber && e.Purpose.ToLower() == "card")
                        .GroupBy(e => new { e.MRN })
                        .Select(e => new { maxregdate = e.Max(e => e.CreatedOn) })
                        .ToArrayAsync();
                // check if payment for card has been paid for.
                
                //if (MaxCardDate.Length <= 0 && !payment.Amount.Any(e=>e.Purpose.ToLower()=="card"))
                //    throw new Exception("CARD PAYMENT REQUIRED / የካርድ ክፍያ አልተከናወነም!");

                // check if the credit patient has been registered.
                var worker = await this._payment.OrganiztionalUsers
                    .Where(e => e.EmployeeID.ToLower() == payment.PatientWorkID.ToLower()
                             && e.AssignedHospital.ToLower() == user.Hospital.ToLower()  
                             && e.WorkPlace.ToLower()==payment.organization).ToArrayAsync();

                // check for the worker if credit payment issued 
                if (payment.PaymentType.ToLower() == "credit" && worker.Length <=0)
                    throw new Exception($" CREADIT USER  [ EmployeeID : {payment.PatientWorkID} ] IS NOT ASSIGNED TO THIS HOSPITAL");  

                // get the current valid CBHI info
                var CurrentCBHID = await this._payment.ProvidersMapPatient
                    .Where(e => e.MRN == payment.CardNumber)
                    .GroupBy(g => new { g.MRN })
                    .Select(s => new { currentRecordID = s.Max(id => id.Id) })
                    .ToArrayAsync();

                // if cbhi payment issued check if the user is registed for cbhi service
                if (payment.PaymentType.ToLower() == "cbhi" && CurrentCBHID.Length <= 0)
                   throw new Exception("PLEASE REGISTER CBHI INFORMATION!");
            
                // generate a refno

                var RefNo = $"{user.Hospital.Trim().Substring(0, 2).ToUpper()}{payment.CardNumber}{payment.PaymentType.ToUpper()}{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Millisecond}{DateTime.Now.Microsecond}{new Random().Next(DateTime.Now.Microsecond,Int32.MaxValue)}";
                List<PatientReuestServicesViewDTO[]> groupPayment = new List<PatientReuestServicesViewDTO[]>();
                if (payment.Amount.Count() <= 0)
                    throw new Exception("THERE IS NO PAYMENT : AMOUNT IS EMPTY");

                    foreach (var items in payment.Amount)
                    {

                        if (MaxCardDate.Length > 0
                            && (DateTime.Now - MaxCardDate[0].maxregdate).Value.Days <= 15
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
                            PatientWorkID = worker.Length >0 ? worker[0]?.WorkPlace: null, // creadit users
                            CBHIID = CurrentCBHID.Length>0? CurrentCBHID[0]?.currentRecordID:null, // cbhi users
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
                                .Where(e => e.isPaid == 0
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
                var PaymentDetails = await this.PaymentQuery().Where(e => e.ReferenceNo == RefNo).ToArrayAsync();
                return Created("/", new {   RefNo = RefNo, 
                                            data = PaymentDetails, 
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

        [HttpPut("get-service-provider")]
        public async Task<IActionResult> GetProviderInfo([FromBody] ProvidersParam providers, [FromHeader] string Authorization)
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
                    var patientInfo = await this._payment.Set<ProvidersMapUsers>()
                                .Where(e => e.MRN == providers.cardnumber && e.Id == CurrentCBHID[0].currentRecordID)
                                .ToArrayAsync();
                    return Ok(patientInfo);
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(new {msg=ex.Message});
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
            public string? RegisteredBy { get; set; }
            public DateTime? RegisteredOn { get; set; }
        }

    }
}
