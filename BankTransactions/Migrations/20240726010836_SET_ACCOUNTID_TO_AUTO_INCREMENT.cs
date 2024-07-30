using Microsoft.EntityFrameworkCore.Migrations;

namespace BankTransactions.Migrations
{
    public partial class SET_ACCOUNTID_TO_AUTO_INCREMENT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_AccountID",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_AccountID",
                table: "Accounts",
                column: "AccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                columns: new[] { "AccountID", "CustomerID" });
        }
    }
}
