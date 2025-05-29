using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class initalMigrationfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "220e5ca6-0f46-4d62-b297-b37b417108cf" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "220e5ca6-0f46-4d62-b297-b37b417108cf");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "20bd2304-63c3-4159-8224-d4310a937bd0", 0, "c3c43520-14bc-4720-842c-22d78b423e8b", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAELE9ygO7jobdYZNpmB+RLfWTTsOKvQ3ezFw86V/D4+FvKztmCEE0EuE/GDz6uVHTFg==", "+251912657147", true, "ac68d525-478a-4c0e-891c-cfcb2ffdc639", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1939));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1946));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1948));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1952));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1954));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1882));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1891));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1893));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1797));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1801));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1803));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1807));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 27, 15, 10, 39, 35, DateTimeKind.Local).AddTicks(1809));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "20bd2304-63c3-4159-8224-d4310a937bd0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "20bd2304-63c3-4159-8224-d4310a937bd0" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20bd2304-63c3-4159-8224-d4310a937bd0");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "PaymentPurposes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "220e5ca6-0f46-4d62-b297-b37b417108cf", 0, "dcfe7de8-fb5d-4d8f-9832-fd389222c583", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEMlTL7FctDSiX1BHEycomWaVwSkrmHsbWfh3co19QA9UgTTIOthuDbj06+Md53cpSA==", "+251912657147", true, "0f09f0ca-22f5-4a95-8345-8b2c85edfa72", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6676));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6678));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6679));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6682));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6642));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6644));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6645));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6647));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6565));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6569));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 24, 6, 21, 34, 328, DateTimeKind.Local).AddTicks(6571));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "220e5ca6-0f46-4d62-b297-b37b417108cf" });
        }
    }
}
