using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class Addtinalfiledaddedinpurpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "7b36a8a8-f293-41ec-be54-ded00dcd4979" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b36a8a8-f293-41ec-be54-ded00dcd4979");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "PaymentPurposes",
                type: "decimal(18,2)",
                nullable: true);

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
                columns: new[] { "Channel", "CreatedOn" },
                values: new object[] { "CBE Mobile Banking", new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8858) });

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
                columns: new[] { "Amount", "CreatedOn" },
                values: new object[] { null, new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8801) });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amount", "CreatedOn" },
                values: new object[] { null, new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8805) });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Amount", "CreatedOn" },
                values: new object[] { null, new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8807) });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "CreatedOn" },
                values: new object[] { null, new DateTime(2025, 5, 10, 10, 16, 43, 203, DateTimeKind.Local).AddTicks(8810) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "44474f82-b770-4cc8-b465-794289cf146d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44474f82-b770-4cc8-b465-794289cf146d");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PaymentPurposes");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "7b36a8a8-f293-41ec-be54-ded00dcd4979", 0, "a0cb0b32-cf59-49f4-8679-f841d7d9788e", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEPCtBN7R63ZACUfIBbKXevBfO+v8NoX7RHVgisTBX8LluSe4cqwMfuNR2awo3fdT0Q==", "+251912657147", true, "becdc453-6beb-4cab-a420-77d635a95fc9", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Channel", "CreatedOn" },
                values: new object[] { "Mobile Banking", new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(958));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(906));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(909));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(912));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(914));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(781));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(788));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(790));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(792));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(795));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 4, 14, 9, 52, 6, 711, DateTimeKind.Local).AddTicks(798));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "7b36a8a8-f293-41ec-be54-ded00dcd4979" });
        }
    }
}
