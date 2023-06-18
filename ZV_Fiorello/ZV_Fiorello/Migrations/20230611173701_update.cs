using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZV_Fiorello.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SliderContent",
                table: "SliderContent");

            migrationBuilder.RenameTable(
                name: "SliderContent",
                newName: "SliderContents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SliderContents",
                table: "SliderContents",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SliderContents",
                table: "SliderContents");

            migrationBuilder.RenameTable(
                name: "SliderContents",
                newName: "SliderContent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SliderContent",
                table: "SliderContent",
                column: "Id");
        }
    }
}
