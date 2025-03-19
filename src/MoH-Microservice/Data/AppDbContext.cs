using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoH_Microservice.Models;

namespace MoH_Microservice.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentChannel> PaymentChannels { get; set; }
        public DbSet<PaymentPurpose> PaymentPurposes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id="1",Name="Admin",NormalizedName="ADMIN"},
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }

                );

            //Seed Admin Data
            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                UserName = "DerejeH",
                NormalizedUserName = "DEREJEH",
                Email = "dereje.hmariam@tsedeybank.com.et",
                NormalizedEmail = "DEREJE.HMARIAM@TSEDEYBANK.COM.ET",
                PhoneNumber = "+251912657147",
                EmailConfirmed = true,
                UserType="Admin",
                Departement="Tsedey Bank",
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Dereje@Tsedeybank");

            builder.Entity<AppUser>().HasData(adminUser);

            //Assign Role To Admin
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = adminUser.Id
                }
                );

            builder.Entity<Payment>().HasIndex("RefNo", "Createdby").IsUnique(false);
            builder.Entity<Payment>().HasIndex("RefNo").IsUnique(false);
            builder.Entity<Payment>().HasIndex("Createdby").IsUnique(false);

            builder.Entity<PaymentType>().HasData(
                new PaymentType { Id = 1, type = "CASH", CreatedBy = "Admin", CreatedOn = DateTime.Now}
                , new PaymentType { Id = 2, type = "Community-Based Health Insurance (CBHI)", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                , new PaymentType { Id = 3, type = "Credit", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                , new PaymentType { Id = 4, type = "Free of Charge", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                , new PaymentType { Id = 5, type = "Digital", CreatedBy = "Admin", CreatedOn = DateTime.Now }

                );

            builder.Entity<PaymentPurpose>().HasData(
                new PaymentPurpose { Id = 1, Purpose = "Card", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                , new PaymentPurpose { Id = 2, Purpose = "Medicine / Drug", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                , new PaymentPurpose { Id = 3, Purpose = "Labratory", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                , new PaymentPurpose { Id = 4, Purpose = "X-RAY", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                );

            builder.Entity<PaymentChannel>().HasData(
                     new PaymentChannel { Id = 1, Channel = "In Person", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                    , new PaymentChannel { Id = 2, Channel = "TeleBirr", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                    , new PaymentChannel { Id = 3, Channel = "Mobile Banking", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                    , new PaymentChannel { Id = 4, Channel = "Other", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                );
        }
    }
}
