namespace MoH_Microservice.Models.Form
{


    public class HospitalReg
    {
        public string HospitalName { get; set; }
        public string? HospitalManager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? ContactMethod { get; set; }
    }

    public class HospitalUpdate
    {
        public int id { get; set; }
        public string HospitalManager { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string ContactMethod { get; set; }
    }

    public class HospitalDelete
    {
        public int id { get; set; }
    }
}
