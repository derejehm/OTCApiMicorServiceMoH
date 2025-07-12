namespace MoH_Microservice.Models.Database
{
    public class Hospital : AuditFields
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public string? HospitalManager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? ContactMethod { get; set; }
    }
}
