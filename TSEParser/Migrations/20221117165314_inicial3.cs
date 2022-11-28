using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSEParser.Migrations
{
    public partial class inicial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dateTimeDBType = "datetime2";
            if (migrationBuilder.ActiveProvider.ToLower().Contains("mysql"))
                dateTimeDBType = "datetime";

            migrationBuilder.CreateTable(
                name: "Partido",
                columns: table => new
                {
                    Numero = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partido", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "Regiao",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeFederativa",
                columns: table => new
                {
                    Sigla = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Nome = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RegiaoId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeFederativa", x => x.Sigla);
                    table.ForeignKey(
                        name: "FK_UnidadeFederativa_Regiao_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    Cargo = table.Column<byte>(type: "tinyint", nullable: false),
                    NumeroCandidato = table.Column<int>(type: "int", nullable: false),
                    UFSigla = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Nome = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
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
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    UFSigla = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false)
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
                name: "DefeitosSecao",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: false),
                    CodigoSecao = table.Column<short>(type: "smallint", nullable: false),
                    SemArquivo = table.Column<bool>(type: "bit", nullable: false),
                    Rejeitado = table.Column<bool>(type: "bit", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false),
                    CodigoIdentificacaoUrnaEletronicaBU = table.Column<int>(type: "int", nullable: false),
                    ArquivoIMGBUFaltando = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoBUFaltando = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoRDVFaltando = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoLOGJEZFaltando = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoBUeIMGBUDiferentes = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoIMGBUCorrompido = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoBUCorrompido = table.Column<bool>(type: "bit", nullable: false),
                    ArquivoRDVCorrompido = table.Column<bool>(type: "bit", nullable: false),
                    DiferencaVotosBUeIMGBU = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefeitosSecao", x => new { x.MunicipioCodigo, x.CodigoZonaEleitoral, x.CodigoSecao });
                    table.ForeignKey(
                        name: "FK_DefeitosSecao_Municipio_MunicipioCodigo",
                        column: x => x.MunicipioCodigo,
                        principalTable: "Municipio",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecaoEleitoral",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: false),
                    CodigoSecao = table.Column<short>(type: "smallint", nullable: false),
                    CodigoLocalVotacao = table.Column<short>(type: "smallint", nullable: false),
                    EleitoresAptos = table.Column<short>(type: "smallint", nullable: false),
                    Comparecimento = table.Column<short>(type: "smallint", nullable: false),
                    EleitoresFaltosos = table.Column<short>(type: "smallint", nullable: false),
                    HabilitadosPorAnoNascimento = table.Column<short>(type: "smallint", nullable: false),
                    CodigoIdentificacaoUrnaEletronica = table.Column<int>(type: "int", nullable: false),
                    AberturaUrnaEletronica = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    FechamentoUrnaEletronica = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    Zeresima = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    DF_EleitoresAptos = table.Column<short>(type: "smallint", nullable: false),
                    DF_VotosNominais = table.Column<short>(type: "smallint", nullable: false),
                    DF_VotosLegenda = table.Column<short>(type: "smallint", nullable: false),
                    DF_Brancos = table.Column<short>(type: "smallint", nullable: false),
                    DF_Nulos = table.Column<short>(type: "smallint", nullable: false),
                    DF_Total = table.Column<short>(type: "smallint", nullable: false),
                    DE_EleitoresAptos = table.Column<short>(type: "smallint", nullable: false),
                    DE_VotosNominais = table.Column<short>(type: "smallint", nullable: false),
                    DE_VotosLegenda = table.Column<short>(type: "smallint", nullable: false),
                    DE_Brancos = table.Column<short>(type: "smallint", nullable: false),
                    DE_Nulos = table.Column<short>(type: "smallint", nullable: false),
                    DE_Total = table.Column<short>(type: "smallint", nullable: false),
                    SE_EleitoresAptos = table.Column<short>(type: "smallint", nullable: false),
                    SE_VotosNominais = table.Column<short>(type: "smallint", nullable: false),
                    SE_Brancos = table.Column<short>(type: "smallint", nullable: false),
                    SE_Nulos = table.Column<short>(type: "smallint", nullable: false),
                    SE_Total = table.Column<short>(type: "smallint", nullable: false),
                    GO_EleitoresAptos = table.Column<short>(type: "smallint", nullable: false),
                    GO_VotosNominais = table.Column<short>(type: "smallint", nullable: false),
                    GO_Brancos = table.Column<short>(type: "smallint", nullable: false),
                    GO_Nulos = table.Column<short>(type: "smallint", nullable: false),
                    GO_Total = table.Column<short>(type: "smallint", nullable: false),
                    PR_EleitoresAptos = table.Column<short>(type: "smallint", nullable: false),
                    PR_VotosNominais = table.Column<short>(type: "smallint", nullable: false),
                    PR_Brancos = table.Column<short>(type: "smallint", nullable: false),
                    PR_Nulos = table.Column<short>(type: "smallint", nullable: false),
                    PR_Total = table.Column<short>(type: "smallint", nullable: false),
                    LogUrnaInconsistente = table.Column<bool>(type: "bit", nullable: false),
                    ModeloUrnaEletronica = table.Column<short>(type: "smallint", nullable: false),
                    ResultadoSistemaApuracao = table.Column<bool>(type: "bit", nullable: false),
                    AberturaUELog = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    FechamentoUELog = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    QtdJustificativasLog = table.Column<short>(type: "smallint", nullable: false),
                    QtdJaVotouLog = table.Column<short>(type: "smallint", nullable: false),
                    CodigoIdentificacaoUrnaEletronicaLog = table.Column<int>(type: "int", nullable: false)
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
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<byte>(type: "tinyint", nullable: false),
                    NumeroCandidato = table.Column<int>(type: "int", nullable: false),
                    QtdVotos = table.Column<long>(type: "bigint", nullable: false),
                    VotoLegenda = table.Column<bool>(type: "bit", nullable: false)
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
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: false),
                    CodigoSecao = table.Column<short>(type: "smallint", nullable: false),
                    IdVotoLog = table.Column<short>(type: "smallint", nullable: false),
                    LinhaLog = table.Column<int>(type: "int", nullable: false),
                    LinhaLogFim = table.Column<int>(type: "int", nullable: false),
                    InicioVoto = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    HabilitacaoUrna = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    FimVoto = table.Column<DateTime>(type: dateTimeDBType, nullable: false),
                    PossuiBiometria = table.Column<bool>(type: "bit", nullable: false),
                    DedoBiometria = table.Column<byte>(type: "tinyint", nullable: false),
                    ScoreBiometria = table.Column<short>(type: "smallint", nullable: false),
                    HabilitacaoCancelada = table.Column<bool>(type: "bit", nullable: false),
                    VotouDF = table.Column<bool>(type: "bit", nullable: false),
                    VotouDE = table.Column<bool>(type: "bit", nullable: false),
                    VotouSE = table.Column<bool>(type: "bit", nullable: false),
                    VotouGO = table.Column<bool>(type: "bit", nullable: false),
                    VotouPR = table.Column<bool>(type: "bit", nullable: false),
                    VotoNuloSuspensaoDF = table.Column<bool>(type: "bit", nullable: false),
                    VotoNuloSuspensaoDE = table.Column<bool>(type: "bit", nullable: false),
                    VotoNuloSuspensaoSE = table.Column<bool>(type: "bit", nullable: false),
                    VotoNuloSuspensaoGO = table.Column<bool>(type: "bit", nullable: false),
                    VotoNuloSuspensaoPR = table.Column<bool>(type: "bit", nullable: false),
                    VotoComputado = table.Column<bool>(type: "bit", nullable: false),
                    QtdTeclasIndevidas = table.Column<byte>(type: "tinyint", nullable: false),
                    EleitorSuspenso = table.Column<bool>(type: "bit", nullable: false),
                    ModeloUrnaEletronica = table.Column<short>(type: "smallint", nullable: false),
                    CodigoIdentificacaoUrnaEletronica = table.Column<int>(type: "int", nullable: false)
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
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: false),
                    CodigoSecao = table.Column<short>(type: "smallint", nullable: false),
                    Cargo = table.Column<byte>(type: "tinyint", nullable: false),
                    NumeroCandidato = table.Column<int>(type: "int", nullable: false),
                    QtdVotos = table.Column<short>(type: "smallint", nullable: false),
                    VotoLegenda = table.Column<bool>(type: "bit", nullable: false)
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
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: false),
                    CodigoSecao = table.Column<short>(type: "smallint", nullable: false),
                    IdVotoRDV = table.Column<short>(type: "smallint", nullable: false),
                    Cargo = table.Column<byte>(type: "tinyint", nullable: false),
                    NumeroCandidato = table.Column<int>(type: "int", nullable: false),
                    QtdVotos = table.Column<short>(type: "smallint", nullable: false),
                    VotoLegenda = table.Column<bool>(type: "bit", nullable: false),
                    VotoNulo = table.Column<bool>(type: "bit", nullable: false),
                    VotoBranco = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Regiao",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { (byte)0, "Brasil" },
                    { (byte)1, "Sul" },
                    { (byte)2, "Sudeste" },
                    { (byte)3, "Centro-oeste" },
                    { (byte)4, "Norte" },
                    { (byte)5, "Nordeste" },
                    { (byte)6, "Exterior" }
                });

            migrationBuilder.InsertData(
                table: "UnidadeFederativa",
                columns: new[] { "Sigla", "Nome", "RegiaoId" },
                values: new object[,]
                {
                    { "AC", "ACRE", (byte)4 },
                    { "AL", "ALAGOAS", (byte)5 },
                    { "AM", "AMAZONAS", (byte)4 },
                    { "AP", "AMAPÁ", (byte)4 },
                    { "BA", "BAHIA", (byte)5 },
                    { "BR", "FED - BRASIL", (byte)0 },
                    { "CE", "CEARÁ", (byte)5 },
                    { "DF", "DISTRITO FEDERAL", (byte)3 },
                    { "ES", "ESPÍRITO SANTO", (byte)2 },
                    { "GO", "GOIÁS", (byte)3 },
                    { "MA", "MARANHÃO", (byte)5 },
                    { "MG", "MINAS GERAIS", (byte)2 },
                    { "MS", "MATO GROSSO DO SUL", (byte)3 },
                    { "MT", "MATO GROSSO", (byte)3 },
                    { "PA", "PARÁ", (byte)4 },
                    { "PB", "PARAÍBA", (byte)5 },
                    { "PE", "PERNAMBUCO", (byte)5 },
                    { "PI", "PIAUÍ", (byte)5 },
                    { "PR", "PARANÁ", (byte)1 },
                    { "RJ", "RIO DE JANEIRO", (byte)2 },
                    { "RN", "RIO GRANDE DO NORTE", (byte)5 },
                    { "RO", "RONDÔNIA", (byte)4 },
                    { "RR", "RORAIMA", (byte)4 },
                    { "RS", "RIO GRANDE DO SUL", (byte)1 },
                    { "SC", "SANTA CATARINA", (byte)1 },
                    { "SE", "SERGIPE", (byte)5 },
                    { "SP", "SÃO PAULO", (byte)2 },
                    { "TO", "TOCANTINS", (byte)4 },
                    { "ZZ", "EXTERIOR", (byte)6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_UFSigla",
                table: "Candidato",
                column: "UFSigla");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_UFSigla",
                table: "Municipio",
                column: "UFSigla");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeFederativa_RegiaoId",
                table: "UnidadeFederativa",
                column: "RegiaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropTable(
                name: "DefeitosSecao");

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

            migrationBuilder.DropTable(
                name: "Regiao");
        }
    }
}
