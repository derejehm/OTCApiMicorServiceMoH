
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;
using static MoH_Microservice.Controllers.PaymentController;

namespace MoH_Microservice.Lib.Interface
{
    public interface PaymentInterface
    {
        public void paymentValidation(AppUser user, PaymentReg payment);
        // adding payment
        /*
         @ Payment information Interface
         */
        public Task<AppReportModel.PaymentReportDTO[]> addPayment(AppUser user, PaymentReg payment,List<PatientReuestServicesViewDTO[]> groupPayment);
        public Task<long> addPaymentCBHI(AppUser user, PaymentReg payment);
        public Task<long?> addPaymentTraffic(AppUser user, PaymentReg payment);
        public Task<string> addPaymentCreadit(AppUser user, PaymentReg payment);
        //--
        public Task<AppReportModel.PaymentReportDTO[]> cancelPayment(AppUser user, PaymentReg payment);
        public Task<AppReportModel.PaymentReportDTO> getReversedPayment(AppUser user, PaymentReg payment);
        //--
        public Task<AppReportModel.PaymentReportDTO[]> getAllPayment(AppUser user, PaymentbyDate payment); // payment report
        public Task<AppReportModel.PaymentSummaryReport[]> getAllRptPayment(AppUser user, PaymentbyDate payment); // payment report 
        
        // using payment inforamation
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByRefereceNumber(string refno);
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByCashier(string username);
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByRefereceNumber(string refno, bool? reversed); 
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByCashier(string username, bool? reversed);

        // using patient information
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByPatientCardNumber(string cardnumber);
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByPhonenumber(string phoneNumber);
        public Task<AppReportModel.PaymentReportDTO[]> getPaymentByName(string phoneNumber);
    }
}
