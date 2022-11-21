using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSEParser.Migrations
{
    public partial class fusohorario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "FusoHorario",
                table: "Municipio",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FusoHorario",
                table: "Municipio");
        }
    }
}
