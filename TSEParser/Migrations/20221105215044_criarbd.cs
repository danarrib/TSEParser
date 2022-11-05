using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TSEParser.Migrations
{
    public partial class criarbd : Migration
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
                    Zeresima = table.Column<DateTime>(nullable: false),
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
                    PR_Total = table.Column<short>(nullable: false),
                    LogUrnaInconsistente = table.Column<bool>(nullable: false),
                    ModeloUrnaEletronica = table.Column<short>(nullable: false)
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
                name: "VotosMunicipio",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(nullable: false),
                    Cargo = table.Column<byte>(nullable: false),
                    NumeroCandidato = table.Column<int>(nullable: false),
                    QtdVotos = table.Column<long>(nullable: false),
                    VotoLegenda = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotosMunicipio", x => new { x.MunicipioCodigo, x.Cargo, x.NumeroCandidato });
                    table.ForeignKey(
                        name: "FK_VotosMunicipio_Municipio_MunicipioCodigo",
                        column: x => x.MunicipioCodigo,
                        principalTable: "Municipio",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VotosLog",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(nullable: false),
                    CodigoSecao = table.Column<short>(nullable: false),
                    IdVotoLog = table.Column<short>(nullable: false),
                    LinhaLog = table.Column<short>(nullable: false),
                    LinhaLogFim = table.Column<short>(nullable: false),
                    InicioVoto = table.Column<DateTime>(nullable: false),
                    HabilitacaoUrna = table.Column<DateTime>(nullable: false),
                    FimVoto = table.Column<DateTime>(nullable: false),
                    PossuiBiometria = table.Column<bool>(nullable: false),
                    DedoBiometria = table.Column<byte>(nullable: false),
                    ScoreBiometria = table.Column<short>(nullable: false),
                    HabilitacaoCancelada = table.Column<bool>(nullable: false),
                    VotouDF = table.Column<bool>(nullable: false),
                    VotouDE = table.Column<bool>(nullable: false),
                    VotouSE = table.Column<bool>(nullable: false),
                    VotouGO = table.Column<bool>(nullable: false),
                    VotouPR = table.Column<bool>(nullable: false),
                    VotoNuloSuspensaoDF = table.Column<bool>(nullable: false),
                    VotoNuloSuspensaoDE = table.Column<bool>(nullable: false),
                    VotoNuloSuspensaoSE = table.Column<bool>(nullable: false),
                    VotoNuloSuspensaoGO = table.Column<bool>(nullable: false),
                    VotoNuloSuspensaoPR = table.Column<bool>(nullable: false),
                    VotoComputado = table.Column<bool>(nullable: false),
                    QtdTeclasIndevidas = table.Column<byte>(nullable: false),
                    EleitorSuspenso = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotosLog", x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao, x.IdVotoLog });
                    table.ForeignKey(
                        name: "FK_VotosLog_SecaoEleitoral_MunicipioCodigo_CodigoZonaEleitoral_CodigoSecao",
                        columns: x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao },
                        principalTable: "SecaoEleitoral",
                        principalColumns: new[] { "MunicipioCodigo", "CodigoZonaEleitoral", "CodigoSecao" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VotosSecao",
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
                    table.PrimaryKey("PK_VotosSecao", x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao, x.Cargo, x.NumeroCandidato });
                    table.ForeignKey(
                        name: "FK_VotosSecao_SecaoEleitoral_MunicipioCodigo_CodigoZonaEleitoral_CodigoSecao",
                        columns: x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao },
                        principalTable: "SecaoEleitoral",
                        principalColumns: new[] { "MunicipioCodigo", "CodigoZonaEleitoral", "CodigoSecao" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VotosSecaoRDV",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(nullable: false),
                    CodigoSecao = table.Column<short>(nullable: false),
                    IdVotoRDV = table.Column<short>(nullable: false),
                    Cargo = table.Column<byte>(nullable: false),
                    NumeroCandidato = table.Column<int>(nullable: false),
                    QtdVotos = table.Column<short>(nullable: false),
                    VotoLegenda = table.Column<bool>(nullable: false),
                    VotoNulo = table.Column<bool>(nullable: false),
                    VotoBranco = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotosSecaoRDV", x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao, x.IdVotoRDV });
                    table.ForeignKey(
                        name: "FK_VotosSecaoRDV_SecaoEleitoral_MunicipioCodigo_CodigoZonaEleitoral_CodigoSecao",
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
                name: "Partido");

            migrationBuilder.DropTable(
                name: "VotosLog");

            migrationBuilder.DropTable(
                name: "VotosMunicipio");

            migrationBuilder.DropTable(
                name: "VotosSecao");

            migrationBuilder.DropTable(
                name: "VotosSecaoRDV");

            migrationBuilder.DropTable(
                name: "SecaoEleitoral");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "UnidadeFederativa");
        }
    }
}
