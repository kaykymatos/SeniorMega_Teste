using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_Authentication.Migrations
{
    public partial class TableNameModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users_Table");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Table",
                table: "Users_Table",
                column: "Codigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Table",
                table: "Users_Table");

            migrationBuilder.RenameTable(
                name: "Users_Table",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Codigo");
        }
    }
}
