using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FisrtName",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Managers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "f641c8c7-f9f6-443f-8d00-35f17be8cd79", false, false, null, null, null, null, false, "e072fbc5-580d-4505-9df2-c55071956bce", false, null });

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "616a6b1f-04e1-46ee-abaf-b30e038e5e84", false, false, null, null, null, null, false, "25306729-3df5-488b-b0e8-460a84f84acc", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FisrtName");
        }
    }
}
