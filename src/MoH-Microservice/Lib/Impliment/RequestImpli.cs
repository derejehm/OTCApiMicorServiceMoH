using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Lib.Interface;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.DTO;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;

namespace MoH_Microservice.Lib.Impliment
{
    public class RequestImpli : RequestInterface
    {
        private readonly AppDbContext _dbContext;
        private readonly AppQuery _appQuery;

        public RequestImpli(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._appQuery = new AppQuery(this._dbContext);
        }
        public async Task<string> addLabRequest(AppUser user,PatientRequestedServicesReg PatientRequest)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == PatientRequest.PatientCardNumber).AsNoTracking().ToArrayAsync();
            if (!doesPatientExisit.Any())
            {
                throw new Exception("PATIENT DOES NOT EXIST");
            }

            var groupid = $"{user.UserType.ToLower()}-{Guid.NewGuid()}"; // groupid for identification purposes

            foreach (var service in PatientRequest.RequestedServices)
            {
                PatientRequestedServices services = new PatientRequestedServices
                {
                    groupId = groupid,
                    MRN = PatientRequest.PatientCardNumber,
                    service = service,
                    purpose = PatientRequest.purpose,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    isPaid = 0,
                    isComplete = 0,
                    UpdatedBy = null,
                    UpdatedOn = null
                };
                await this._dbContext.PatientRequestedServices.AddAsync(services);
            }
            await this._dbContext.SaveChangesAsync();
            return groupid;
        }
        public async Task<bool> cancelLabRequest(PatientRequestedServicesDelete patient)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST.");
            }
            var patientServ = await this._dbContext
                    .PatientRequestedServices
                    .Where(w => (w.isPaid == 0 || w.isPaid == null) &&
                    w.isComplete == 0 &&
                    w.MRN == patient.PatientCardNumber &&
                    w.groupId == patient.groupID &&
                    w.purpose == patient.purpose).ExecuteDeleteAsync();

            if (patientServ > 0)
                return true;
            else
                // the request can only be canceled if it is not paid.
                throw new Exception("REQUEST FAILD TO CANCEL  :: REQUEST NOT FOUND");
        }
        public async  Task<bool> cancelNurseRequest(PatientRequestedServicesDelete patient)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST.");
            }

            var patientServ = await this._dbContext
                    .NurseRequests
                    .Where(w => (w.isPaid == 0 || w.isPaid == null) &&
                    w.isComplete == 0 &&
                    w.MRN == patient.PatientCardNumber &&
                    w.groupId == patient.groupID &&
                    w.Service == patient.purpose).ExecuteDeleteAsync();

            if (patientServ > 0)
                return true;
            else
                // the reqiest can only be canceled if it is not paid.
                throw new Exception("REQUEST FAILD TO CANCEL :: REQUEST NOT FOUND");
        }
        public async Task<bool> completeLabRequest(AppUser user,PatientRequestedServicesViewOne patient)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST.");
            }
            var patientServ = await this._dbContext
                    .PatientRequestedServices
                    .Where(w => w.isPaid == 1 && w.isComplete == 0 && w.MRN == patient.PatientCardNumber && w.groupId == patient.groupID)
                    .ExecuteUpdateAsync(e =>
                    e.SetProperty(p => p.isComplete, 1)
                     .SetProperty(p => p.UpdatedBy, user.UserName)
                     .SetProperty(p => p.UpdatedOn, DateTime.Now)
                    );
            if (patientServ > 0)
                return true;
            else
                throw new Exception("REQUEST IS YET TO BE PAID");
        }
        public async Task<bool> completeNurseRequest(AppUser user, PatientRequestedServicesViewOne patient)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST.");
            }
            var patientServ = await this._dbContext
                    .NurseRequests
                    .Where(w => w.isPaid == 1 && w.isComplete == 0 && w.MRN == patient.PatientCardNumber && w.groupId == patient.groupID)
                    .ExecuteUpdateAsync(e =>
                    e.SetProperty(p => p.isComplete, 1)
                     .SetProperty(p => p.UpdatedBy, user.UserName)
                     .SetProperty(p => p.UpdatedOn, DateTime.Now)
                    );
            if (patientServ > 0)
                return true;
            else
                throw new Exception("REQUEST FAILD TO UPDATE  :: REQUEST IS YET TO BE PAID");
        }
        public async Task<PatientReuestServicesDisplayDTO[]> getLabRequest(PatientRequestedServicesViewOne patient)
        {
            var patientServ = this._appQuery
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
                    RquestedServices = s.Select(s => new patientRequestServiceOut { service = s.RquestedServices, amount = s.Price, catagory = s.RequestedReason }).ToList(),
                    RequestedReason = s.FirstOrDefault().RequestedReason,
                    RequestedBy = s.FirstOrDefault().RequestedBy,
                    createdOn = s.FirstOrDefault().createdOn.Date

                }).OrderByDescending(o => o.Paid); ;

            var result = await patientServ.Where(e => e.IsCompleted == patient.isComplete).ToArrayAsync();

            if (patient.PatientCardNumber != null && patient.groupID != null)
            {
                result = await patientServ
                .Where(e => e.PatientCardNumber.ToLower() == patient.PatientCardNumber.ToLower()
                            && e.IsCompleted == patient.isComplete
                            && e.RequestGroup == patient.groupID).ToArrayAsync();
            }

            return result;
        }
        public async Task<PatientReuestServicesDisplayDTO[]> getLabRequest(AppUser user,PatientRequestedServicesViewOne patient)
        {
            var patientServ = await this._appQuery
                    .PatientServiceQuery()
                    .Where(e => (e.Paid == false || e.Paid == null) && e.Complete==false)
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
                        PatientCardNumber = s.FirstOrDefault()!=null? s.FirstOrDefault().PatientCardNumber:null,
                        PatientFirstName = s.FirstOrDefault() != null ? s.FirstOrDefault().PatientFirstName:null,
                        PatientLastName = s.FirstOrDefault() != null ? s.FirstOrDefault().PatientLastName : null,
                        PatientMiddleName = s.FirstOrDefault() != null ? s.FirstOrDefault().PatientMiddleName : null,
                        PatientAge = s.FirstOrDefault() != null ? s.FirstOrDefault().PatientAge : null,
                        PatientGender = s.FirstOrDefault() != null ? s.FirstOrDefault().PatientGender : null,
                        Paid = s.FirstOrDefault() != null ? s.FirstOrDefault().Paid : null,
                        IsCompleted = s.FirstOrDefault() != null ? s.FirstOrDefault().Complete : null,
                        RequestedCatagories = s.GroupBy(g => new { RequestGroup = g.RequestGroup, RequestedReason = g.RequestedReason, Paid = g.Paid })
                        .Select(c => new patientRequestOut
                        {
                            groupID = c.FirstOrDefault().RequestGroup,
                            amount = c.Sum(s => s.Price),
                            purpose = c.FirstOrDefault().RequestedReason,
                            isPaid = !c.FirstOrDefault().Paid,
                        }).ToList(),
                        TotalPrice = s.Sum(s => s.Price),
                        NoRequestedServices = s.Select(s => s.RquestedServices).Count(),
                        RquestedServices = s.Select(s => new patientRequestServiceOut { service = s.RquestedServices, amount = s.Price, catagory = s.RequestedReason }).ToList(),
                        RequestedBy = s.FirstOrDefault()!=null?s.FirstOrDefault().RequestedBy  :null,
                        createdOn = s.FirstOrDefault() != null ? s.FirstOrDefault().createdOn.Date :DateTime.MinValue,
                    }).OrderByDescending(o => o.Paid).ToArrayAsync();

            return patientServ;
        }
        public async Task<NurseRequestDTO[]> getNurseRequest(NurseRequestGetOne patient)
        {
            var patientServ = this._appQuery
                                .NurseRequesteQuery()
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
                                    e.RequestedBy,
                                    e.createdOn.Date
                                }).Select(s => new NurseRequestDTO
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
                                    RquestedServices = s.Select(c => new RequestedServices
                                    {
                                        groupID = c.RequestGroup,
                                        services = c.RquestedServices,
                                        Amount = c.Amount,
                                        Price = c.Price,

                                    }).ToList(),
                                    CreatedBy = s.FirstOrDefault().RequestedBy,
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
            return result;
        }
        public async Task<NurseRequestDTO[]> getNurseRequest(AppUser user)
        {
            var patientServ = await this._appQuery
                    .NurseRequesteQuery()
                    .Where(e => (e.Paid == false || e.Paid == null) && e.Complete == false)
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
                    }).Select(s => new NurseRequestDTO
                    {
                        PatientCardNumber = s.FirstOrDefault().PatientCardNumber,
                        PatientFirstName = s.FirstOrDefault().PatientFirstName,
                        PatientLastName = s.FirstOrDefault().PatientLastName,
                        PatientMiddleName = s.FirstOrDefault().PatientMiddleName,
                        PatientAge = s.FirstOrDefault().PatientAge,
                        PatientGender = s.FirstOrDefault().PatientGender,
                        Paid = s.FirstOrDefault().Paid,
                        IsCompleted = s.FirstOrDefault().Complete,
                        RquestedServices = s.Select(c => new RequestedServices
                        {
                            groupID = c.RequestGroup,
                            services = c.RquestedServices,
                            Price = (c.Price * c.Amount),
                            Amount = c.Amount
                        }).ToList(),
                        TotalPrice = s.Sum(s => (s.Price * s.Amount)),
                        NoRequestedServices = s.Select(s => s.RquestedServices).Count(),
                        CreatedBy = s.FirstOrDefault().RequestedBy,
                        createdOn = s.FirstOrDefault().createdOn.Date
                    }).OrderByDescending(o => o.Paid).ToArrayAsync();

            return patientServ;
        }
        public Task<string> modifyLabRequest()
        {
            throw new NotImplementedException();
        }
        public Task<string> modifyNurseRequest()
        {
            throw new NotImplementedException();
        }
        public async Task<string> addNurseRequest(AppUser user, NurseRequestReg nurseRequest)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == nurseRequest.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST");
            }

            var groupid = $"{user.UserType.ToLower()}-{Guid.NewGuid()}"; // groupid for identification purposes

            foreach (var service in nurseRequest.Services)
            {
                NurseRequest services = new NurseRequest
                {
                    groupId = groupid,
                    MRN = nurseRequest.PatientCardNumber,
                    AdmissionDate = nurseRequest.AdmissionDate,
                    DischargeDate = nurseRequest.DischargeDate,
                    hasMedication = nurseRequest.hasMedication,
                    PatientCondition = nurseRequest.PatientCondition,
                    Service = service.Service,
                    Amount = service.Amount,
                    Price = service.Price,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    isPaid = 0,
                    isComplete = 0,
                    UpdatedBy = null,
                    UpdatedOn = null
                };
                await this._dbContext.AddAsync(services);
            }
            await this._dbContext.SaveChangesAsync();

            return groupid;
        }
        public async Task<DoctorRequestDTO2[]> orderDoctorRequest(AppUser user, Models.Form.DoctorRequest doctor)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == doctor.patientCardnumber).AsNoTracking().ToArrayAsync();
            if (!doesPatientExisit.Any())
            {
                throw new Exception("Patient doesn't exist");
            }
            var group = Guid.NewGuid().ToString();
            foreach(var item in doctor.requestItems)
            {
                Models.Database.DoctorRequest request = new Models.Database.DoctorRequest
                {
                    groupId = group,
                    MRN = doctor.patientCardnumber,
                    requestTo = doctor.reqestedTo,
                    requestFrom = user.Departement,
                    service = item.prodedures,
                    count = item.prodeduresCount,
                    price = item.price==null?0: (decimal)item.price,
                    measurment = item.measurment,
                    duration = item.duration,
                    instruction=item.instruction,
                    catagory = doctor.catagory,
                    CreatedBy = user.UserName
                };
                await this._dbContext.AddAsync(request);
            }
            await this._dbContext.SaveChangesAsync();
            var result = await this.getDoctorRequestOne(user,group);
            
            return result;
        }
        public async Task<bool> payDoctorRequest(AppUser user, long id,string groupid,string mrn)
        {
            var paid = await this._dbContext.DoctorRequests
                .Where(w => w.id == id && w.status==0 && w.groupId==groupid && w.MRN==mrn)
                .ExecuteUpdateAsync(w => w
                .SetProperty(p => p.status, 1)
                .SetProperty(p => p.UpdatedBy, user.UserName)
                .SetProperty(p => p.UpdatedOn, DateTime.Now));

            if (paid<=0)
            {
                throw new Exception("Order not found");
            }
            return true;
        }
        public async Task<bool> pickDoctorRequest(AppUser user, long id)
        {
            var paid = await this._dbContext.DoctorRequests.Where(w => w.id == id && w.status == 1)
                .ExecuteUpdateAsync(w => w
                    .SetProperty(p => p.status, 2)
                    .SetProperty(p => p.UpdatedBy, user.UserName)
                    .SetProperty(p => p.UpdatedOn, DateTime.Now)
                    );

            if (paid <= 0)
            {
                throw new Exception("Paid order not found");
            }
            return true;
        }
        public async Task<bool> proccessDoctorRequest(AppUser user, long id)
        {
            var paid = await this._dbContext.DoctorRequests.Where(w => w.id == id && w.status == 2)
               .ExecuteUpdateAsync(w => w
                .SetProperty(p => p.status, 3)
                .SetProperty(p => p.UpdatedBy, user.UserName)
                .SetProperty(p => p.UpdatedOn, DateTime.Now)
                );

            if (paid <= 0)
            {
                throw new Exception("Picked order not found");
            }
            return true;
        }
        public async Task<bool> completeDoctorRequest(AppUser user, long id)
        {
            var paid = await this._dbContext.DoctorRequests
               .Where(w => w.id == id && w.status == 3)
               .ExecuteUpdateAsync(w => w
                   .SetProperty(p => p.status, 4)
                   .SetProperty(p => p.UpdatedBy, user.UserName)
                   .SetProperty(p => p.UpdatedOn, DateTime.Now)
               );

            if (paid <= 0)
            {
                throw new Exception("Proccessed order not found");
            }
            return true;
        }
        public async Task<bool> cancelDoctorRequest(AppUser user, long id)
        {
            var paid = await this._dbContext.DoctorRequests
               .Where(w => w.id == id && w.status == 0)
               .ExecuteUpdateAsync(w => w
                   .SetProperty(p => p.status, -3)
                   .SetProperty(p => p.UpdatedBy, user.UserName)
                   .SetProperty(p => p.UpdatedOn, DateTime.Now));
            if (paid <= 0)
            {
                throw new Exception("Proccessed order not found");
            }
            return true;
        }
        public async Task<bool> cancelDoctorRequest(AppUser user, string id)
        {
            var paid = await this._dbContext.DoctorRequests
               .Where(w => w.groupId == id && w.status == 0)
               .ExecuteUpdateAsync(w => w
                   .SetProperty(p => p.status, -3)
                   .SetProperty(p => p.UpdatedBy, user.UserName)
                   .SetProperty(p => p.UpdatedOn, DateTime.Now));
            if (paid <= 0)
            {
                throw new Exception("Proccessed order not found");
            }
            return true;
        }
        //--
        public async Task<bool> failedDoctorRequest(AppUser user, long id)
        {
            var paid = await this._dbContext.DoctorRequests.Where(w => w.id == id && w.status == 3)
               .ExecuteUpdateAsync(
                w => w.SetProperty(p => p.status, -1)
                .SetProperty(p=>p.UpdatedBy,user.UserName)
                .SetProperty(p=>p.UpdatedOn,DateTime.Now)
                );

            if (paid <= 0)
            {
                throw new Exception("Proccessed order not found");
            }
            return true;
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequest(AppUser user)
        {
            var request = await this._appQuery.DoctorRequestSummary(user.Departement);
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request;
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequestOne(AppUser user,string group)
        {
            var request = await this._appQuery.DoctorRequestSummary(user.Departement);
            request = request.Where(w => w.RequestedBy == user.UserName && w.RequestGroup==group).ToArray();
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request;
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequestLab_v()
        {
            var request = await this._appQuery.DoctorRequestSummary();
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request.Where(w=>w.RequestedDepartment.ToLower()=="lab").ToArray();
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequestPharma_v()
        {
            var request = await this._appQuery.DoctorRequestSummary_Pharma();
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request.Where(w => w.RequestedDepartment.ToLower() == "pharmacy").ToArray();
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequestPharma_paid()
        {
            var request = await this._appQuery.DoctorRequestSummary_Pharma_paid();
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request;
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequestlab_paid()
        {
            var request = await this._appQuery.DoctorRequestSummary_lab_paid();
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request;
        }
        public async Task<DoctorRequestDTO2[]> getDoctorRequestCashier_v()
        {
            var request = await this._appQuery.DoctorRequestSummaryPayable(0);
            if (!request.Any())
            {
                throw new Exception("No request found");
            }
            return request;
        }
    }   
}
