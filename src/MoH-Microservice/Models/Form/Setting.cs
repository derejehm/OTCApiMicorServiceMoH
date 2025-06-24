namespace MoH_Microservice.Models.Form
{
    public class GroupSettingUpdate
    {
        public int id { get; set; }
        public string group { get; set; }
        public string action { get; set; }
        public bool isAdmin { get; set; }
        public string description { get; set; }
        public bool IsGrunted { get; set; }
    }
    public class UserSettingUpdate
    {
        public int id { get; set; }
        public string userId { get; set; }
        public bool isAdmin { get; set; }
        public string permission { get; set; }
        public string action { get; set; }
        public string description { get; set; }
        public bool IsGrunted { get; set; }
    }


}
