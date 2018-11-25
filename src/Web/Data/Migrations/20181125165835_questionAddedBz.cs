using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class questionAddedBz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddedById",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById1",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AddedById1",
                table: "Questions",
                column: "AddedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_AddedById1",
                table: "Questions",
                column: "AddedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_AddedById1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_AddedById1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AddedById1",
                table: "Questions");
        }
    }
}
