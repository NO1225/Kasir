using Microsoft.EntityFrameworkCore.Migrations;

namespace Kasir.Infrastructure.Persistence.Migrations
{
    public partial class Addingdefaultimageforwords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Words",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Words");
        }
    }
}
