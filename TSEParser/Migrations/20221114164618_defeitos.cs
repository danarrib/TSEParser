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
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefeitosSecao");
        }
    }
}
