using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class modifydocreq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ea9dae47-5aa9-4ce2-a1b3-673a6f3d6ab8" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea9dae47-5aa9-4ce2-a1b3-673a6f3d6ab8");

            migrationBuilder.AlterColumn<string>(
                name: "count",
                table: "DoctorRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "9fbb0f8f-4b61-4076-ac98-dbcff9199605", 0, "4ee0f2b1-c7fc-4c0f-b28d-85b5315ba760", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEP8769Ig4Fc8F0dx9GmQl8yrKdBy+gdlcrWn62iyLrrce35MtYbr22bvAgeL3WLjdA==", "+251912657147", true, "4a218fb5-3348-440f-8071-fb46624e211c", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7949));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7952));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7953));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7955));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7956));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7957));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7925));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7926));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7927));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7873));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 49, 31, 176, DateTimeKind.Local).AddTicks(7877));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "9fbb0f8f-4b61-4076-ac98-dbcff9199605" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "9fbb0f8f-4b61-4076-ac98-dbcff9199605" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9fbb0f8f-4b61-4076-ac98-dbcff9199605");

            migrationBuilder.AlterColumn<int>(
                name: "count",
                table: "DoctorRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7087));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7088));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 21, 9, 37, 26, 868, DateTimeKind.Local).AddTicks(7090));

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
    }
}
