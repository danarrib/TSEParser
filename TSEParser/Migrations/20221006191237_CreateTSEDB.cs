using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TSEParser.Migrations
{
    public partial class CreateTSEDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partido",
                columns: table => new
                {
                    Numero = table.Column<byte>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partido", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeFederativa",
                columns: table => new
                {
                    Sigla = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeFederativa", x => x.Sigla);
                });

            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    Cargo = table.Column<byte>(nullable: false),
                    NumeroCandidato = table.Column<int>(nullable: false),
                    UFSigla = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => new { x.Cargo, x.NumeroCandidato, x.UFSigla });
                    table.ForeignKey(
                        name: "FK_Candidato_UnidadeFederativa_UFSigla",
                        column: x => x.UFSigla,
                        principalTable: "UnidadeFederativa",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    UFSigla = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Municipio_UnidadeFederativa_UFSigla",
                        column: x => x.UFSigla,
                        principalTable: "UnidadeFederativa",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecaoEleitoral",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(nullable: false),
                    CodigoSecao = table.Column<short>(nullable: false),
                    CodigoLocalVotacao = table.Column<short>(nullable: false),
                    EleitoresAptos = table.Column<short>(nullable: false),
                    Comparecimento = table.Column<short>(nullable: false),
                    EleitoresFaltosos = table.Column<short>(nullable: false),
                    HabilitadosPorAnoNascimento = table.Column<short>(nullable: false),
                    CodigoIdentificacaoUrnaEletronica = table.Column<int>(nullable: false),
                    AberturaUrnaEletronica = table.Column<DateTime>(nullable: false),
                    FechamentoUrnaEletronica = table.Column<DateTime>(nullable: false),
                    DF_EleitoresAptos = table.Column<short>(nullable: false),
                    DF_VotosNominais = table.Column<short>(nullable: false),
                    DF_VotosLegenda = table.Column<short>(nullable: false),
                    DF_Brancos = table.Column<short>(nullable: false),
                    DF_Nulos = table.Column<short>(nullable: false),
                    DF_Total = table.Column<short>(nullable: false),
                    DE_EleitoresAptos = table.Column<short>(nullable: false),
                    DE_VotosNominais = table.Column<short>(nullable: false),
                    DE_VotosLegenda = table.Column<short>(nullable: false),
                    DE_Brancos = table.Column<short>(nullable: false),
                    DE_Nulos = table.Column<short>(nullable: false),
                    DE_Total = table.Column<short>(nullable: false),
                    SE_EleitoresAptos = table.Column<short>(nullable: false),
                    SE_VotosNominais = table.Column<short>(nullable: false),
                    SE_Brancos = table.Column<short>(nullable: false),
                    SE_Nulos = table.Column<short>(nullable: false),
                    SE_Total = table.Column<short>(nullable: false),
                    GO_EleitoresAptos = table.Column<short>(nullable: false),
                    GO_VotosNominais = table.Column<short>(nullable: false),
                    GO_Brancos = table.Column<short>(nullable: false),
                    GO_Nulos = table.Column<short>(nullable: false),
                    GO_Total = table.Column<short>(nullable: false),
                    PR_EleitoresAptos = table.Column<short>(nullable: false),
                    PR_VotosNominais = table.Column<short>(nullable: false),
                    PR_Brancos = table.Column<short>(nullable: false),
                    PR_Nulos = table.Column<short>(nullable: false),
                    PR_Total = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecaoEleitoral", x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao });
                    table.ForeignKey(
                        name: "FK_SecaoEleitoral_Municipio_MunicipioCodigo",
                        column: x => x.MunicipioCodigo,
                        principalTable: "Municipio",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalheVoto",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(nullable: false),
                    CodigoSecao = table.Column<short>(nullable: false),
                    Cargo = table.Column<byte>(nullable: false),
                    NumeroCandidato = table.Column<int>(nullable: false),
                    QtdVotos = table.Column<short>(nullable: false),
                    VotoLegenda = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheVoto", x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao, x.Cargo, x.NumeroCandidato });
                    table.ForeignKey(
                        name: "FK_DetalheVoto_SecaoEleitoral_MunicipioCodigo_CodigoZonaEleitoral_CodigoSecao",
                        columns: x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao },
                        principalTable: "SecaoEleitoral",
                        principalColumns: new[] { "MunicipioCodigo", "CodigoZonaEleitoral", "CodigoSecao" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_UFSigla",
                table: "Candidato",
                column: "UFSigla");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_UFSigla",
                table: "Municipio",
                column: "UFSigla");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropTable(
                name: "DetalheVoto");

            migrationBuilder.DropTable(
                name: "Partido");

            migrationBuilder.DropTable(
                name: "SecaoEleitoral");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "UnidadeFederativa");
        }
    }
}
