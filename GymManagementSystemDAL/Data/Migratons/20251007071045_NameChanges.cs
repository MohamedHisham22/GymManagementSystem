using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystemDAL.Data.Migratons
{
    /// <inheritdoc />
    public partial class NameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Trainers",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Trainers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Address_BuildingNumber",
                table: "Trainers",
                newName: "BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Members",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Members",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Address_BuildingNumber",
                table: "Members",
                newName: "BuildingNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Trainers",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Trainers",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "Trainers",
                newName: "Address_BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Members",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Members",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "Members",
                newName: "Address_BuildingNumber");
        }
    }
}
