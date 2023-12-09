using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSchema4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_teams_teamRefname",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "team",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "teamRefname",
                table: "Players",
                newName: "teamName");

            migrationBuilder.RenameIndex(
                name: "IX_Players_teamRefname",
                table: "Players",
                newName: "IX_Players_teamName");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_teams_teamName",
                table: "Players",
                column: "teamName",
                principalTable: "teams",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_teams_teamName",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "teamName",
                table: "Players",
                newName: "teamRefname");

            migrationBuilder.RenameIndex(
                name: "IX_Players_teamName",
                table: "Players",
                newName: "IX_Players_teamRefname");

            migrationBuilder.AddColumn<string>(
                name: "team",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_teams_teamRefname",
                table: "Players",
                column: "teamRefname",
                principalTable: "teams",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
