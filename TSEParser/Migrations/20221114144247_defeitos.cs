using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSEParser.Migrations
{
    public partial class defeitos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefeitosSecao",
                columns: table => new
                {
                    MunicipioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: false),
                    CodigoSecao = table.Column<short>(type: "smallint", nullable: false),
                    SecaoEleitoralMunicipioCodigo = table.Column<int>(type: "int", nullable: true),
                    SecaoEleitoralCodigoZonaEleitoral = table.Column<short>(type: "smallint", nullable: true),
                    SecaoEleitoralCodigoSecao = table.Column<short>(type: "smallint", nullable: true),
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
                        name: "FK_DefeitosSecao_SecaoEleitoral_SecaoEleitoralMunicipioCodigo_SecaoEleitoralCodigoZonaEleitoral_SecaoEleitoralCodigoSecao",
                        columns: x => new { x.SecaoEleitoralMunicipioCodigo, x.SecaoEleitoralCodigoZonaEleitoral, x.SecaoEleitoralCodigoSecao },
                        principalTable: "SecaoEleitoral",
                        principalColumns: new[] { "MunicipioCodigo", "CodigoZonaEleitoral", "CodigoSecao" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefeitosSecao_SecaoEleitoralMunicipioCodigo_SecaoEleitoralCodigoZonaEleitoral_SecaoEleitoralCodigoSecao",
                table: "DefeitosSecao",
                columns: new[] { "SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefeitosSecao");
        }
    }
}
