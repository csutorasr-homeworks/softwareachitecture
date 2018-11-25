using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class userselectedanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentQuestion",
                table: "GameSessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "GameQuestion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "UserSelectedAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameQuestionId = table.Column<Guid>(nullable: false),
                    UserGameSessionId = table.Column<string>(nullable: true),
                    UserGameSessionId1 = table.Column<Guid>(nullable: true),
                    AnswerId = table.Column<Guid>(nullable: false),
                    AnswerTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSelectedAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSelectedAnswer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSelectedAnswer_GameQuestion_GameQuestionId",
                        column: x => x.GameQuestionId,
                        principalTable: "GameQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSelectedAnswer_UserGameSessions_UserGameSessionId1",
                        column: x => x.UserGameSessionId1,
                        principalTable: "UserGameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedAnswer_AnswerId",
                table: "UserSelectedAnswer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedAnswer_GameQuestionId",
                table: "UserSelectedAnswer",
                column: "GameQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedAnswer_UserGameSessionId1",
                table: "UserSelectedAnswer",
                column: "UserGameSessionId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSelectedAnswer");

            migrationBuilder.DropColumn(
                name: "CurrentQuestion",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "GameQuestion");
        }
    }
}
