using Microsoft.AspNetCore.Mvc;

namespace MoH_Microservice.Misc
{
    public class AppError
    {
        public string? msg { get; set; } = "Error occured.";
        public int? ErrorCode { get; set; } = 0;
        public string? ErrorDescription { get; set; }=string.Empty;
        public Exception? ErrorException { get; set; }=null;
    }
    public class AppSuccess
    {
        public string? msg { get; set; }="Action completed successfully.";
        public int? count { get; set; } = 0;
        public TimeSpan? elapsedTime { get; set; }=TimeSpan.Zero;
        public JsonResult? data { get; set; }=null;
    }
}
