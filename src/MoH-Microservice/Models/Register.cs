namespace MoH_Microservice.Models
{
    public class Register
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string Departement { get; set; } = string.Empty;

        public string Hospital { get; set; } = string.Empty;

    }
}
