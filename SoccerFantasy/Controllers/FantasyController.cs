using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;
using SoccerFantasy.Models.ViewModels;

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
            List<int> points = dataContext.fantasyTeams.Select(ft => ft.current_round_points).ToList();
            FantasyIndexViewModel viewModel = new FantasyIndexViewModel();
            if(points.Count() != 0)
            {
                int? averageRoundPoints = (int?)points.Average() ?? 0;
                int? highest = (int?)points.Max() ?? 0;
                viewModel.averageRoundPoints = averageRoundPoints;
                viewModel.highestRoundPoints = highest;
                if(CurrentUser.Instance != null)
                {
                    viewModel.currentUserFantasyTeam = dataContext.fantasyTeams.Where(ft => ft.fantasyTeamId == CurrentUser.Instance.fantasyTeam.fantasyTeamId).FirstOrDefault();
                }
            }
            return View("Index", viewModel);
		}
		public IActionResult PickTeam()
		{

            CurrentUser.Instance.fantasyTeam = dataContext.fantasyTeams.Include(ft=> ft.players).Where(ft => ft.userId == CurrentUser.Instance.user_id).FirstOrDefault();
            return View("PickTeam");
        }

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

        [HttpPost]
        public IActionResult SignIn(string userName,string password)
        {
			User? user = dataContext.users
                .Include(u=> u.fantasyTeam)
                .ThenInclude(ft=> ft.players)
                .FirstOrDefault(u => u.username == userName && u.password == password);
			if(user != null)
			{
				CurrentUser.Instance = user;
                return RedirectToAction("Index");
            }
			else
			{
				Console.WriteLine("Wrong crednetials");
                return RedirectToAction("SignIn");
            }
        }

        [HttpGet]
        public IActionResult Register()
		{
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fantasyTeamName, string userName, string password)
        {
			if(!dataContext.users.Any(u=> u.username == userName))
			{
                User newUser = new User() { username = userName, password = password};
                dataContext.users.Add(newUser);
                dataContext.SaveChanges();
                FantasyTeam fantasyTeam = new FantasyTeam() { user = newUser, name = fantasyTeamName , players = new List<Player>()};
                dataContext.fantasyTeams.Add(fantasyTeam);
                dataContext.SaveChanges();
                User user = dataContext.users
                    .Include(u => u.fantasyTeam)
                    .ThenInclude(ft => ft.players)
                    .First(u => u.user_id == newUser.user_id);
                CurrentUser.Instance = user;
                dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
			else
			{
                Console.WriteLine("UserName already in use");
                return RedirectToAction("Register");
            }
			
        }
    }
}