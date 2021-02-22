using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaApp.Migrations
{
    public partial class AddDbSetAutores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorObra_Autor_AutoresId",
                table: "AutorObra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Autor",
                table: "Autor");

            migrationBuilder.RenameTable(
                name: "Autor",
                newName: "Autores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autores",
                table: "Autores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorObra_Autores_AutoresId",
                table: "AutorObra",
                column: "AutoresId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorObra_Autores_AutoresId",
                table: "AutorObra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Autores",
                table: "Autores");

            migrationBuilder.RenameTable(
                name: "Autores",
                newName: "Autor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autor",
                table: "Autor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorObra_Autor_AutoresId",
                table: "AutorObra",
                column: "AutoresId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
