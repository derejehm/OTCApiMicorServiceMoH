using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoH_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class paymenttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "3ab168f2-0120-4f48-a963-6481680d0409" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ab168f2-0120-4f48-a963-6481680d0409");

            migrationBuilder.RenameColumn(
                name: "Createdby",
                table: "ProvidersMapPatient",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Updatedby",
                table: "Providers",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Createdby",
                table: "Providers",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdateOn",
                table: "Providers",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "Createdby",
                table: "Payments",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_RefNo_Createdby",
                table: "Payments",
                newName: "IX_Payments_RefNo_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Createdby",
                table: "Payments",
                newName: "IX_Payments_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedOn",
                table: "Patients",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "Patients",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "createdOn",
                table: "Patients",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Patients",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedOn",
                table: "PatientRequestedServices",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "PatientRequestedServices",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "createdOn",
                table: "PatientRequestedServices",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "PatientRequestedServices",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedOn",
                table: "PatientAddress",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "PatientAddress",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "createdOn",
                table: "PatientAddress",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "PatientAddress",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedOn",
                table: "PatientAccedents",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "PatientAccedents",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "createdOn",
                table: "PatientAccedents",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "PatientAccedents",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedOn",
                table: "NurseRequests",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "NurseRequests",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "createdOn",
                table: "NurseRequests",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "NurseRequests",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "RegisteredOn",
                table: "Hospitals",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "RegisteredBy",
                table: "Hospitals",
                newName: "UpdatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "permission",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "userSettings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "provider",
                table: "ProvidersMapPatient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Examination",
                table: "ProvidersMapPatient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProvidersMapPatient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ProvidersMapPatient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ProvidersMapPatient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProvidersMapPatient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "ProvidersMapPatient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "service",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "provider",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Providers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "PaymentTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentTypeDiscriptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentTypeDiscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentTypeDiscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefNo",
                table: "Payments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentPurposes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentPurposeLimits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentPurposeLimits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentPurposeLimits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PaymentCollectors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentCollectors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentCollectors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentCollectors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PaymentCollectors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "PaymentCollectors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PaymentCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "PaymentCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentChannels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Channel",
                table: "PaymentChannels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PaymentChannels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentChannels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "iscreadituser",
                table: "Patients",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "iscbhiuser",
                table: "Patients",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PatientRequestedServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PatientRequestedServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PatientAddress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PatientAddress",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PatientAccedents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PatientAccedents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Organiztions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Organiztions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Organiztions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "OrganiztionalUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "OrganiztionalUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrganiztionalUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OrganiztionalUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "OrganiztionalUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrganiztionalUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "NurseRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "NurseRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Hospitals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Hospitals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "group",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "groupSettings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReportFilters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datatype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFilters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    command = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Columns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    grouped = table.Column<bool>(type: "bit", nullable: false),
                    enableCount = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "465a99e7-b55d-437e-a9c3-3815b8dc7638", 0, "2d588f3f-0cf5-430e-a5a2-c305a294d0fe", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEI3DzUBsoFvsshIU1yOgeU1B0rL99owMT9WrUoIGeDvTlWMOJtWWOZ+g4/he8vLj/g==", "+251912657147", true, "e3ffc1b6-ead4-4635-ae84-33d3d5126403", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2462), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2464), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2465), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2467), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2468), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2469), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2470), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2472), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2436), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2437), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2439), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2440), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2441), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2363), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2365), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2366), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2368), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2396), null, null });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedBy", "DeletedOn" },
                values: new object[] { new DateTime(2025, 6, 18, 5, 23, 1, 892, DateTimeKind.Local).AddTicks(2397), null, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "465a99e7-b55d-437e-a9c3-3815b8dc7638" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportFilters");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "465a99e7-b55d-437e-a9c3-3815b8dc7638" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "465a99e7-b55d-437e-a9c3-3815b8dc7638");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "userSettings");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "userSettings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProvidersMapPatient");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ProvidersMapPatient");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProvidersMapPatient");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "ProvidersMapPatient");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentTypeDiscriptions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentTypeDiscriptions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentPurposes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentPurposes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentPurposeLimits");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentPurposeLimits");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PaymentCollectors");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PaymentCollectors");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentCollectors");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentCollectors");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PaymentCollectors");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "PaymentCollectors");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentCollections");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentCollections");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PaymentCollections");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "PaymentCollections");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentChannels");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentChannels");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PatientRequestedServices");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PatientRequestedServices");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PatientAddress");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PatientAddress");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PatientAccedents");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PatientAccedents");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Organiztions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Organiztions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrganiztionalUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrganiztionalUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "OrganiztionalUsers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrganiztionalUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "NurseRequests");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "NurseRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "groupSettings");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "groupSettings");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProvidersMapPatient",
                newName: "Createdby");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Providers",
                newName: "Updatedby");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Providers",
                newName: "Createdby");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Providers",
                newName: "UpdateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Payments",
                newName: "Createdby");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_RefNo_CreatedBy",
                table: "Payments",
                newName: "IX_Payments_RefNo_Createdby");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CreatedBy",
                table: "Payments",
                newName: "IX_Payments_Createdby");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Patients",
                newName: "updatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Patients",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Patients",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Patients",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "PatientRequestedServices",
                newName: "updatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "PatientRequestedServices",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "PatientRequestedServices",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PatientRequestedServices",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "PatientAddress",
                newName: "updatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "PatientAddress",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "PatientAddress",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PatientAddress",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "PatientAccedents",
                newName: "updatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "PatientAccedents",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "PatientAccedents",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PatientAccedents",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "NurseRequests",
                newName: "updatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "NurseRequests",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "NurseRequests",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "NurseRequests",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Hospitals",
                newName: "RegisteredOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Hospitals",
                newName: "RegisteredBy");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "permission",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "userSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "provider",
                table: "ProvidersMapPatient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Examination",
                table: "ProvidersMapPatient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Createdby",
                table: "ProvidersMapPatient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "service",
                table: "Providers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "provider",
                table: "Providers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Createdby",
                table: "Providers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "PaymentTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentTypeDiscriptions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RefNo",
                table: "Payments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Payments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Createdby",
                table: "Payments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "PaymentPurposes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentPurposes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentPurposeLimits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentChannels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Channel",
                table: "PaymentChannels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iscreadituser",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iscbhiuser",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Organiztions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "OrganiztionalUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "OrganiztionalUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "group",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "groupSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departement", "Email", "EmailConfirmed", "Hospital", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "3ab168f2-0120-4f48-a963-6481680d0409", 0, "0bc4bd54-c6e1-4a59-ae2c-29fefd4f87a5", "Tsedey Bank", "dereje.hmariam@tsedeybank.com.et", true, "", false, null, "DEREJE.HMARIAM@TSEDEYBANK.COM.ET", "DEREJEH", "AQAAAAIAAYagAAAAEKqz1svr7jREk7bbH5rWWYwbaCWE6DLKwY88z0ZWwHOhzCZCnQwQrRHZkEv3CPCNAg==", "+251912657147", true, "5145cf3d-5434-41a0-8470-a8e94103fdde", false, "DerejeH", "Admin" });

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9634));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9636));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9637));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9639));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9640));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9641));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9643));

            migrationBuilder.UpdateData(
                table: "PaymentChannels",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9644));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9607));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9608));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9611));

            migrationBuilder.UpdateData(
                table: "PaymentPurposes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9612));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9563));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9565));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9567));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9568));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9569));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 16, 9, 49, 47, 522, DateTimeKind.Local).AddTicks(9571));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "3ab168f2-0120-4f48-a963-6481680d0409" });
        }
    }
}
