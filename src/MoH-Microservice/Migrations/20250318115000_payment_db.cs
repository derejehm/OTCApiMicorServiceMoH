using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class payment_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "d4def35a-cc57-4984-b90e-d15670d0a012" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4def35a-cc57-4984-b90e-d15670d0a012");

            migrationBuilder.CreateTable(
                name: "PaymentChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Channel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPurposes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purpose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPurposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentVerifingID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PatientLoaction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PatientWorkingPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Createdby = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "4f5db469-92d9-4136-aae7-5eb6bd6dfc98", 0, "0993b56e-9beb-4ab7-a5c0-63590b442e16", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAELk0buObKeeKpWakypFGj4+DTYmwW2ubtsEIpnv5anV1LXaVhZPY69eIz9hAWsyFTg==", "+251912657147", true, "391c2d32-9faf-4386-98a0-3ed89688e8c1", false, "DerejeH", "Admin" });

            migrationBuilder.InsertData(
                table: "PaymentChannels",
                columns: new[] { "Id", "Channel", "CreatedBy", "CreatedOn" },
                values: new object[,]
                {
                    { 1, "In Person", "Admin", "3/18/2025 8:49:59 AM" },
                    { 2, "TeleBirr", "Admin", "3/18/2025 8:49:59 AM" },
                    { 3, "Mobile Banking", "Admin", "3/18/2025 8:49:59 AM" },
                    { 4, "Other", "Admin", "3/18/2025 8:49:59 AM" }
                });

            migrationBuilder.InsertData(
                table: "PaymentPurposes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Purpose" },
                values: new object[,]
                {
                    { 1, "Admin", "3/18/2025 8:49:59 AM", "Card" },
                    { 2, "Admin", "3/18/2025 8:49:59 AM", "Medicine / Drug" },
                    { 3, "Admin", "3/18/2025 8:49:59 AM", "Labratory" },
                    { 4, "Admin", "3/18/2025 8:49:59 AM", "X-RAY" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "type" },
                values: new object[,]
                {
                    { 1, "Admin", "3/18/2025 8:49:59 AM", "CASH" },
                    { 2, "Admin", "3/18/2025 8:49:59 AM", "Community-Based Health Insurance (CBHI)" },
                    { 3, "Admin", "3/18/2025 8:49:59 AM", "Credit" },
                    { 4, "Admin", "3/18/2025 8:49:59 AM", "Free of Charge" },
                    { 5, "Admin", "3/18/2025 8:49:59 AM", "Digital" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4f5db469-92d9-4136-aae7-5eb6bd6dfc98" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Createdby",
                table: "Payments",
                column: "Createdby");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RefNo",
                table: "Payments",
                column: "RefNo");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RefNo_Createdby",
                table: "Payments",
                columns: new[] { "RefNo", "Createdby" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentChannels");

            migrationBuilder.DropTable(
                name: "PaymentPurposes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "4f5db469-92d9-4136-aae7-5eb6bd6dfc98" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f5db469-92d9-4136-aae7-5eb6bd6dfc98");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "d4def35a-cc57-4984-b90e-d15670d0a012", 0, "92f20be3-8da8-4582-9d70-ea7a578666bc", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEJ7sfVSctu5isbacYN4LMeJTCrKcQcU6y0IB9WjMg3S6QZ3FU+GjNUvwfjpoNLhWRQ==", "+251912657147", true, "17260c7a-e82d-440b-b419-37e28ee47a9e", false, "DerejeH", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "d4def35a-cc57-4984-b90e-d15670d0a012" });
        }
    }
}
