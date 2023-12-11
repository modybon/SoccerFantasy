using System;
using Microsoft.AspNetCore.Mvc;
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
            var players = dataContext.players.Where(p => p.position.Contains(position)).ToList();
            return Json(players);
        }

        public void addPlayerToFantasyTeam(string id)
        {
            Guid playerId = Guid.Parse(id);
            Player player = dataContext.players.First(p => p.playerId == playerId);
        }
    }
}

