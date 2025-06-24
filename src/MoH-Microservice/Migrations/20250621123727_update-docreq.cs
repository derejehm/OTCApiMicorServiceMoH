using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class updatedocreq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "c1a4620b-bcc5-442c-8b5c-f3bdfd87423c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c1a4620b-bcc5-442c-8b5c-f3bdfd87423c");

            migrationBuilder.AddColumn<string>(
                name: "group",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subgroup",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "duration",
                table: "DoctorRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instruction",
                table: "DoctorRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "ea9dae47-5aa9-4ce2-a1b3-673a6f3d6ab8", 0, "32292110-fbb1-488b-94b6-6b7e8420e4e7", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEEf9RdUs7hF8USDX9+5CJe+E1X8A1qF93cAeQMe6s1poF1/SWYldKV4isQPaHTWA7g==", "+251912657147", true, "8bdde0a5-b755-4d3f-86e3-55b534a00c4e", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7122));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7124));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7125));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7128));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7132));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "group", "subgroup" },
                values: new object[] { new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7083), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "group", "subgroup" },
                values: new object[] { new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7085), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "group", "subgroup" },
                values: new object[] { new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7087), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "group", "subgroup" },
                values: new object[] { new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7088), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "group", "subgroup" },
                values: new object[] { new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7090), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7017));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7020));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7021));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7023));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7025));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7026));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ea9dae47-5aa9-4ce2-a1b3-673a6f3d6ab8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ea9dae47-5aa9-4ce2-a1b3-673a6f3d6ab8" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea9dae47-5aa9-4ce2-a1b3-673a6f3d6ab8");

            migrationBuilder.DropColumn(
                name: "group",
                table: "PaymentPurposes");

            migrationBuilder.DropColumn(
                name: "subgroup",
                table: "PaymentPurposes");

            migrationBuilder.DropColumn(
                name: "duration",
                table: "DoctorRequests");

            migrationBuilder.DropColumn(
                name: "instruction",
                table: "DoctorRequests");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "c1a4620b-bcc5-442c-8b5c-f3bdfd87423c", 0, "b928db5e-472e-48d0-b9eb-b4b2ae3138a1", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEO7ELLYtkriD9N3+kKYzKkG4B89WEg6v1t/gACP3vPj4J8TMJAe/brcjcUt4VVSFTQ==", "+251912657147", true, "1179a13e-2737-4ec2-b455-89eabbe76b8a", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2979));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2980));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2981));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2982));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2983));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2984));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2985));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2956));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2959));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2915));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2916));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2918));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2919));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 6, 35, 4, 256, DateTimeKind.Local).AddTicks(2921));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "c1a4620b-bcc5-442c-8b5c-f3bdfd87423c" });
        }
    }
}
