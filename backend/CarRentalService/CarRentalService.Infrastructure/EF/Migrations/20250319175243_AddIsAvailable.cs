using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalService.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                table: "RefreshToken");

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6d23c249-71dd-4348-8d6d-1c0d6469fc87"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("72c1e4d9-0e48-4eec-997d-36bddfef044b"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("f041300a-2ef1-4def-848f-1a85359accc2"));

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "IsAvailable", "LicensePlate", "Model", "PricePerDay", "RentalPointId", "Seats", "Type", "Year" },
                values: new object[,]
                {
                    { new Guid("3e856063-99de-45ac-bbd9-a10fb8c6fe3a"), "Toyota", true, "KR1234AB", "Camry", 50m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2020 },
                    { new Guid("715db536-3816-46f8-9163-95cfb6c7b265"), "Honda", true, "WA5678CD", "Civic", 45m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2021 },
                    { new Guid("aea5884b-7d57-4f97-b500-381fa747e990"), "Ford", true, "PO4012FF", "F-150", 80m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 2, "Truck", 2018 },
                    { new Guid("e76e0895-81bb-4cc3-8ac4-fb4e84889c1c"), "Ford", true, "PO9012EF", "Focus", 40m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 4, "Car", 2019 },
                    { new Guid("edfab026-656b-46c5-92bd-ad82001d298c"), "Yamaha", true, "PO9014EL", "MT-07", 35m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 2, "Motorcycle", 2021 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                table: "RefreshToken");

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("3e856063-99de-45ac-bbd9-a10fb8c6fe3a"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("715db536-3816-46f8-9163-95cfb6c7b265"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("aea5884b-7d57-4f97-b500-381fa747e990"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("e76e0895-81bb-4cc3-8ac4-fb4e84889c1c"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("edfab026-656b-46c5-92bd-ad82001d298c"));

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "IsAvailable", "LicensePlate", "Model", "PricePerDay", "RentalPointId", "Seats", "Type", "Year" },
                values: new object[,]
                {
                    { new Guid("6d23c249-71dd-4348-8d6d-1c0d6469fc87"), "Ford", true, "PO9012EF", "Focus", 40m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 4, "Car", 2019 },
                    { new Guid("72c1e4d9-0e48-4eec-997d-36bddfef044b"), "Toyota", true, "KR1234AB", "Camry", 50m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2020 },
                    { new Guid("f041300a-2ef1-4def-848f-1a85359accc2"), "Honda", true, "WA5678CD", "Civic", 45m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2021 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
