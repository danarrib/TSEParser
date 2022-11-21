using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSEParser.Migrations
{
    public partial class lognovoscampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CodigoSecaoLog",
                table: "VotosLog",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "CodigoZonaEleitoralLog",
                table: "VotosLog",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioCodigoLog",
                table: "VotosLog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UrnaTestada",
                table: "VotosLog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoSecaoLog",
                table: "VotosLog");

            migrationBuilder.DropColumn(
                name: "CodigoZonaEleitoralLog",
                table: "VotosLog");

            migrationBuilder.DropColumn(
                name: "MunicipioCodigoLog",
                table: "VotosLog");

            migrationBuilder.DropColumn(
                name: "UrnaTestada",
                table: "VotosLog");
        }
    }
}
