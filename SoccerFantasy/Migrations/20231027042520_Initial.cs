using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fantasyLeagues",
                columns: table => new
                {
                    fantasyLeagueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    fantasyLeagueCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fantasyLeagues", x => x.fantasyLeagueId);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    clubLogo = table.Column<string>(type: "TEXT", nullable: false),
                    gamesPlayed = table.Column<int>(type: "INTEGER", nullable: false),
                    gamesWon = table.Column<int>(type: "INTEGER", nullable: false),
                    gamesDraw = table.Column<int>(type: "INTEGER", nullable: false),
                    gamesLost = table.Column<int>(type: "INTEGER", nullable: false),
                    goalDiff = table.Column<int>(type: "INTEGER", nullable: false),
                    points = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    totalPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    currentRoundPoints = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    matchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    homeTeamNameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    awayTeamNameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    homeTeamNamename = table.Column<string>(type: "TEXT", nullable: false),
                    awayTeamNamename = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    homeScore = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false),
                    awayScore = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false),
                    homePossesion = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false),
                    awayPossesion = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false),
                    homeTotalShots = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false),
                    awayTotalShots = table.Column<byte>(type: "TINYINT UNSIGNED", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.matchId);
                    table.ForeignKey(
                        name: "FK_matches_teams_awayTeamNamename",
                        column: x => x.awayTeamNamename,
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_teams_homeTeamNamename",
                        column: x => x.homeTeamNamename,
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    position = table.Column<string>(type: "TEXT", nullable: false),
                    teamName = table.Column<string>(type: "TEXT", nullable: false),
                    team = table.Column<string>(type: "TEXT", nullable: false),
                    age = table.Column<string>(type: "TEXT", nullable: false),
                    fantasy_round_points = table.Column<int>(type: "INTEGER", nullable: false),
                    goals = table.Column<int>(type: "INTEGER", nullable: false),
                    nationality = table.Column<string>(type: "TEXT", nullable: false),
                    nationURL = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.playerId);
                    table.ForeignKey(
                        name: "FK_Players_teams_teamName",
                        column: x => x.teamName,
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeams",
                columns: table => new
                {
                    fantasyTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    totalPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    current_round_points = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeams", x => x.fantasyTeamId);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goals",
                columns: table => new
                {
                    goalId = table.Column<Guid>(type: "TEXT", nullable: false),
                    matchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    goalScorerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    goalAssisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    minute = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goals", x => x.goalId);
                    table.ForeignKey(
                        name: "FK_goals_Players_goalAssisterId",
                        column: x => x.goalAssisterId,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goals_Players_goalScorerId",
                        column: x => x.goalScorerId,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goals_matches_matchId",
                        column: x => x.matchId,
                        principalTable: "matches",
                        principalColumn: "matchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fantasyTeamLeagues",
                columns: table => new
                {
                    FantasyTeamLeagueId = table.Column<Guid>(type: "TEXT", nullable: false),
                    fantasyTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fantasyLeagueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fantasyTeamLeagues", x => x.FantasyTeamLeagueId);
                    table.ForeignKey(
                        name: "FK_fantasyTeamLeagues_FantasyTeams_fantasyTeamId",
                        column: x => x.fantasyTeamId,
                        principalTable: "FantasyTeams",
                        principalColumn: "fantasyTeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fantasyTeamLeagues_fantasyLeagues_fantasyLeagueId",
                        column: x => x.fantasyLeagueId,
                        principalTable: "fantasyLeagues",
                        principalColumn: "fantasyLeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fantasyTeamLeagues_fantasyLeagueId",
                table: "fantasyTeamLeagues",
                column: "fantasyLeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_fantasyTeamLeagues_fantasyTeamId",
                table: "fantasyTeamLeagues",
                column: "fantasyTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_userId",
                table: "FantasyTeams",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_goalAssisterId",
                table: "goals",
                column: "goalAssisterId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_goalScorerId",
                table: "goals",
                column: "goalScorerId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_matchId",
                table: "goals",
                column: "matchId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_awayTeamNamename",
                table: "matches",
                column: "awayTeamNamename");

            migrationBuilder.CreateIndex(
                name: "IX_matches_homeTeamNamename",
                table: "matches",
                column: "homeTeamNamename");

            migrationBuilder.CreateIndex(
                name: "IX_Players_teamName",
                table: "Players",
                column: "teamName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fantasyTeamLeagues");

            migrationBuilder.DropTable(
                name: "goals");

            migrationBuilder.DropTable(
                name: "FantasyTeams");

            migrationBuilder.DropTable(
                name: "fantasyLeagues");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
