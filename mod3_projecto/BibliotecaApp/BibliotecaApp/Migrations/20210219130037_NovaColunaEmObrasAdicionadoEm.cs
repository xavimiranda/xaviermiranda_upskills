using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaApp.Migrations
{
    public partial class NovaColunaEmObrasAdicionadoEm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AdicionadoEm",
                table: "Obras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdicionadoEm",
                table: "Obras");
        }
    }
}
