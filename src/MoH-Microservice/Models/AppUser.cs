using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MoH_Microservice.Models
{
    public class AppUser :IdentityUser
    {

        public  string UserType { get; set; } = string.Empty;
        public string Departement { get; set; } = string.Empty;
        public string Hospital { get; set; } = string.Empty;
    }
}
