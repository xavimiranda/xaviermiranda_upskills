using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaApp.Migrations
{
    public partial class UltimaRequisicaoAddedToLeitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaRequesicao",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimaRequesicao",
                table: "AspNetUsers");
        }
    }
}
