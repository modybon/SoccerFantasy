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
            
            var players = dataContext.players
                .Where(p => p.position.Contains(position))
                .AsEnumerable()
                .Except(CurrentUser.Instance.fantasyTeam.players)
                .ToList();
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
            if (!fantasyTeam.players.Contains(player))
            {

            }
            fantasyTeam.players.Add(player);
            CurrentUser.Instance.fantasyTeam = fantasyTeam;
            dataContext.SaveChanges();
        }
    }
}

