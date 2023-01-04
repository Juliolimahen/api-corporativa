using Microsoft.EntityFrameworkCore.Migrations;

namespace CT.Data.Migrations
{
    public partial class AddCamposCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Clientes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Clientes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
