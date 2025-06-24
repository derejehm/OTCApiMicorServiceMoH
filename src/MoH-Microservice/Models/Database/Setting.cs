namespace MoH_Microservice.Models.Database
{
    public class Setting : AuditFields
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? type { get; set; } // group, user level
    }
    public class GroupSetting : Setting
    {

        public string? group { get; set; }
        public string? action { get; set; }
        public bool isAdmin { get; set; }
        public string? description { get; set; }
        public bool IsGrunted { get; set; }
    }
    public class UserSetting : Setting
    {
        public string userId { get; set; }
        public bool isAdmin { get; set; }
        public string? permission { get; set; }
        public string? action { get; set; }
        public string? description { get; set; }
        public bool IsGrunted { get; set; }
    }
}
