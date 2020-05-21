using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetJams.Data.Migrations
{
    public partial class RefineSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongName",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SongUrl",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Songs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Songs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongUrl",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "SongName",
                table: "Songs",
                type: "text",
                nullable: true);
        }
    }
}
