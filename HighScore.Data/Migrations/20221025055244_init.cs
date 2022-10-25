using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HighScore.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighScores_Users_userId",
                table: "HighScores");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HighScores",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HighScores_userId",
                table: "HighScores",
                newName: "IX_HighScores_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighScores_Users_UserId",
                table: "HighScores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighScores_Users_UserId",
                table: "HighScores");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HighScores",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HighScores_UserId",
                table: "HighScores",
                newName: "IX_HighScores_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighScores_Users_userId",
                table: "HighScores",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
