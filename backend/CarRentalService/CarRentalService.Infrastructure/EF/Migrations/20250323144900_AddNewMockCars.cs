using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalService.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMockCars : Migration
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
                keyValue: new Guid("2c903c06-07cb-4f4a-891f-d7a6c67e8580"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6cd52b97-b4f7-41c7-a4df-37bf112fc672"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("89df28b2-b79b-41cb-84c9-8b30d16a66a1"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fa4c757a-cefe-498c-8f69-dc9338f59d8d"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fbdb89f8-ee46-4833-bcfa-de7431d3ad08"));

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "IsAvailable", "LicensePlate", "Model", "PricePerDay", "RentalPointId", "Seats", "Type", "Year" },
                values: new object[,]
                {
                    { new Guid("4c423274-ec42-4079-ac5e-6f330c8a62ca"), "Ford", true, "PO9012EF", "Focus", 40m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 4, "Car", 2019 },
                    { new Guid("58cddf25-1a9f-453e-98c2-c753903adeb3"), "Honda", true, "WA5678CD", "Civic", 45m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2021 },
                    { new Guid("b676eb66-d644-4045-8f0d-5411e2275535"), "Ford", true, "PO4012FF", "F-150", 80m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 2, "Truck", 2018 },
                    { new Guid("c1494ae8-fb1d-4e31-9c89-b0d939e8f469"), "Yamaha", true, "PO9014EL", "MT-07", 35m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 2, "Motorcycle", 2021 },
                    { new Guid("c6a8cd83-b80b-4caa-99cc-958acd4a94fb"), "Toyota", true, "KR7777AB", "Supra", 100m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 2, "Car", 2020 },
                    { new Guid("d7b77389-46b1-4754-a3ae-e6533dfe6f50"), "Toyota", true, "KR1234AB", "Camry", 50m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2020 }
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
                keyValue: new Guid("4c423274-ec42-4079-ac5e-6f330c8a62ca"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("58cddf25-1a9f-453e-98c2-c753903adeb3"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b676eb66-d644-4045-8f0d-5411e2275535"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c1494ae8-fb1d-4e31-9c89-b0d939e8f469"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c6a8cd83-b80b-4caa-99cc-958acd4a94fb"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d7b77389-46b1-4754-a3ae-e6533dfe6f50"));

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "IsAvailable", "LicensePlate", "Model", "PricePerDay", "RentalPointId", "Seats", "Type", "Year" },
                values: new object[,]
                {
                    { new Guid("2c903c06-07cb-4f4a-891f-d7a6c67e8580"), "Yamaha", true, "PO9014EL", "MT-07", 35m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 2, "Motorcycle", 2021 },
                    { new Guid("6cd52b97-b4f7-41c7-a4df-37bf112fc672"), "Toyota", true, "KR1234AB", "Camry", 50m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2020 },
                    { new Guid("89df28b2-b79b-41cb-84c9-8b30d16a66a1"), "Ford", true, "PO9012EF", "Focus", 40m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 4, "Car", 2019 },
                    { new Guid("fa4c757a-cefe-498c-8f69-dc9338f59d8d"), "Honda", true, "WA5678CD", "Civic", 45m, new Guid("550e8400-e29b-41d4-a716-446655440100"), 5, "Car", 2021 },
                    { new Guid("fbdb89f8-ee46-4833-bcfa-de7431d3ad08"), "Ford", true, "PO4012FF", "F-150", 80m, new Guid("550e8400-e29b-41d4-a716-446655440101"), 2, "Truck", 2018 }
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
