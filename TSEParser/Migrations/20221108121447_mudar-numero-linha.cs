using Microsoft.EntityFrameworkCore.Migrations;

namespace TSEParser.Migrations
{
    public partial class mudarnumerolinha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LinhaLogFim",
                table: "VotosLog",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "LinhaLog",
                table: "VotosLog",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "LinhaLogFim",
                table: "VotosLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "LinhaLog",
                table: "VotosLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
