using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCBasico.Migrations
{
    public partial class MVCBasicoContextSubastaDatabaseContext_AgregadoImagenProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ArchivoImagen",
                table: "Articulo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Articulo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Articulo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NombreImagen",
                table: "Articulo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoImagen",
                table: "Articulo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreadorId",
                table: "Articulo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_UsuarioCreadorId",
                table: "Articulo",
                column: "UsuarioCreadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulo_Usuarios_UsuarioCreadorId",
                table: "Articulo",
                column: "UsuarioCreadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulo_Usuarios_UsuarioCreadorId",
                table: "Articulo");

            migrationBuilder.DropIndex(
                name: "IX_Articulo_UsuarioCreadorId",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "ArchivoImagen",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "NombreImagen",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "TipoImagen",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "UsuarioCreadorId",
                table: "Articulo");
        }
    }
}
