using Microsoft.EntityFrameworkCore.Migrations;

namespace CT.Data.Migrations
{
    public partial class AddCamposMedicoEspecialidadeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadeMedico_Especialidade_EspecialidadesId",
                table: "EspecialidadeMedico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidade",
                table: "Especialidade");

            migrationBuilder.RenameTable(
                name: "Especialidade",
                newName: "Especialidades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadeMedico_Especialidades_EspecialidadesId",
                table: "EspecialidadeMedico",
                column: "EspecialidadesId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadeMedico_Especialidades_EspecialidadesId",
                table: "EspecialidadeMedico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades");

            migrationBuilder.RenameTable(
                name: "Especialidades",
                newName: "Especialidade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidade",
                table: "Especialidade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadeMedico_Especialidade_EspecialidadesId",
                table: "EspecialidadeMedico",
                column: "EspecialidadesId",
                principalTable: "Especialidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
