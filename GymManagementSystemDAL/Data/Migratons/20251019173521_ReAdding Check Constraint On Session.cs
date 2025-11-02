using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystemDAL.Data.Migratons
{
    /// <inheritdoc />
    public partial class ReAddingCheckConstraintOnSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "EndDateCheck",
                table: "Sessions",
                sql: "[EndDate] > [StartDate]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EndDateCheck",
                table: "Sessions");
        }
    }
}
