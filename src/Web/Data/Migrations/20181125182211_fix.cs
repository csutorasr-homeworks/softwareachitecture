using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameQuestion_GameSessions_GameId",
                table: "GameQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_GameQuestion_Questions_QuestionId",
                table: "GameQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedAnswer_Answers_AnswerId",
                table: "UserSelectedAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedAnswer_GameQuestion_GameQuestionId",
                table: "UserSelectedAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedAnswer_UserGameSessions_UserGameSessionId1",
                table: "UserSelectedAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSelectedAnswer",
                table: "UserSelectedAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserSelectedAnswer_UserGameSessionId1",
                table: "UserSelectedAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameQuestion",
                table: "GameQuestion");

            migrationBuilder.DropColumn(
                name: "UserGameSessionId1",
                table: "UserSelectedAnswer");

            migrationBuilder.RenameTable(
                name: "UserSelectedAnswer",
                newName: "UserSelectedAnswers");

            migrationBuilder.RenameTable(
                name: "GameQuestion",
                newName: "GameQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_UserSelectedAnswer_GameQuestionId",
                table: "UserSelectedAnswers",
                newName: "IX_UserSelectedAnswers_GameQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSelectedAnswer_AnswerId",
                table: "UserSelectedAnswers",
                newName: "IX_UserSelectedAnswers_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameQuestion_QuestionId",
                table: "GameQuestions",
                newName: "IX_GameQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_GameQuestion_GameId",
                table: "GameQuestions",
                newName: "IX_GameQuestions_GameId");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Answers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserGameSessionId",
                table: "UserSelectedAnswers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSelectedAnswers",
                table: "UserSelectedAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameQuestions",
                table: "GameQuestions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedAnswers_UserGameSessionId",
                table: "UserSelectedAnswers",
                column: "UserGameSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameQuestions_GameSessions_GameId",
                table: "GameQuestions",
                column: "GameId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameQuestions_Questions_QuestionId",
                table: "GameQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedAnswers_Answers_AnswerId",
                table: "UserSelectedAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedAnswers_GameQuestions_GameQuestionId",
                table: "UserSelectedAnswers",
                column: "GameQuestionId",
                principalTable: "GameQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedAnswers_UserGameSessions_UserGameSessionId",
                table: "UserSelectedAnswers",
                column: "UserGameSessionId",
                principalTable: "UserGameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameQuestions_GameSessions_GameId",
                table: "GameQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_GameQuestions_Questions_QuestionId",
                table: "GameQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedAnswers_Answers_AnswerId",
                table: "UserSelectedAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedAnswers_GameQuestions_GameQuestionId",
                table: "UserSelectedAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedAnswers_UserGameSessions_UserGameSessionId",
                table: "UserSelectedAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSelectedAnswers",
                table: "UserSelectedAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserSelectedAnswers_UserGameSessionId",
                table: "UserSelectedAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameQuestions",
                table: "GameQuestions");

            migrationBuilder.RenameTable(
                name: "UserSelectedAnswers",
                newName: "UserSelectedAnswer");

            migrationBuilder.RenameTable(
                name: "GameQuestions",
                newName: "GameQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_UserSelectedAnswers_GameQuestionId",
                table: "UserSelectedAnswer",
                newName: "IX_UserSelectedAnswer_GameQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSelectedAnswers_AnswerId",
                table: "UserSelectedAnswer",
                newName: "IX_UserSelectedAnswer_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameQuestions_QuestionId",
                table: "GameQuestion",
                newName: "IX_GameQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_GameQuestions_GameId",
                table: "GameQuestion",
                newName: "IX_GameQuestion_GameId");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Answers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "UserGameSessionId",
                table: "UserSelectedAnswer",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "UserGameSessionId1",
                table: "UserSelectedAnswer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSelectedAnswer",
                table: "UserSelectedAnswer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameQuestion",
                table: "GameQuestion",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedAnswer_UserGameSessionId1",
                table: "UserSelectedAnswer",
                column: "UserGameSessionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameQuestion_GameSessions_GameId",
                table: "GameQuestion",
                column: "GameId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameQuestion_Questions_QuestionId",
                table: "GameQuestion",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedAnswer_Answers_AnswerId",
                table: "UserSelectedAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedAnswer_GameQuestion_GameQuestionId",
                table: "UserSelectedAnswer",
                column: "GameQuestionId",
                principalTable: "GameQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedAnswer_UserGameSessions_UserGameSessionId1",
                table: "UserSelectedAnswer",
                column: "UserGameSessionId1",
                principalTable: "UserGameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
