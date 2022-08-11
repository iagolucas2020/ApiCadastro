using Microsoft.EntityFrameworkCore.Migrations;

namespace Cadastro.Migrations
{
    public partial class atualizandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaJuridicaId",
                table: "Telefone",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PessoaJuridica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Razao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CidadeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaJuridica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaJuridica_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_PessoaJuridicaId",
                table: "Telefone",
                column: "PessoaJuridicaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaJuridica_CidadeId",
                table: "PessoaJuridica",
                column: "CidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_PessoaJuridica_PessoaJuridicaId",
                table: "Telefone",
                column: "PessoaJuridicaId",
                principalTable: "PessoaJuridica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_PessoaJuridica_PessoaJuridicaId",
                table: "Telefone");

            migrationBuilder.DropTable(
                name: "PessoaJuridica");

            migrationBuilder.DropIndex(
                name: "IX_Telefone_PessoaJuridicaId",
                table: "Telefone");

            migrationBuilder.DropColumn(
                name: "PessoaJuridicaId",
                table: "Telefone");
        }
    }
}
