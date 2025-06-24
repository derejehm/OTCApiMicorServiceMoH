using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Data;
using MoH_Microservice.Lib.Interface;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.DTO;
using MoH_Microservice.Query;
using System.Data.Common;

namespace MoH_Microservice.Lib.Impliment
{
    public class ReportImpli : ReportInterface
    {
        private readonly AppDbContext _dbContext;
        private readonly AppQuery _appQuery;

        public ReportImpli(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._appQuery = new AppQuery(this._dbContext);
        }
        public  async Task<ReportDTO[]> createReport(AppUser user, Models.Form.Report report)
        {

            AppReportGenerator generate = new AppReportGenerator(report,report.command);
            var reportid = Guid.NewGuid().ToString();
            Report create = new Report
            {
                title = report.title,
                category = report.category,
                description = report.description,
                uuid = reportid,
                summary = report.summary,
                publisher = report.publisher,
                command = generate.getSqlCommand(),
                Columns = string.Join(",",report.command.Columns),
                source = report.command.Source,
                enabled = report.enabled,
                grouped = report.command.enablegroup,
                enableCount = report.command.enablegroup,
                CreatedBy=user.UserName
            };
            await this._dbContext.AddAsync(create);
            foreach (var item  in report.command.filters)
            {
                ReportFilters filters = new ReportFilters
                {
                    uuid = reportid,
                    filters = item.filters,
                    datatype = item.datatype,
                    conditions = item.conditions,
                    CreatedBy = user.UserName
                };
                await this._dbContext.AddAsync(filters);
            }
            
            await _dbContext.SaveChangesAsync();
            
            var getreport = await getReport(reportid);
            return getreport;
        }
        public async Task<ReportSource[]> sourceReport(AppUser user, Models.Form.ReportSource report)
        {
            var getreport = await this.getSource(report.source);
            if (getreport.Any())
            {
                throw new Exception("Source Aleady exists");
            }
            var loadSript = AppReportGenerator.loadSource(report.sourceScript);
            if (loadSript)
            {}
            ReportSource create = new ReportSource
            {
                 source=report.source
            };
            await this._dbContext.AddAsync(create);

            await _dbContext.SaveChangesAsync();
            getreport = await this.getSource(report.source);

            return getreport;
        }
        public async Task<List<Dictionary<string, object>>> excuteReport(AppUser user, Models.Form.ReportExcute report)
        {
           // check if the user has access to the report
           var hasasccess = await this.getReportAccess(user, report.uuid);
           if (hasasccess.Any())
            {
                throw new Exception("Report not found!");
            }
            var exeReport = await this._dbContext.Reports.Where(w=>w.uuid==report.uuid).ToArrayAsync();
            var filters = string.Join(" ",report.filters.Select(s => $"{s.filters} {s.conditions} '{s.values}' {s.conditionBinder}"));

            var reportObj = new List<Dictionary<string, object>>();

            using (var command = this._dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"{exeReport?.FirstOrDefault()?.command} WHERE {filters}";
                this._dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var results = new List<Dictionary<string, object>>();

                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                        }

                        results.Add(row);
                    }

                    reportObj = results;
                }
            }         
            return reportObj;
        }
        public async Task<ReportStoreDTO[]> enableAccessReport(string uuid)
        {
            throw new NotImplementedException();
        }
        public async Task<ReportStoreDTO[]> enableAccessReport(AppUser user, Models.Form.ReportAccess report)
        {
            throw new NotImplementedException();
        }
        public async Task<ReportDTO[]> getReport()
        {
            var getAllreport = await this._appQuery.Report().ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportDTO[]> getReport(string uuid)
        {
            var getAllreport = await this._appQuery.Report().Where(w => w.uuid == uuid).ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportAccessDTO[]> getReportAccess()
        {
            var getAllreport = await this._appQuery.ReportAccess().ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportAccessDTO[]> getReportAccess(AppUser user,string uuid)
        {
            var getAllreport = await this._appQuery.ReportAccess().Where(w => w.uuid == uuid && w.AllowedFor==user.UserName).ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportStoreDTO[]> getReportStore()
        {
            var getAllreport = await this._appQuery.ReportStore().ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportStoreDTO[]> getReportStore(AppUser user)
        {
            var getAllreport = await this._appQuery.ReportStore().Where(w=>w.users==user.UserName).ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportStoreDTO[]> getReportStore(string uuid)
        {
            var getAllreport = await this._appQuery.ReportStore().Where(w => w.uuid == uuid).ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportStoreDTO[]> getReportStore(AppUser user,string uuid)
        {
            var getAllreport = await this._appQuery.ReportStore().Where(w => w.uuid == uuid && w.users==user.UserName).ToArrayAsync();
            return getAllreport;
        }
        public async Task<ReportSource[]> getSource()
        {
            var getreport = await this._dbContext.ReportSource.ToArrayAsync();
            return getreport;
        }
        public async Task<ReportSource[]> getSource(string name)
        {
            var getreport = await this._dbContext.ReportSource.Where(w=>w.source==name).ToArrayAsync();
            return getreport;
        }
        public async Task<bool> removeReport(string uuid)
        {
            var deleteReport = await this._dbContext.Reports.Where(w => w.uuid == uuid).ExecuteDeleteAsync();
            return true;

        }
        public async Task<ReportStoreDTO[]> saveReport(AppUser user, string uuid)
        {
            var reportExist = await this.getReport(uuid);
            if (!reportExist.Any())
            {
                throw new Exception("Report does't exist.");
            }
            var storeExist = await this.getReportStore(uuid);
            if (storeExist.Any())
            {
                throw new Exception("Report aleardy saved.");
            }
            ReportStore store = new ReportStore
            {
                Report = uuid,
                users = user.UserName,
                CreatedBy=user.UserName
            };
            await this._dbContext.AddAsync(store);
            await this._dbContext.SaveChangesAsync();

            var getreport = await getReportStore(user,uuid);
            return getreport;
        }
        public async Task<ReportDTO[]> updateReport(AppUser user, string uuid, Models.Form.Report report)
        {
            AppReportGenerator generate = new AppReportGenerator(report, report.command);
            var updatereport = await this._dbContext.Reports.Where(w => w.uuid == uuid)
                .ExecuteUpdateAsync(u => u
                .SetProperty(p => p.title, report.title)
                .SetProperty(p => p.summary, report.summary)
                .SetProperty(p => p.description, report.description)
                .SetProperty(p => p.publisher, report.publisher)
                .SetProperty(p => p.source, report.command.Source)
                .SetProperty(p => p.category, report.category)
                .SetProperty(p => p.command, generate.getSqlCommand())
                );
            IQueryable<ReportFilters> updateFilter = this._dbContext.ReportFilters.Where(w => w.uuid == uuid);
            
            foreach(var filter in report.command.filters)
            {
                updateFilter?.ExecuteUpdateAsync(u => u
                 .SetProperty(p => p.filters, filter.filters)
                 .SetProperty(p => p.datatype, filter.datatype)
                 .SetProperty(p=>p.conditions,filter.conditions)
                 .SetProperty(p=>p.UpdatedBy,user.UserName)
                 .SetProperty(p=>p.UpdatedOn,DateTime.Now)
                );
            }

            var getreport = await getReport(uuid);
            return getreport;
        }
    }
}
