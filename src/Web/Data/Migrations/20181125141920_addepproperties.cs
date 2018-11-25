using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class addepproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "UserGameSessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Finnished",
                table: "GameSessions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InProgress",
                table: "GameSessions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WaitingForPlayers",
                table: "GameSessions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "UserGameSessions");

            migrationBuilder.DropColumn(
                name: "Finnished",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "InProgress",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "WaitingForPlayers",
                table: "GameSessions");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
