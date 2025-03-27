using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Repository.Migrations
{
    /// <inheritdoc />
    public partial class newseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HotelId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonalId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "c6cee3db-546e-479b-9c7e-cdb21888a2e1", "giorgi@example.com", false, "Giorgi", 1, "Beridze", false, null, null, null, null, "12345678901", "+995555123456", false, "2adab4b0-0d68-4136-9b7f-193cb9fd5104", false, null },
                    { 2, 0, "db798f8f-113d-461e-b80e-8c1afb400773", "nino@example.com", false, "Nino", 2, "Mchedlidze", false, null, null, null, null, "98765432109", "+995599654321", false, "2449f0dd-3f0a-4bf9-9d39-8a39baa1d03b", false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
