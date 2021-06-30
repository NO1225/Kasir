using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kasir.Infrastructure.Persistence.Migrations
{
    public partial class addingappinfolanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppInfos");

            migrationBuilder.CreateTable(
                name: "AppInfoLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppInfoId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInfoLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInfoLanguages_AppInfos_AppInfoId",
                        column: x => x.AppInfoId,
                        principalTable: "AppInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppInfoLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInfoLanguages_AppInfoId",
                table: "AppInfoLanguages",
                column: "AppInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfoLanguages_LanguageId",
                table: "AppInfoLanguages",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInfoLanguages");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppInfos",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AppInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
