using Microsoft.EntityFrameworkCore.Migrations;

namespace SisClientes.Migrations
{
    public partial class IncluindoEnderecoPrincipal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EhPrincipal",
                table: "Endereco",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EhPrincipal",
                table: "Endereco");
        }
    }
}
