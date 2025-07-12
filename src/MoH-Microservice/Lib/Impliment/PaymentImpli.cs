using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Controllers;
using MoH_Microservice.Data;
using MoH_Microservice.Lib.Impliment;
using MoH_Microservice.Lib.Interface;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;
using static MoH_Microservice.Misc.AppReportModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoH_Microservice.Lib.Implimentation
{
    public class PaymentImpli : PatientImpli, PaymentInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly AppQuery _appQuery;
        private readonly RequestImpli _requestImpli1;
        public PaymentImpli(AppDbContext appDbContext,AppQuery appQuery):base(appDbContext)
        {
            this._appDbContext = appDbContext;
            this._appQuery = appQuery;
            this._requestImpli1 = new RequestImpli(appDbContext);
        }
        public async void paymentValidation(AppUser user, PaymentReg payment)
        {
            if (payment.Amount.Count() <= 0)
                throw new Exception("THERE IS NO PAYMENT : AMOUNT IS EMPTY");

            // check if the patient has been registed. 
            var doesPatientExisit = await this._appDbContext.Patients.Where(e => e.MRN == payment.CardNumber).ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
                throw new Exception($"PATIENT IS NOT REGISTEED / ታካሚው አልተመዘገበም !{doesPatientExisit.ToList().Count()}");

            // fetch the max card payment date
            // this will be usefull to check the last time payment has been made for a card / MRN

            if (payment.reverse == false)
            {
                var pay = await this._appDbContext.Payments.Where(w => w.RefNo == payment.PaymentRefNo).ToArrayAsync();
                if (pay.Length > 0)
                {
                    throw new Exception("RECIPT NUMBER ALREADY EXISITS!");
                }
            }
            // check if payment for card has been paid for.

            //if (MaxCardDate.Length <= 0 && !payment.Amount.Any(e=>e.Purpose.ToLower()=="card"))
            //    throw new Exception("CARD PAYMENT REQUIRED / የካርድ ክፍያ አልተከናወነም!");
        }
        public async Task<AppReportModel.PaymentReportDTO[]>? addPayment(AppUser user, PaymentReg payment, List<PatientReuestServicesViewDTO[]> groupPayment)
        {
            var doesPatientExisit = await this._appDbContext.Patients.Where(e => e.MRN == payment.CardNumber).ToArrayAsync();
            RequestImpli requestImpli = new RequestImpli(this._appDbContext);

            if (doesPatientExisit.Length <= 0)
                throw new Exception("PATIENT IS NOT REGISTEED / ታካሚው አልተመዘገበም !");

            var recipt = await this._appDbContext.Payments.Where(w => w.ReceptNo == payment.PaymentRefNo && w.ReceptNo!="").ToArrayAsync();
            if (recipt.Any())
            {
                throw new Exception("Recipt id already used please use another one");
            }
            string worker = await this.addPaymentCreadit(user, payment);
            long CurrentCBHID = await this.addPaymentCBHI(user, payment);
            long? AccedentID = await this.addPaymentTraffic(user, payment);
            var unique = await this.getReference(user, payment);

            var MaxCardDate = await this._appDbContext.Payments.Where(e => e.MRN == payment.CardNumber && e.Purpose.ToLower() == "card")
                    .GroupBy(e => new { e.MRN })
                    .Select(e => new { maxregdate = e.Max(e => e.CreatedOn) })
                    .ToArrayAsync();

            var description = string.Empty;
            if (!payment.DescriptionId.ToString().IsNullOrEmpty())
            {
                var Paymentdescription = await this._appDbContext.PaymentTypeDiscriptions.Where(w => w.Id == payment.DescriptionId).ToArrayAsync();
                description = Paymentdescription.FirstOrDefault() != null ? Paymentdescription?.FirstOrDefault()?.Discription : "";
            }
            
            foreach (var items in payment.Amount)
            {
                var maxCardPaidDate = !MaxCardDate.Any() ? 0 : (DateTime.Now - (MaxCardDate?.FirstOrDefault()?.maxregdate)).Value.Days;

                if (MaxCardDate.Any() && maxCardPaidDate <= 15 && items?.Purpose?.ToLower() == "card")
                {
                    throw new Exception($"DAYS PASSED SINCE LAST REGISTRATION ( {maxCardPaidDate} )\nLAST REGISTRATION DATE : {MaxCardDate[0].maxregdate}");
                }

                Payment data = new Payment()
                {
                    RefNo = unique,
                    ReceptNo = payment.PaymentRefNo,
                    HospitalName = user.Hospital,
                    Department = user.Departement,
                    MRN = payment.CardNumber,
                    Type = payment.PaymentType.ToUpper(),
                    PaymentVerifingID = payment.PaymentVerifingID, //  digital payment id
                    Channel = payment.Channel,
                    PaymentDescriptionId = description.IsNullOrEmpty() ? null : payment.DescriptionId,
                    Description = $"{description}:{payment.Description}",
                    PatientWorkID = worker, // creadit users
                    CBHIID = CurrentCBHID == 0l ? null : CurrentCBHID, // cbhi users
                    AccedentID = AccedentID == 0l ? null : AccedentID, // accedent registration
                    Purpose = items.Purpose.ToUpper(), //
                    Amount = items.Amount,
                    CreatedBy = user.UserName,
                };

                if (!items.groupID.IsNullOrEmpty())
                {

                    if (payment.isLabRequest == true)
                    {
                        // check if the groupId exists in the patient request table if not skip the payment
                        var groupId = await this._appDbContext.PatientRequestedServices
                                 .Where(w => w.groupId == items.groupID)
                                .ToArrayAsync();
                        if (groupId.Length <= 0)
                        {
                            continue;
                        }

                        // check if the payment has aleardy been completed!
                        var groupPaymentExists = await this._appQuery.PatientServiceQuery()
                             .Where(e => e.RequestGroup == items.groupID
                                 && e.PatientCardNumber == payment.CardNumber
                                 && e.RequestedReason == items.Purpose
                                 && e.Paid == true
                                 ).ToArrayAsync();

                        var labRequest = await this._appDbContext.DoctorRequests
                                        .Where(w=> w.MRN==payment.CardNumber && w.groupId==items.groupID && w.status==0)
                                        .ToArrayAsync();

                        if (labRequest.Any())
                        {
                           await requestImpli.payDoctorRequest(user, Convert.ToInt64(items.id), items.groupID,payment.CardNumber);
                        }

                        groupPayment.Add(groupPaymentExists);

                        if (groupPaymentExists.Length <= 0)
                        {
                            await this._appDbContext.PatientRequestedServices
                                .Where(e => (e.isPaid == 0 || e.isPaid == null)
                                    && e.groupId == items.groupID
                                    && e.MRN == payment.CardNumber
                                    && e.purpose == items.Purpose)
                                .ExecuteUpdateAsync(u => u.SetProperty(p => p.isPaid, items.isPaid == true ? 1 : null));
                            data.groupId = items.groupID;
                        }

                        if(!labRequest.Any() && groupPaymentExists.Any())
                        {
                            continue;
                        }
                    }

                    if (payment.isNurseRequest == true)
                    {
                        var groupId = await this._appDbContext.NurseRequests
                             .Where(w => w.groupId == items.groupID)
                            .ToArrayAsync();
                        if (groupId.Length <= 0)
                        {
                            continue;
                        }

                        // check if the payment has aleardy been completed!
                        var groupPaymentExists = await this._appDbContext.NurseRequests
                             .Where(e => e.groupId == items.groupID
                                 && e.MRN == payment.CardNumber
                                 && e.Service == items.Purpose
                                 && e.isPaid == 1
                                 ).ToArrayAsync();

                        if (groupPaymentExists.Length <= 0)
                        {
                            await this._appDbContext.NurseRequests
                                .Where(e => (e.isPaid == 0 || e.isPaid == null)
                                    && e.groupId == items.groupID
                                    && e.MRN == payment.CardNumber
                                    && e.Service == items.Purpose)
                                .ExecuteUpdateAsync(u => u.SetProperty(p => p.isPaid, items.isPaid == true ? 1 : null));
                            data.NurseReqGroupId = items.groupID;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (payment.isPharmacy == true)
                    {
                        /**
                         * Check if the [PHARMACY] request exist 
                         *  if the request does not exist skip to the next payment in the list.
                         * **/
                        var groupId = await this._appDbContext.DoctorRequests
                             .Where(w => w.groupId == items.groupID && w.requestTo.ToLower()=="pharmacy")
                            .ToArrayAsync();
                        if (!groupId.Any())
                        {
                            continue;
                        }

                        // check if the payment has aleardy been completed!
                        var groupPaymentExists = await this._appDbContext.DoctorRequests
                             .Where(e => e.groupId == items.groupID
                                 && e.MRN == payment.CardNumber
                                 //&& e.service == items.Purpose
                                 && e.status == 1 && e.requestTo.ToLower() == "pharmacy"
                                 ).ToArrayAsync();
                        /**
                         * Check if the request is already paid 
                         * if the request is aleardy paid skip to the next payment.
                         * **/
                        if (!groupPaymentExists.Any())
                        {
                            
                            await this._appDbContext.DoctorRequests
                                .Where(e => (e.status==0)
                                    && e.groupId == items.groupID
                                    && e.MRN == payment.CardNumber)
                                .ExecuteUpdateAsync(u => u.SetProperty(p => p.status,1));
                            /**
                                if by any case we want to update each doctor request for pharmacy
                                use this [+] await this._requestImpli1.payDoctorRequest(user, (int)items.id, items.groupID, payment.CardNumber);
                                and  comment the code above 
                             */

                            data.pharmacygroupid = items.groupID;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                /**
                 * Check if the request doesn't have GroupId,or the flag [ispaid] is false then skip the group payment
                 * **/
                
                if (!items.groupID.IsNullOrEmpty() && (items.isPaid == false || items.isPaid == null))
                {
                    await this._appDbContext.SaveChangesAsync();
                    continue;
                }

                await this._appDbContext.AddAsync(data);
                await this._appDbContext.SaveChangesAsync();
            }

            var Paid = await this.getPaymentByRefereceNumber(unique);
            return Paid;
        }
        public async Task<long> addPaymentCBHI(AppUser user, PaymentReg payment)
        {
            if (payment.PaymentType.ToLower() == "cbhi")
            {
                // get the current valid CBHI info
                var result = await this._appDbContext.ProvidersMapPatient
                        .Where(e => e.MRN == payment.CardNumber)
                        .GroupBy(g => new { g.MRN })
                        .Select(s => new {
                            currentRecordID = s.Max(id => id.Id),
                            ExpDate = s.Max(s => s.ExpDate)
                        })
                        .ToArrayAsync();
                if (!result.Any())
                    throw new Exception("PLEASE REGISTER CBHI INFORMATION!");

                if (DateTime.Now.Date > result.FirstOrDefault().ExpDate.Date)
                    throw new Exception($"CBHI ID HAS EXPIRED. ExpDate: [{result.FirstOrDefault().ExpDate.Date}]");

                var CurrentCBHID = result.FirstOrDefault().currentRecordID;
                return CurrentCBHID;
            }
            return 0l;
        }

        public async Task<long?> addPaymentTraffic(AppUser user, PaymentReg payment)
        {
            
            if (payment.PaymentType.ToLower() == "traffic")
            {
                var type = await this._appDbContext.PaymentTypes.Where(w => w.type.ToLower() == payment.PaymentType.ToLower()).ToArrayAsync();
                var typeID = type?.FirstOrDefault()?.Id;
                var payLimit = await this._appDbContext.PaymentPurposeLimits.Where(w => w.type == typeID).ToArrayAsync();

                var timeInHrThreshold = payLimit.FirstOrDefault() == null ? 24 : payLimit?.FirstOrDefault()?.Time;
                var amountThreshold = payLimit.FirstOrDefault() == null ? int.MaxValue : payLimit?.FirstOrDefault()?.Amount;
                // get the current valid Accdents info
                var result = await this._appDbContext.PatientAccedents
                        .Where(e => e.MRN == payment.CardNumber)
                        .GroupBy(g => new { g.MRN })
                        .Select(s => new {
                            LastAccedentID = s.Max(s => s.id),
                            LastAccedentDate = s.Max(s => s.accedentDate),
                            TreatedSince = (DateTime.Now-s.Max(s => s.CreatedOn)).TotalHours,
                            LastTreatedDate = s.Max(s => s.CreatedOn)
                        }).ToArrayAsync();

                if (!result.Any())
                    throw new Exception("PLEASE REGISTER ACCEDENT INFORMATION!");
                var timepassed = (DateTime.Now.ToUniversalTime() - result.FirstOrDefault().LastTreatedDate.ToUniversalTime()).TotalHours;
                if (timepassed >= timeInHrThreshold)
                    throw new Exception($"the time limit set for traffic payment has passed : (LimitSet [{timeInHrThreshold}],currentTimeDifferece [{timepassed}]) ");

                var AccedentID = result?.FirstOrDefault()?.LastAccedentID;

                var paymentThreshold = await this._appDbContext.Payments
                    .Where(w => w.Type.ToLower() == "traffic" && w.MRN == payment.CardNumber && w.AccedentID == AccedentID)
                    .GroupBy(g => new { g.MRN })
                    .Select(s => new { TotalAmount = s.Sum(s => s.Amount) }).ToArrayAsync();

                var paymentTreshold = paymentThreshold.FirstOrDefault().TotalAmount ?? 0;
                var payTotal = payment.Amount.Sum(s => s.Amount) ?? 0;
                paymentTreshold = paymentTreshold + payTotal;
                Console.WriteLine($"Total Payment {paymentTreshold} {payment.Amount.Sum(s => s.Amount)} {amountThreshold}");
                if (paymentTreshold >= amountThreshold)
                    throw new Exception($"Total payment Reached {paymentTreshold} Birr which is above the treshold set {amountThreshold} Birr.");

                return AccedentID ?? 0l;
            }
            return 0l;
        }
        public async Task<string> addPaymentCreadit(AppUser user, PaymentReg payment)
        {
            if (payment.PaymentType.ToLower() == "credit")
            {
                // check if the credit patient has been registered.
                var result = await this._appDbContext.OrganiztionalUsers
                                    .Where(e => e.EmployeeID.ToLower() == payment.PatientWorkID.ToLower()
                                             && e.AssignedHospital.ToLower() == user.Hospital.ToLower()
                                             && e.WorkPlace.ToLower() == payment.organization).ToArrayAsync();

                if (!result.Any())
                    throw new Exception($" CREADIT USER  [ EmployeeID : {payment.PatientWorkID} ] IS NOT ASSIGNED TO THIS HOSPITAL");

               var  worker = result.FirstOrDefault().EmployeeID;

                return worker;
            }
            return null;
        }

        public async Task<AppReportModel.PaymentReportDTO[]> cancelPayment(AppUser user, PaymentReg payment)
        {
            var findRef = await this._appDbContext.Payments.Where(w => w.RefNo == payment.PaymentRefNo).ToArrayAsync();

            if (!findRef.Any())
                throw new Exception("REFERENCE ID REQUESTED DOES NOT EXIST.");

            var reverse = await this._appDbContext.Payments
                .Where(w => w.RefNo == payment.PaymentRefNo
                    && (w.IsReversed == 0 || w.IsReversed == null))
                .ExecuteUpdateAsync(u => u
                   .SetProperty(p => p.IsReversed, 1)
                   .SetProperty(p => p.Reversedby, user.UserName)
                   .SetProperty(p => p.ReversedOn, DateTime.Now)
                   .SetProperty(p => p.ReversedDescription, payment.Description)
                );

            if (reverse <= 0)
                throw new Exception("FAILED TO REVERSE THE PAYMENT. PAYMENT NOT FOUND OR IS ALREADY REVERSED");

            var paymentD = await this._appQuery.PaymentQuery().Where(e => e.ReferenceNo == payment.PaymentRefNo).ToArrayAsync();

            return paymentD;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getAllPayment(AppUser user, PaymentbyDate payment)
        {
            IQueryable<AppReportModel.PaymentReportDTO> conditions = null;

            if (payment.reversedOnly != null)
            {
                conditions = this._appQuery.PaymentQuery().Where(e =>
                                                   e.RegisteredOn.Value.Date >= payment.startDate &&
                                                   e.RegisteredOn.Value.Date <= payment.endDate && e.IsReversed == payment.reversedOnly);
            }
            else
            {

                conditions = this._appQuery.PaymentQuery().Where(e =>
                                                 e.RegisteredOn.Value.Date >= payment.startDate &&
                                                 e.RegisteredOn.Value.Date <= payment.endDate);
            }

            if (user.UserType != "Supervisor")
            {

                var data = await conditions.Where(e => e.RegisteredBy.ToLower() == user.UserName.ToLower()).ToArrayAsync();
                return data;
            }
            else
            {
                var data = await conditions.ToArrayAsync();

                return data;

            }

        }

        public async Task<AppReportModel.PaymentSummaryReport[]> getAllRptPayment(AppUser user, PaymentbyDate payment)
        {
            IQueryable<AppReportModel.PaymentReportDTO> conditions = null;

            if (payment.reversedOnly != null)
            {
                conditions = this._appQuery.PaymentQuery().Where(e =>
                                                   e.RegisteredOn.Value.Date >= payment.startDate &&
                                                   e.RegisteredOn.Value.Date <= payment.endDate && e.IsReversed == payment.reversedOnly);
            }
            else
            {

                conditions = this._appQuery.PaymentQuery().Where(e =>
                                                 e.RegisteredOn.Value.Date >= payment.startDate &&
                                                 e.RegisteredOn.Value.Date <= payment.endDate);
            }

            if (user.UserType.ToLower() != "supervisor")
            {

                var data = await conditions.Where(e => e.RegisteredBy.ToLower() == user.UserName.ToLower()).GroupBy(g => new {
                    g.ReferenceNo,g.PatientCardNumber, g.PatientName,g.PatientVisiting,
                    g.PatientAge,g.PatientGender,g.PatientKebele,g.PatientsGoth,
                    g.PatientReferalNo,g.PatientCBHI_ID,g.PatientType, g.PaymentReason,
                    g.PaymentType,g.PatientWorkingPlace,g.PatientWorkID,g.PatientWoreda,
                    g.accedentDate, g.policeName,g.policePhone,g.CarPlateNumber,
                    g.RegisteredBy,g.RegisteredOn,

                }).Select(s => new PaymentSummaryReport
                {
                                         ReferenceNumber = s.FirstOrDefault().ReferenceNo,
                                         TreatmentDate = s.FirstOrDefault().RegisteredOn,
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

                return data;
            }
            else
            {
                var data = await conditions.GroupBy(g => new {
                    g.ReferenceNo,g.PatientCardNumber, g.PatientName,g.PatientVisiting,
                    g.PatientAge,g.PatientGender,g.PatientKebele,g.PatientsGoth,
                    g.PatientReferalNo,g.PatientCBHI_ID,g.PatientType, g.PaymentReason,
                    g.PaymentType,g.PatientWorkingPlace,g.PatientWorkID,g.PatientWoreda,
                    g.accedentDate, g.policeName,g.policePhone,g.CarPlateNumber,
                    g.RegisteredBy,g.RegisteredOn,
                }).Select(s => new PaymentSummaryReport
                {
                    ReferenceNumber = s.FirstOrDefault().ReferenceNo,
                    TreatmentDate = s.FirstOrDefault().RegisteredOn,
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

                return data;
            }
        }

        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByRefereceNumber(string refno, bool? reversed)
        {

            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.ReferenceNo == refno && x.IsReversed == reversed).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByRefereceNumber(string refno)
        {

            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.ReferenceNo == refno).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByCashier(string username, bool? reversed)
        {

            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.RegisteredBy == username && x.IsReversed == reversed).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByCashier(string username)
        {

            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.RegisteredBy == username && x.RegisteredOn==DateTime.Now.Date).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByPatientCardNumber(string cardnumber)
        {
            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.PatientCardNumber == cardnumber).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByPhonenumber(string phoneNumber)
        {
            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.PatientPhone == phoneNumber).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByType(string type)
        {
            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.PaymentType.ToLower() == type.ToLower()).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByType(string cardnumber,string type)
        {
            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.PaymentType.ToLower() == type.ToLower()).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByType(string type,bool? reversed)
        {
            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.PaymentType.ToLower() == type.ToLower() && x.IsReversed == reversed).ToArrayAsync();
            return paymentInfo;
        }
        public async Task<AppReportModel.PaymentReportDTO[]> getPaymentByName(string Name)
        {
            var paymentInfo = await this._appQuery.PaymentQuery().Where(x => x.PatientPhone.ToLower() == Name.ToLower()).ToArrayAsync();
            return paymentInfo;
        }

        public async Task<AppReportModel.PaymentReportDTO> getReversedPayment(AppUser user, PaymentReg payment)
        {
            throw new NotImplementedException();
        }

        public async Task<long> getNextSequence() {
            long nextSequenceValue;
            using (var command = this._appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT NEXT VALUE FOR rapyment_recipt_number";
                this._appDbContext.Database.OpenConnection();

                var result = await command.ExecuteScalarAsync();
                nextSequenceValue = Convert.ToInt64(result);
            }

            return nextSequenceValue;
        }

        public async Task<string> getReference(AppUser user,PaymentReg payment)
        {
            var sequence = await this.getNextSequence();
            var unique = $"RCPT-{user.Hospital.Trim().Substring(0, 2).ToUpper()}{payment.PaymentType.ToUpper().Replace(" ", "").Trim().Substring(0, 4)}";
            unique = $"{unique}-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "").ToUpper()}{sequence}";
            return unique ;
        }
    }
}
