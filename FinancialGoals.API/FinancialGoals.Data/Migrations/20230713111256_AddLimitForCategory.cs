using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoals.Data.Migrations
{
    public partial class AddLimitForCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Limit",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Categories");
        }
    }
}
