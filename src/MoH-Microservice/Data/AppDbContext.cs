using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoH_Microservice.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id="1",Name="Admin",NormalizedName="ADMIN"},
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }

                );

            //Seed Admin Data
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                UserName = "DerejeH",
                NormalizedUserName = "DEREJEH",
                Email = "dereje.hmariam@tsedeybank.com.et",
                NormalizedEmail = "DEREJE.HMARIAM@TSEDEYBANK.COM.ET",
                PhoneNumber = "+251912657147",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Dereje@Tsedeybank");

            builder.Entity<IdentityUser>().HasData(adminUser);

            //Assign Role To Admin
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = adminUser.Id
                }
                );
        }
    }
}
