using System.Xml;

namespace MoH_Microservice.Models.Database
{
    public class Report:AuditFields
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public string? summary { get; set; }
        public string? category { get; set; }
        public string? publisher { get; set; }
        public string command { get; set; } = string.Empty;
        public string source { get; set; } = string.Empty;
        public string Columns { get; set; } = string.Empty;
        public string? filters { get; set; } = null;
        public bool enabled { get; set; }=false;
        public bool grouped { get; set; }=true;
        public bool enableCount { get; set; } = false;

    }
    public class ReportFilters : AuditFields
    {
        public int id { get; set; }
        public string uuid { get; set; } // report id
        public string filters { get; set; } = string.Empty;
        public string datatype { get; set; } = string.Empty;
        public string conditions { get; set; } = string.Empty;

    }

    public class ReportStore : AuditFields
    {
        public int id { get; set; }
        public string Report { get; set; } // report id
        public string users { get; set; } = string.Empty;
        public string datatype { get; set; } = string.Empty;
        public string conditions { get; set; } = string.Empty;

    }
    public class ReportAccess : AuditFields
    {
        public int id { get; set; }
        public string Report { get; set; } // report id
        public string users { get; set; } = string.Empty;
        public bool enabled { get; set; } = false;
    }
    public class ReportSource : AuditFields
    {
        public int id { get; set; }
        public string source { get; set; } // report id
    }
}
