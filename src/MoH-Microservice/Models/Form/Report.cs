namespace MoH_Microservice.Models.Form
{
    public class Report
    {
        public string title { get; set; }
        public string? description { get; set; }
        public string? summary { get; set; }
        public string? category { get; set; }
        public string? publisher { get; set; }
        public ReportCommands command { get; set; }
        public bool enabled { get; set; } = false;
    }
    public class ReportCommands
    {
        public string Source { get; set; }
        public string[] Columns { get; set; }
        public ReportFilters[] filters { get; set; }
        public bool enablegroup { get; set; } = true;
        public bool enableCount { get; set; } = false;
    }
    public class ReportFilters
    {
        public string uuid { get; set; } // report id
        public string filters { get; set; } = string.Empty;
        public string datatype { get; set; } = string.Empty;
        public string conditions { get; set; } = string.Empty;
    }
    public class ReportFiltersForExe
    {
        public string filters { get; set; } = string.Empty;
        public string values {  get; set; } = string.Empty;
        public string conditions { get; set; } = string.Empty;
        public string conditionBinder { get; set; } = string.Empty;
    }

    public class ReportStore
    {
        public string uuid { get; set; } // report id
        public string users { get; set; } = string.Empty;
        public string datatype { get; set; } = string.Empty;
        public string conditions { get; set; } = string.Empty;

    }
    public class ReportAccess
    {
        public string uuid { get; set; } // report id
        public string users { get; set; } = string.Empty;
        public bool enabled { get; set; } = false;
    }
    public class ReportSource
    {
        public string source { get; set; } = string.Empty; // report id
        public string sourceScript {  get; set; }=string.Empty;
    }
    public class ReportExcute
    {
        public string uuid { get; set; } // report id
        public ReportFiltersForExe[] filters { get; set; }
    }

}
