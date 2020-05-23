using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetJams.Data.Migrations
{
    public partial class AddSongReleaseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReleaseDate",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Songs");
        }
    }
}
