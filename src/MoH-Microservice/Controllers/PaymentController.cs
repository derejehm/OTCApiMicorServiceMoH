
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoH_Microservice.Data;
using MoH_Microservice.Models;
using System.Threading.Tasks.Dataflow;


namespace MoH_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _payment;
        public PaymentController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext payment
            ) {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._payment = payment;
        }

        [HttpPut("Get-all-Payment")]
        public async Task<IActionResult> GetPaymentByDate([FromBody] PaymentbyDate payment)
        {
            var user = await this._userManager.FindByNameAsync(payment.user); // Check if the user exists
            //var usersHttp = HttpContext.GetTokenAsync
            if (user == null)
                return NotFound("User not found");

            if (user.UserType != "Supervisor")
            {
                
                var data = await (from payments in this._payment.Payments
                                  where payments.CreatedOn.Value.Date >= payment.startDate.Value.Date
                                  && payments.CreatedOn.Value.Date <= payment.endDate.Value.Date
                                  && payments.Createdby == user.UserName &&payments.PatientWorkID !="-"
                                  join patients in this._payment.Patients on payments.CardNumber equals patients.PatientCardNumber into rpt_patient
                                  from report in rpt_patient.DefaultIfEmpty()
                                  join workers in this._payment.OrganiztionalUsers on payments.PatientWorkID equals workers.EmployeeID into rpt_worker
                                  from report_workers in rpt_worker.DefaultIfEmpty()
                                  select new
                                  {
                                      RefNo = payments.RefNo,
                                      CardNumber = payments.CardNumber,
                                      HospitalName = payments.HospitalName,
                                      Department = payments.Department,
                                      Channel = payments.Channel,
                                      PatientName = report.PatientName == null ? report_workers.EmployeeName == null ? "" : report_workers.EmployeeName : report.PatientName,
                                      PatientPhone = report.PatientPhoneNumber == null ? report_workers.EmployeePhone == null ? "" : report_workers.EmployeePhone : report.PatientPhoneNumber,
                                      PatientAge = report.PatientAge == null ? "" : report.PatientAge.ToString(),
                                      PatientAddress = report.PatientAddress == null ? "" : report.PatientAddress,
                                      PatientGender = report.PatientGender == null ? "" : report.PatientGender,
                                      PaymentVerifingID = payments.PaymentVerifingID,
                                      PatientLoaction = payments.PatientLoaction,
                                      PatientWorkingPlace = payments.PatientWorkingPlace,
                                      PatientWorkID = payments.PatientWorkID,
                                      Purpose = payments.Purpose,
                                      Amount = payments.Amount,
                                      Description = payments.Description,
                                      IsCollected = payments.IsCollected,
                                      Createdby = payments.Createdby,
                                      CreatedOn = payments.CreatedOn,
                                  }
                            ).ToArrayAsync();

                if (data.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(data).Value);
            }
            else
            {
                var data = await (from payments in this._payment.Payments
                                  where payments.CreatedOn.Value.Date >= payment.startDate.Value.Date
                                  && payments.CreatedOn.Value.Date <= payment.endDate.Value.Date
                                  join patients in this._payment.Patients on payments.CardNumber equals patients.PatientCardNumber into rpt_patient
                                  from report in rpt_patient.DefaultIfEmpty()
                                  join workers in this._payment.OrganiztionalUsers on payments.PatientWorkID equals workers.EmployeeID into rpt_worker
                                  from report_workers in rpt_worker.DefaultIfEmpty()
                                  select new
                                  {
                                      RefNo = payments.RefNo,
                                      CardNumber = payments.CardNumber,
                                      HospitalName = payments.HospitalName,
                                      Department = payments.Department,
                                      Channel = payments.Channel,
                                      PatientName = report.PatientName == null ? report_workers.EmployeeName ==null ? "": report_workers.EmployeeName : report.PatientName,
                                      PatientPhone = report.PatientPhoneNumber == null ? report_workers.EmployeePhone == null ? "" : report_workers.EmployeePhone : report.PatientPhoneNumber,
                                      PatientAge = report.PatientAge == null ? "" : report.PatientAge.ToString(),
                                      PatientAddress = report.PatientAddress == null ? "" : report.PatientAddress,
                                      PatientGender = report.PatientGender == null ? "" : report.PatientGender,
                                      PaymentVerifingID = payments.PaymentVerifingID,
                                      PatientLoaction = payments.PatientLoaction,
                                      PatientWorkingPlace = payments.PatientWorkingPlace,
                                      PatientWorkID = payments.PatientWorkID,
                                      Purpose = payments.Purpose,
                                      Amount = payments.Amount,
                                      Description = payments.Description,
                                      IsCollected = payments.IsCollected,
                                      Createdby = payments.Createdby,
                                      CreatedOn = payments.CreatedOn,
                                  }
                            ).ToArrayAsync();

                if (data.Length <= 0)
                    return NoContent();

                return Ok(new JsonResult(data).Value);

            }


        }



        [HttpPut("payment-by-refno")]
        public async Task<IActionResult> GetPaymentInfoByRefNo([FromBody] PaymentInfo payment)
        {
            var user = await this._userManager.FindByNameAsync(payment?.user); // Check if the user exists
            //var usersHttp = HttpContext.GetTokenAsync
            if (user == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<Payment>().Where(x => x.RefNo == payment.paymentId).ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }

        [HttpPut("payment-by-RefNoInstitution")]
        public async Task<IActionResult> GetPaymentInfoByRefNo([FromBody] PaymentDetailByInstitution payment)
        {
            var user = await this._userManager.FindByNameAsync(payment.user); // Check if the user exists
            if (user == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<Payment>().Where(x => x.RefNo == payment.paymentId && x.HospitalName == payment.hospital).ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();


            return Ok(new JsonResult(PymentInfo).Value);
        }
        [HttpPut("payment-by-cashier")]

        public async Task<IActionResult> GetPaymentInfoByCashier([FromBody] string user)
        {
            var username = await this._userManager.FindByNameAsync(user); // Check if the user exists
            if (username == null)
                return NotFound("User not found");
            try
            {
                var PymentInfo = await this._payment.Set<Payment>().Where(x => x.Createdby == user && EF.Functions.DateDiffDay(x.CreatedOn, DateTime.Now.Date)==0  ).ToArrayAsync();
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

        public async Task<IActionResult> GetPaymentInfoByCardNumber([FromBody] PaymentDetailByCardNo payment)
        {
            var username = await this._userManager.FindByNameAsync(payment.name); // Check if the user exists
            if (username == null)
                return NotFound("User not found");

            var PymentInfo = await this._payment.Set<Payment>().Where(x => x.CardNumber == payment.code).ToArrayAsync();

            if (PymentInfo.Length <= 0)
                return NoContent();

            return Ok(new JsonResult(PymentInfo).Value);
        }
        [HttpPost("add-payment")]
       // [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> InsertPaymentInfo([FromBody] PaymentReg payment)
        {
            var user = await this._userManager.FindByNameAsync(payment.Createdby);
            if (user == null)
                return NotFound("User not found");

            if (user.UserType.ToLower() != "cashier")
                return Unauthorized("You are unautorized to perform payment registration!");
            
            var MaxCardDate = await this._payment.Payments.Where(e => e.CardNumber == payment.CardNumber && e.Purpose.ToLower() == "card")
                    .GroupBy(e => new { e.CardNumber })
                    .Select(e => new { maxregdate = e.Max(e => e.CreatedOn) })
                    .ToArrayAsync();


            if (payment.PaymentType.ToLower() == "credit")
            {
                var worker =await this._payment.OrganiztionalUsers
                    .Where(e => 
                               e.EmployeeID.ToLower() == payment.PatientWorkID.ToLower() 
                            && e.AssignedHospital.ToLower() == user.Hospital.ToLower() 
                            && e.WorkPlace.ToLower()==payment.PatientWorkingPlace.ToLower())
                    .ToArrayAsync();
                if (worker.Length <=0)
                {
                       return NotFound($"Patient <br/>" +
                           $"EmployeeID: {payment.PatientWorkID} <br/>" +
                           $"Working Place:{payment.PatientWorkingPlace} <br/> " +
                           $"is not assigned to this hospital");
                }   
            }

            var RefNo = $"TS_{payment.Hospital.Trim().Substring(0, 2).ToUpper()}-{payment.PaymentType}{DateTime.Now.Microsecond + new Random().Next()}";
            
            try
            {
                foreach (var items in payment.Amount)
                {



                    if (MaxCardDate.Length>0 && (DateTime.Now-MaxCardDate[0].maxregdate).Value.Days <= 15
                        && items.Purpose.ToLower() == "card")
                    {
                        return BadRequest($"Card usage has't yet expired! {(DateTime.Now - MaxCardDate[0].maxregdate).Value.Days}. Days Passed since registration, Last Registration Date : {MaxCardDate[0].maxregdate}");
                    }

                    Payment data = new Payment()
                    {
                        RefNo = RefNo,
                        Purpose = items.Purpose,
                        Type = payment.PaymentType,
                        HospitalName = payment.Hospital,
                        CardNumber = payment.CardNumber,
                        Createdby = payment.Createdby,
                        Amount = items.Amount,
                        PatientLoaction = payment.PatientLocation,
                        PatientWorkingPlace = payment.PatientWorkingPlace,
                        PatientWorkID = payment.PatientWorkID,
                        PaymentVerifingID = payment.PaymentVerifingID,
                        Department = payment.Department,
                        Channel = payment.Channel,
                        Description = payment.Description,
                    };

                    await this._payment.AddAsync<Payment>(data);
                    await this._payment.SaveChangesAsync();
                }
                return Created("/", new { RefNo = RefNo, data = payment });
            }
            catch (Exception ex)
            {
                // Error [Pay0000] = "Insert Failed"
                return BadRequest($"Error [Pay0000] Insert Failed Reason: {ex}");
            }
                       
           
        }

        [HttpPost("add-patient-info")]

        public async Task<IActionResult> addGetPatientInfo([FromBody] PatientReg patient)
        {
            var user = await this._userManager.FindByNameAsync(patient.CreatedBy);
            if (user == null)
                return NotFound("User not found");
            try
            {
                Patient Patient = new Patient
                {
                    PatientCardNumber = patient.PatientCardNumber,
                    PatientAge = patient.PatientAge,
                    PatientAddress = patient.PatientAddress,
                    PatientGender = patient.PatientGender,
                    PatientName = patient.PatientName,
                    PatientPhoneNumber = patient.PatientPhoneNumber,
                    CreatedOn = DateTime.Now,
                    CreatedBy = patient.CreatedBy,
                    UpdatedBy = "",
                    UpdatedOn = null // change today
                };
                await this._payment.AddAsync<Patient>(Patient);
                await this._payment.SaveChangesAsync();

                return Created("/", patient);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Insert PatientData failed! Reason: {ex.StackTrace}");
            }
        }
        [HttpPut("patient-info")]

        public async Task<IActionResult> GetPatientInfo([FromBody] PatientView patient)
        {
            var user = await this._userManager.FindByNameAsync(patient.Cashier);
            if (user == null)
                return NotFound("User not found");

            var patientInfo = await this._payment.Set<Patient>()
                               .Where(e => e.PatientCardNumber == patient.PatientCardNumber)
                             .ToArrayAsync(); // add Hospital name later
            if (patientInfo == null)
                return Ok("No patient with this card number!");

            return Ok(patientInfo);
        }
        [HttpPost("add-service-provider")]
        public async Task<IActionResult> addGetProviderInfo([FromBody] ProvidersMapReg providers)
        {
            var user = await this._userManager.FindByNameAsync(providers.Cashier);
            if (user == null)
                return NotFound("User not found");
            try
            {
                ProvidersMapUsers provider = new ProvidersMapUsers
                {
                    CardNumber = providers.CardNumber,
                    provider = providers.provider,
                    Kebele = providers.Kebele,
                    Goth = providers.Goth,
                    IDNo = providers.IDNo,
                    letterNo = providers.letterNo,
                    Examination = providers.Examination,
                    service = providers.service,
                    Createdby = providers.Cashier,
                    CreatedOn = DateTime.Now,
                    ReferalNo = providers.ReferalNo,
                };
                await this._payment.AddAsync<ProvidersMapUsers>(provider);
                await this._payment.SaveChangesAsync();

                return Created("/", provider);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Insert Provider [ {providers.provider} ] to Patient [{providers.CardNumber}] Mapping failed! Reason: {ex.StackTrace}");
            }
        }

        [HttpPut("get-service-provider")]
        public async Task<IActionResult> GetProviderInfo([FromBody] ProvidersParam providers)
        {
            var user = await this._userManager.FindByNameAsync(providers.user);
            if (user == null)
                return NotFound("User not found");

            var patientInfo = await this._payment.Set<ProvidersMapUsers>()
                              .Where(e => e.CardNumber == providers.cardnumber) // changed on 2025-22-03 req: breket
                            .ToArrayAsync();

            return Ok(patientInfo);
        }

        private class BankLinkList
        {
            
            public string? Institution { get; set; }
            public string? QRLink { get; set; }
        }

    }
}
