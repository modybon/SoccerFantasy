using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSchema5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_awayTeamNamename",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_homeTeamNamename",
                table: "matches");

            migrationBuilder.DropIndex(
                name: "IX_matches_awayTeamNamename",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "awayTeamNameId",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "awayTeamNamename",
                table: "matches");

            migrationBuilder.RenameColumn(
                name: "homeTeamNamename",
                table: "matches",
                newName: "homeTeamName");

            migrationBuilder.RenameColumn(
                name: "homeTeamNameId",
                table: "matches",
                newName: "awayTeamName");

            migrationBuilder.RenameIndex(
                name: "IX_matches_homeTeamNamename",
                table: "matches",
                newName: "IX_matches_homeTeamName");

            migrationBuilder.AddColumn<Guid>(
                name: "matchId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "matchId1",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "matchId2",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "matchId3",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "date",
                table: "matches",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<bool>(
                name: "matchPlayed",
                table: "matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Players_matchId",
                table: "Players",
                column: "matchId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_matchId1",
                table: "Players",
                column: "matchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Players_matchId2",
                table: "Players",
                column: "matchId2");

            migrationBuilder.CreateIndex(
                name: "IX_Players_matchId3",
                table: "Players",
                column: "matchId3");

            migrationBuilder.CreateIndex(
                name: "IX_matches_awayTeamName",
                table: "matches",
                column: "awayTeamName");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_awayTeamName",
                table: "matches",
                column: "awayTeamName",
                principalTable: "teams",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_homeTeamName",
                table: "matches",
                column: "homeTeamName",
                principalTable: "teams",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_matches_matchId",
                table: "Players",
                column: "matchId",
                principalTable: "matches",
                principalColumn: "matchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_matches_matchId1",
                table: "Players",
                column: "matchId1",
                principalTable: "matches",
                principalColumn: "matchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_matches_matchId2",
                table: "Players",
                column: "matchId2",
                principalTable: "matches",
                principalColumn: "matchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_matches_matchId3",
                table: "Players",
                column: "matchId3",
                principalTable: "matches",
                principalColumn: "matchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_awayTeamName",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_homeTeamName",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_matches_matchId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_matches_matchId1",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_matches_matchId2",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_matches_matchId3",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_matchId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_matchId1",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_matchId2",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_matchId3",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_matches_awayTeamName",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "matchId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "matchId1",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "matchId2",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "matchId3",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "matchPlayed",
                table: "matches");

            migrationBuilder.RenameColumn(
                name: "homeTeamName",
                table: "matches",
                newName: "homeTeamNamename");

            migrationBuilder.RenameColumn(
                name: "awayTeamName",
                table: "matches",
                newName: "homeTeamNameId");

            migrationBuilder.RenameIndex(
                name: "IX_matches_homeTeamName",
                table: "matches",
                newName: "IX_matches_homeTeamNamename");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "matches",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "awayTeamNameId",
                table: "matches",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "awayTeamNamename",
                table: "matches",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_matches_awayTeamNamename",
                table: "matches",
                column: "awayTeamNamename");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_awayTeamNamename",
                table: "matches",
                column: "awayTeamNamename",
                principalTable: "teams",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_homeTeamNamename",
                table: "matches",
                column: "homeTeamNamename",
                principalTable: "teams",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
