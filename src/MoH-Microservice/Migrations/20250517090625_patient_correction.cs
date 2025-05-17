using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class patient_correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "57f282eb-68d7-43ff-aab3-ee9f716f4c95" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57f282eb-68d7-43ff-aab3-ee9f716f4c95");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "PatientAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdOn",
                table: "PatientAddress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "79045938-fd3b-4d70-a8c2-db9415a40cef", 0, "ef6668e2-275b-43a3-a774-fc8bcdb79204", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEOkxfeNLWCf4bUBSQ+oljzQWqnwWQ0f21Kauc1/pfEi1/XH3EtxzKWWSPOmwj0UsdA==", "+251912657147", true, "eccc05fe-8130-446c-9901-7c913dfdcd0c", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1763));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1765));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1769));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1781));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1795));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1798));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1802));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1664));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1666));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1668));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1671));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1673));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1528));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1533));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1535));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 6, 24, 350, DateTimeKind.Local).AddTicks(1540));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "79045938-fd3b-4d70-a8c2-db9415a40cef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "79045938-fd3b-4d70-a8c2-db9415a40cef" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79045938-fd3b-4d70-a8c2-db9415a40cef");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "PatientAddress");

            migrationBuilder.DropColumn(
                name: "createdOn",
                table: "PatientAddress");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "57f282eb-68d7-43ff-aab3-ee9f716f4c95", 0, "654571fe-5efa-4ab7-918e-d6c825f11859", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEKs4W75avhCxBSXWppSzBkcUtNsTSiqkw3aPrtBhcidUzzc55iKpmzQWmb9daEtSgg==", "+251912657147", true, "849c49b2-3f57-4e9a-92f0-2d221e3c91a6", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8102));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8104));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8108));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(7991));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 17, 6, 3, 51, 948, DateTimeKind.Local).AddTicks(7993));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "57f282eb-68d7-43ff-aab3-ee9f716f4c95" });
        }
    }
}
