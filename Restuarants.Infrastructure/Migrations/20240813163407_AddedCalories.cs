using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCalories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Dishes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Dishes");
        }
    }
}
