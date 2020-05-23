using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetJams.Data.Migrations
{
    public partial class UpdateSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongTitle",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongTitle",
                table: "Songs");
        }
    }
}
