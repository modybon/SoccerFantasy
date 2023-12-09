using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSchema6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FantasyTeams_userId",
                table: "FantasyTeams");

            migrationBuilder.DropColumn(
                name: "currentRoundPoints",
                table: "users");

            migrationBuilder.DropColumn(
                name: "totalPoints",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_userId",
                table: "FantasyTeams",
                column: "userId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FantasyTeams_userId",
                table: "FantasyTeams");

            migrationBuilder.AddColumn<int>(
                name: "currentRoundPoints",
                table: "users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalPoints",
                table: "users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_userId",
                table: "FantasyTeams",
                column: "userId");
        }
    }
}
