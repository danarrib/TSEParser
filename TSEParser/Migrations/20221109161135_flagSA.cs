using Microsoft.EntityFrameworkCore.Migrations;

namespace TSEParser.Migrations
{
    public partial class flagSA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ResultadoSistemaApuracao",
                table: "SecaoEleitoral",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultadoSistemaApuracao",
                table: "SecaoEleitoral");
        }
    }
}
