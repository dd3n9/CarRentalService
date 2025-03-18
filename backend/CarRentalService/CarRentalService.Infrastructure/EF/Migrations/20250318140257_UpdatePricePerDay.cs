using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalService.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePricePerDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Vehicles",
                newName: "PricePerDay");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "Vehicles",
                newName: "Price");
        }
    }
}
