using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HighScore.Data.Migrations
{
    public partial class Leaderboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaderBoardId",
                table: "HighScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeaderBoards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderBoards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HighScores_LeaderBoardId",
                table: "HighScores",
                column: "LeaderBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighScores_LeaderBoards_LeaderBoardId",
                table: "HighScores",
                column: "LeaderBoardId",
                principalTable: "LeaderBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighScores_LeaderBoards_LeaderBoardId",
                table: "HighScores");

            migrationBuilder.DropTable(
                name: "LeaderBoards");

            migrationBuilder.DropIndex(
                name: "IX_HighScores_LeaderBoardId",
                table: "HighScores");

            migrationBuilder.DropColumn(
                name: "LeaderBoardId",
                table: "HighScores");
        }
    }
}
