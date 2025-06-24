using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class doctorrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ecfb050a-15a8-49dc-b1af-f0ded1057c61" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ecfb050a-15a8-49dc-b1af-f0ded1057c61");

            migrationBuilder.CreateTable(
                name: "DoctorRequests",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    groupId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MRN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requestTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requestFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    measurment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    catagory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorRequests", x => x.id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorRequests");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "c1a4620b-bcc5-442c-8b5c-f3bdfd87423c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c1a4620b-bcc5-442c-8b5c-f3bdfd87423c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "ecfb050a-15a8-49dc-b1af-f0ded1057c61", 0, "c6f12c13-7a50-44c5-b3b5-d347b50ef53b", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEI/WzCaK4WeKBBOXgcjgLK5593qyAqA1HTisyv9yyuKMzS1EzJu3/rGdCtkximo6tw==", "+251912657147", true, "735bda4f-8192-4dbe-8e18-dc16863f25bc", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9473));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9475));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9476));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9477));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9478));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9480));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9481));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9483));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9444));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9446));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9447));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9392));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9398));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 20, 10, 25, 47, 116, DateTimeKind.Local).AddTicks(9399));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ecfb050a-15a8-49dc-b1af-f0ded1057c61" });
        }
    }
}
