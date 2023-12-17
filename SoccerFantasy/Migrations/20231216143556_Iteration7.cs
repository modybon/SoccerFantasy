using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class Iteration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_Players_goalAssisterId",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_goalAssisterId",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "goals",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "awayScore",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "homeScore",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "goalAssisterId",
                table: "goals");

            migrationBuilder.AlterColumn<Guid>(
                name: "goalId",
                table: "goals",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "matchId1",
                table: "goals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_goals_matchId1",
                table: "goals",
                column: "matchId1");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_matches_matchId1",
                table: "goals",
                column: "matchId1",
                principalTable: "matches",
                principalColumn: "matchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_matches_matchId1",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_matchId1",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "matchId1",
                table: "goals");

            migrationBuilder.AddColumn<int>(
                name: "goals",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "awayScore",
                table: "matches",
                type: "TINYINT UNSIGNED",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "homeScore",
                table: "matches",
                type: "TINYINT UNSIGNED",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<Guid>(
                name: "goalId",
                table: "goals",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "goalAssisterId",
                table: "goals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_goals_goalAssisterId",
                table: "goals",
                column: "goalAssisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_Players_goalAssisterId",
                table: "goals",
                column: "goalAssisterId",
                principalTable: "Players",
                principalColumn: "playerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
