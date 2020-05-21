using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetJams.Data.Migrations
{
    public partial class ChangeSongProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongName",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongName",
                table: "Songs");
        }
    }
}
