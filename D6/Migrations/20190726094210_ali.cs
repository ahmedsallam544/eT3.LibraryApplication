using Microsoft.EntityFrameworkCore.Migrations;

namespace eT3.LibraryApplication.Migrations
{
    public partial class ali : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "User_ID",
                table: "Borrowers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "User_ID",
                table: "Borrowers",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
