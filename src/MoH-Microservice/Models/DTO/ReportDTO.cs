namespace MoH_Microservice.Models.DTO
{
    public class ReportDTO
    {
        public string uuid { get; set; } = string.Empty;
        public string ReportTitle { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
        public string ReportSummary { get; set; } = string.Empty;
        public string ReportCategory { get; set; } = string.Empty;
        public string ReportPublisher { get; set; } = string.Empty;
        public string ReportSource { get; set; } = string.Empty;
        public string ReportColumns { get; set; } = string.Empty;
        public string ReportFilters { get; set; } = string.Empty;
        public string datatype { get; set; } = string.Empty;
        public string conditions { get; set; } = string.Empty;
        public bool Accessable { get; set; } = false;
        public bool grouped { get; set; } = true;
        public bool enableCount { get; set; } = false;
        public string Reportusers { get; set; } = string.Empty;
        public bool Allowed { get; set; } = false;
        public DateTime ReportCreatedOn { get; set; }   = DateTime.MinValue;
        public string ReportCreatedBy { get; set; } = string.Empty;
    }

    public class ReportStoreDTO
    {
        public string uuid { get; set; } = string.Empty;
        public string ReportTitle { get; set; }
        public string? ReportDescription { get; set; }
        public string? ReportSummary { get; set; }
        public string? ReportCategory { get; set; }
        public string? ReportPublisher { get; set; }
        public string users { get; set; } = string.Empty;
    }
    public class ReportAccessDTO
    {
        public string uuid { get; set; }
        public string ReportTitle { get; set; }
        public string? ReportDescription { get; set; }
        public string? ReportSummary { get; set; }
        public string? ReportCategory { get; set; }
        public string? ReportPublisher { get; set; }
        public string AllowedFor { get; set; } = string.Empty;
    }
}
