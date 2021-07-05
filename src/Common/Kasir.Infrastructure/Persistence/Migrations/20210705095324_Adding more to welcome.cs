using Microsoft.EntityFrameworkCore.Migrations;

namespace Kasir.Infrastructure.Persistence.Migrations
{
    public partial class Addingmoretowelcome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Disclaimer",
                table: "AppInfoLanguages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Disclaimer");

            migrationBuilder.AddColumn<string>(
                name: "Welcome",
                table: "AppInfoLanguages",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "Welcome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disclaimer",
                table: "AppInfoLanguages");

            migrationBuilder.DropColumn(
                name: "Welcome",
                table: "AppInfoLanguages");
        }
    }
}
