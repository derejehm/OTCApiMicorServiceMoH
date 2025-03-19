using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class payment_db3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "b8a9ea6d-ce3a-4a3f-9ce7-c8ca0763aa3f" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8a9ea6d-ce3a-4a3f-9ce7-c8ca0763aa3f");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IsCollected",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentPurposes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentChannels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "e0fcc3da-737c-49ac-a8cd-ba18b049c696", 0, "6c6311d8-decd-45f6-9efd-37349e6dd304", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEA/YuJ/BODk4CbTZvCbPuj8GVkw4pr4vt6YBvqnJV1XkulhQMzEsgayr35ANO1xKUQ==", "+251912657147", true, "ec6429eb-b215-4ba2-9c9c-fd0cd967a831", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(504));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(506));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(507));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(509));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(480));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(482));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(484));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(485));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(442));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(443));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(445));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(446));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 18, 9, 54, 26, 599, DateTimeKind.Local).AddTicks(447));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e0fcc3da-737c-49ac-a8cd-ba18b049c696" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e0fcc3da-737c-49ac-a8cd-ba18b049c696" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0fcc3da-737c-49ac-a8cd-ba18b049c696");

            migrationBuilder.DropColumn(
                name: "IsCollected",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "PaymentTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "PaymentChannels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
    }
}
