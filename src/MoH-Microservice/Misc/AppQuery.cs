
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Models;

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
                             PatientAge = DateTime.Now.Year - patients.PatientDOB.Year,
                             RequestGroup = serviceMap.groupId,
                             PatientGender = patients.gender,
                             RquestedServices = purposeMap.Purpose,
                             RequestedReason = serviceMap.purpose,
                             Price = purposeMap.Amount,
                             RequestedBy = serviceMap.createdBy,
                             createdOn = serviceMap.createdOn,
                             Paid = serviceMap.isPaid == 0 ? false : serviceMap.isPaid == 1 ? true : null,
                             Complete = serviceMap.isComplete == 0 ? false : serviceMap.isPaid == 1 ? true : null
                         }).AsNoTracking();

            return query;
        }

    }
}
