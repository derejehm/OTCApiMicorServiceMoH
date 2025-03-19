using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class payment_db2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "4f5db469-92d9-4136-aae7-5eb6bd6dfc98" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f5db469-92d9-4136-aae7-5eb6bd6dfc98");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Payments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "b8a9ea6d-ce3a-4a3f-9ce7-c8ca0763aa3f", 0, "47ff66a7-1362-463d-8d39-0a85725b300a", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEJkgxAYXYcnjotpME8izVFRj/zBP5SK/4MdVultbuTBDZUhWk8byDdS/ZAlsF/4n6g==", "+251912657147", true, "4498dacc-d2a5-46b3-9367-1171d4a7cbcc", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: "3/18/2025 9:45:16 AM");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "b8a9ea6d-ce3a-4a3f-9ce7-c8ca0763aa3f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "b8a9ea6d-ce3a-4a3f-9ce7-c8ca0763aa3f" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8a9ea6d-ce3a-4a3f-9ce7-c8ca0763aa3f");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "4f5db469-92d9-4136-aae7-5eb6bd6dfc98", 0, "0993b56e-9beb-4ab7-a5c0-63590b442e16", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAELk0buObKeeKpWakypFGj4+DTYmwW2ubtsEIpnv5anV1LXaVhZPY69eIz9hAWsyFTg==", "+251912657147", true, "391c2d32-9faf-4386-98a0-3ed89688e8c1", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: "3/18/2025 8:49:59 AM");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4f5db469-92d9-4136-aae7-5eb6bd6dfc98" });
        }
    }
}
