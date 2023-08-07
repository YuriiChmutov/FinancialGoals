using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoals.Data.Migrations
{
    public partial class AddTransactionTypeForCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Categories");
        }
    }
}
