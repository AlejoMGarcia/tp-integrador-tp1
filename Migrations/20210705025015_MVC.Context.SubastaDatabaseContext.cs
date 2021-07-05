using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCBasico.Migrations
{
    public partial class MVCContextSubastaDatabaseContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Subastas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Subastas",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Subastas");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Subastas");
        }
    }
}
