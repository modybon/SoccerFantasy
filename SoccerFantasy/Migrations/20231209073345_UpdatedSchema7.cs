using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSchema7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "fantasyTeamId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_fantasyTeamId",
                table: "Players",
                column: "fantasyTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_FantasyTeams_fantasyTeamId",
                table: "Players",
                column: "fantasyTeamId",
                principalTable: "FantasyTeams",
                principalColumn: "fantasyTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_FantasyTeams_fantasyTeamId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_fantasyTeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "fantasyTeamId",
                table: "Players");
        }
    }
}
