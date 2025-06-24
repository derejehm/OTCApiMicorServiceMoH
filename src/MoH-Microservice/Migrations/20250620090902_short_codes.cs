using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class short_codes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ed02b962-82a5-4b21-a8b3-cbf8d655e6a6" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ed02b962-82a5-4b21-a8b3-cbf8d655e6a6");

            migrationBuilder.AddColumn<string>(
                name: "shortCodes",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReportSource",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSource", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "0bf67d32-d27e-462a-97d1-94c8702e217c", 0, "b7892774-9889-4a87-aab7-6698fa31e3d2", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEHMYb3VnodktCHLM+OVq0LFKPlhiYYUnt9uoooQb3FS4JyKdQodnZiXrIcigrkKUTQ==", "+251912657147", true, "bd9e4568-9aae-440d-a222-1bfce9d3bd54", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6443));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6445));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6446));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6448));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6450));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6451));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6454));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "shortCodes" },
                values: new object[] { new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6412), null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "shortCodes" },
                values: new object[] { new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6414), null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "shortCodes" },
                values: new object[] { new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6416), null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "shortCodes" },
                values: new object[] { new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6418), null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "shortCodes" },
                values: new object[] { new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6419), null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6359));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6362));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6363));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6365));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6367));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 6, 9, 1, 214, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "0bf67d32-d27e-462a-97d1-94c8702e217c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportSource");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "0bf67d32-d27e-462a-97d1-94c8702e217c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0bf67d32-d27e-462a-97d1-94c8702e217c");

            migrationBuilder.DropColumn(
                name: "shortCodes",
                table: "PaymentPurposes");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "ed02b962-82a5-4b21-a8b3-cbf8d655e6a6", 0, "51c6beaa-7d32-4476-a068-d7dbe68c32c8", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEIGY9reV79ff2rXjmnWwmN8bDMkV7bcnozsLDFf03q019Z03ZR0PyVr1JcTbcHbRlA==", "+251912657147", true, "2c67d042-d2a2-42f8-b923-908a39602958", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6848));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6850));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6852));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6854));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6855));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6856));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6857));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6827));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6782));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6784));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6785));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6786));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6788));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 18, 11, 59, 31, 1, DateTimeKind.Local).AddTicks(6789));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ed02b962-82a5-4b21-a8b3-cbf8d655e6a6" });
        }
    }
}
