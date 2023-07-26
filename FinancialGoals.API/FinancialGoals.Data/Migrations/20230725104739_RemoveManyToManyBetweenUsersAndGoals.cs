using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoals.Data.Migrations
{
    public partial class RemoveManyToManyBetweenUsersAndGoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryUser");

            migrationBuilder.CreateTable(
                name: "CategoryFinancialAccount",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    FinancialAccountsFinancialAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFinancialAccount", x => new { x.CategoriesCategoryId, x.FinancialAccountsFinancialAccountId });
                    table.ForeignKey(
                        name: "FK_CategoryFinancialAccount_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFinancialAccount_FinancialAccounts_FinancialAccountsFinancialAccountId",
                        column: x => x.FinancialAccountsFinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "FinancialAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFinancialAccount_FinancialAccountsFinancialAccountId",
                table: "CategoryFinancialAccount",
                column: "FinancialAccountsFinancialAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryFinancialAccount");

            migrationBuilder.CreateTable(
                name: "CategoryUser",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryUser", x => new { x.CategoriesCategoryId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_CategoryUser_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUser_UsersUserId",
                table: "CategoryUser",
                column: "UsersUserId");
        }
    }
}
