using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class useridstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGameSessions_AspNetUsers_UserId1",
                table: "UserGameSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserGameSessions_UserId1",
                table: "UserGameSessions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserGameSessions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserGameSessions",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSessions_UserId",
                table: "UserGameSessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGameSessions_AspNetUsers_UserId",
                table: "UserGameSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGameSessions_AspNetUsers_UserId",
                table: "UserGameSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserGameSessions_UserId",
                table: "UserGameSessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserGameSessions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserGameSessions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSessions_UserId1",
                table: "UserGameSessions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGameSessions_AspNetUsers_UserId1",
                table: "UserGameSessions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
