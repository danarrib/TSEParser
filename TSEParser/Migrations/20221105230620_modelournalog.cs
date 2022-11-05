using Microsoft.EntityFrameworkCore.Migrations;

namespace TSEParser.Migrations
{
    public partial class modelournalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ModeloUrnaEletronica",
                table: "VotosLog",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeloUrnaEletronica",
                table: "VotosLog");
        }
    }
}
