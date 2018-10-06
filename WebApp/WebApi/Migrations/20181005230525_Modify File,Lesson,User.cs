using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ModifyFileLessonUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lessons",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Lessons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CreatorId",
                table: "Lessons",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Users_CreatorId",
                table: "Lessons",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Users_CreatorId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CreatorId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Files");
        }
    }
}
