using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;

namespace SoccerFantasy.Controllers
{
	public class PlayersController : Controller
	{
        private DataContext dataContext;
        public PlayersController(DataContext dc)
		{
            dataContext = dc;

        }

        public ActionResult getPlayersByPosition(string position)
        {
            var c = CurrentUser.Instance.fantasyTeam.players;
            List<Player> players;
            if(CurrentUser.Instance.fantasyTeam.players != null)
            {
                players = dataContext.players
                    .Where(p => p.position.Contains(position))
                    .AsEnumerable()
                    .Except(CurrentUser.Instance.fantasyTeam.players)
                    .ToList();
            }
            else {
                players = dataContext.players
                    .Where(p => p.position.Contains(position))
                    .AsEnumerable()
                    .ToList();
            }
            return Json(players);
        }

        public void addPlayerToFantasyTeam(string fantasyTeamId,string playerId)
        {
            Guid pId = Guid.Parse(playerId);
            Guid fTeamId = Guid.Parse(fantasyTeamId);
            Player player = dataContext.players.First(p => p.playerId == pId);
            FantasyTeam fantasyTeam = dataContext.fantasyTeams
                .Include(ft=> ft.players)
                .First(ft=> ft.fantasyTeamId == fTeamId);
            fantasyTeam.players.Add(player);
            CurrentUser.Instance.fantasyTeam = fantasyTeam;
            dataContext.SaveChanges();
        }
        public void replacePlayerInFantasyTeam(string currentPlayerId,string fantasyTeamId, string newPlayerId)
        {
            using (var transaction = dataContext.Database.BeginTransaction())
            {
                try
                {
                    Guid cpId = Guid.Parse(currentPlayerId);
                    Guid npId = Guid.Parse(newPlayerId);
                    Guid fTeamId = Guid.Parse(fantasyTeamId);
                    Player currentPlayer = dataContext.players.First(p => p.playerId == cpId);
                    Player newPlayer = dataContext.players.First(p => p.playerId == npId);
                    var c = CurrentUser.Instance.fantasyTeam.players;
                    FantasyTeam fantasyTeam = dataContext.fantasyTeams
                        .Include(ft => ft.players)
                        .First(ft => ft.fantasyTeamId == fTeamId);
                    fantasyTeam.players.Remove(currentPlayer);
                    fantasyTeam.players.Add(newPlayer);
                    CurrentUser.Instance.fantasyTeam.players = fantasyTeam.players;
                    dataContext.SaveChanges();
                    transaction.Commit();
                }catch(Exception error)
                {
                    Console.WriteLine(error.Message);
                    transaction.Rollback();
                }

            }
        }
    }
}

