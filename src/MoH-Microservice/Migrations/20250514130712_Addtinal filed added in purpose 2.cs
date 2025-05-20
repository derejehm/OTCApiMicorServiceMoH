using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class Addtinalfiledaddedinpurpose2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "44474f82-b770-4cc8-b465-794289cf146d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44474f82-b770-4cc8-b465-794289cf146d");


            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "OrganiztionalUsers",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "5df11583-5f21-46a7-ae41-ec4764293aaa" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5df11583-5f21-46a7-ae41-ec4764293aaa");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "OrganiztionalUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "OrganiztionalUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "44474f82-b770-4cc8-b465-794289cf146d", 0, "fe321cb5-aa58-41ab-828f-613fc67205bb", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEOFxC/UCBEQjGZK+sADb3l2jx0WwVwOrvyoiJ0Y9O3Xft/gJSHA/Uvefke5HvEfkiw==", "+251912657147", true, "a37f887c-0923-419c-b53b-298d47a7cb28", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8863));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8867));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8801));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8805));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8807));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8722));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8726));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8729));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8731));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8733));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8734));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "44474f82-b770-4cc8-b465-794289cf146d" });
        }
    }
}
