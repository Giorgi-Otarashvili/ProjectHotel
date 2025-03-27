using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Repository.Migrations
{
    /// <inheritdoc />
    public partial class login : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "FirstName", "LastName", "PersonalId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Luka", "Giorgadze", "11122233344", "+995577123456" },
                    { 2, "Ana", "Kiknavelidze", "55566677788", "+995599876543" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "City", "Country", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Rustaveli Ave 12", "Tbilisi", "Georgia", "Grand Palace", 5 },
                    { 2, "Black Sea St 7", "Batumi", "Georgia", "Sea View Resort", 4 }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HotelId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonalId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "f641c8c7-f9f6-443f-8d00-35f17be8cd79", "giorgi@example.com", false, "Giorgi", 1, "Beridze", false, null, null, null, null, "12345678901", "+995555123456", false, "e072fbc5-580d-4505-9df2-c55071956bce", false, null },
                    { 2, 0, "616a6b1f-04e1-46ee-abaf-b30e038e5e84", "nino@example.com", false, "Nino", 2, "Mchedlidze", false, null, null, null, null, "98765432109", "+995599654321", false, "25306729-3df5-488b-b0e8-460a84f84acc", false, null }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "HotelId", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, true, "Deluxe Room", 150m },
                    { 2, 1, true, "Standard Room", 80m },
                    { 3, 2, false, "Luxury Suite", 200m }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckIn", "CheckOut", "GuestId", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });
        }
    }
}
