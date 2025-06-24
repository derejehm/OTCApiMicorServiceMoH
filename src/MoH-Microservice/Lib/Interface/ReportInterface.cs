using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.DTO;

namespace MoH_Microservice.Lib.Interface
{
    public interface ReportInterface
    {
        public  Task<ReportDTO[]> createReport(AppUser user, Models.Form.Report report);
        public  Task<Models.Database.ReportSource[]> sourceReport(AppUser user, Models.Form.ReportSource report);
        public  Task<List<Dictionary<string, object>>> excuteReport(AppUser user, Models.Form.ReportExcute report);
        public  Task<ReportStoreDTO[]> enableAccessReport(string uuid);
        public  Task<ReportStoreDTO[]> enableAccessReport(AppUser user, Models.Form.ReportAccess report);
        public  Task<ReportDTO[]> getReport();
        public  Task<ReportDTO[]> getReport(string uuid);
        public  Task<ReportAccessDTO[]> getReportAccess();
        public  Task<ReportAccessDTO[]> getReportAccess(AppUser user, string uuid);
        public  Task<ReportStoreDTO[]> getReportStore();
        public  Task<ReportStoreDTO[]> getReportStore(AppUser user);
        public  Task<ReportStoreDTO[]> getReportStore(string uuid);
        public  Task<ReportStoreDTO[]> getReportStore(AppUser user, string uuid);
        public  Task<Models.Database.ReportSource[]> getSource();
        public  Task<Models.Database.ReportSource[]> getSource(string name);
        public  Task<bool> removeReport(string uuid);
        public  Task<ReportStoreDTO[]> saveReport(AppUser user, string uuid);
        public  Task<ReportDTO[]> updateReport(AppUser user, string uuid, Models.Form.Report report);
    }
}
