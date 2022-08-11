using Microsoft.EntityFrameworkCore.Migrations;

namespace Cadastro.Migrations
{
    public partial class atualizandoDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaFisica_Cidade_CidadeId",
                table: "PessoaFisica");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaJuridica_Cidade_CidadeId",
                table: "PessoaJuridica");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Telefone",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CidadeId",
                table: "PessoaJuridica",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CidadeId",
                table: "PessoaFisica",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaFisica_Cidade_CidadeId",
                table: "PessoaFisica",
                column: "CidadeId",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaJuridica_Cidade_CidadeId",
                table: "PessoaJuridica",
                column: "CidadeId",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaFisica_Cidade_CidadeId",
                table: "PessoaFisica");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaJuridica_Cidade_CidadeId",
                table: "PessoaJuridica");

            migrationBuilder.AlterColumn<int>(
                name: "Descricao",
                table: "Telefone",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CidadeId",
                table: "PessoaJuridica",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CidadeId",
                table: "PessoaFisica",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaFisica_Cidade_CidadeId",
                table: "PessoaFisica",
                column: "CidadeId",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaJuridica_Cidade_CidadeId",
                table: "PessoaJuridica",
                column: "CidadeId",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
