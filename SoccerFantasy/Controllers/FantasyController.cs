using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;

namespace SoccerFantasy.Controllers
{
	public class FantasyController : Controller
	{
		private DataContext dataContext;
		public FantasyController(DataContext dc)
		{
			dataContext = dc;
		}

		public IActionResult Index()
		{
			CurrentUser.Instance = new User()
			{
				username = "fahmy",
				//fantasyTeam = new FantasyTeam()
				//{
				//	players = dataContext.teams.Include(t => t.players).First(t => t.name == "Arsenal").players
				//}
			}; // Remove when u are done with this page

			return View();
		}
		public IActionResult PickTeam()
		{
			return View();
		}
	}
}

