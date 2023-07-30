using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoals.Data.Migrations
{
    public partial class AddNumberPropertyForFinancialAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "FinancialAccounts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "FinancialAccounts");
        }
    }
}
