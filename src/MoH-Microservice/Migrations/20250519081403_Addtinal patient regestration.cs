using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class Addtinalpatientregestration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "5df11583-5f21-46a7-ae41-ec4764293aaa" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5df11583-5f21-46a7-ae41-ec4764293aaa");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "b34cd829-f7bd-4f54-81b8-72faf60aad5d", 0, "f1267473-25af-4e27-bf4f-5ace425f25d7", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEO9fI5t7HZL4X5mIvSQC6SLRChz+8P4Aey+uloCVwC/HWvhYRDaibEJfR5S6JV2wsQ==", "+251912657147", true, "886af949-33f3-4d06-b71c-5a0f061f9057", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8659));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8663));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8665));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8667));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8669));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8671));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8613));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8615));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8619));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8550));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 19, 11, 14, 1, 967, DateTimeKind.Local).AddTicks(8552));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "b34cd829-f7bd-4f54-81b8-72faf60aad5d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "b34cd829-f7bd-4f54-81b8-72faf60aad5d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b34cd829-f7bd-4f54-81b8-72faf60aad5d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "5df11583-5f21-46a7-ae41-ec4764293aaa", 0, "8b7bf145-e2ea-4165-a8a7-72fd0f8cabf9", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEMQ3r8AF+b2UdYNkhMQUlL34yHe3muHqIhWfCfk8IEhlUv+gkdBduPkvwuKXk9YO6g==", "+251912657147", true, "6b2f5734-3dbb-4bda-a66d-134371921aff", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2942));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2948));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2963));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2877));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2883));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2886));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2764));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2772));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2775));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2777));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 14, 16, 7, 11, 365, DateTimeKind.Local).AddTicks(2784));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "5df11583-5f21-46a7-ae41-ec4764293aaa" });
        }
    }
}
