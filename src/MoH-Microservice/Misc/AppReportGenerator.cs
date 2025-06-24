using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoH_Microservice.Models.Form;

namespace MoH_Microservice.Misc
{
    /**
     * Custome report generator
     
     */
    public class AppReportGenerator
    {
        private readonly ILogger _logger;
        private string _title;
        private string _description;
        private string _summary;
        private string _category;
        private string _publisher; // Created by
        private ReportCommands _command; // formatted json

        /**
         * @title : report name
         * @Description : Report Description
         * @fields: report filters
         */
        public AppReportGenerator() {
            _logger.BeginScope(" [WARNING] ", " - ");
        }
        public AppReportGenerator(Report report, ReportCommands command)
        {
            this._title = report.title;
            _description = report.description;
            _summary = report.summary;
            _category = report.category;
            _publisher = report.publisher;
            _command = report.command;
        }
        public string getSqlCommand()
        {
            var sql = "SELECT";
            var columns = string.Join(",", this._command.Columns);
            
            sql = $"{sql} {columns}";
            if (this._command.enableCount)
                sql = $"{sql}, Count(1) {this._title}_count ";

            sql = $"{sql}  FROM {_command.Source}";
            sql = $"{sql}  GROUP BY {columns.Remove(columns.Length)}";

            // SELECT columns, count FROM source GROUP BY columns

            string finalSql = $"SELECT * FROM ( {sql} ) source_{_title}";
            return finalSql;
        }
        public void parser()
        {
            // takes the command and convert into sqlCommand
            throw new NotImplementedException();
        }

        public static bool loadSource(string script)
        {
            throw new NotImplementedException();
        }
    }
}
