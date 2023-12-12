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
            int averageRoundPoints = (int)points.Average();
            int highest = points.Max();
            FantasyIndexViewModel viewModel = new FantasyIndexViewModel()
            {
                averageRoundPoints = averageRoundPoints,
                highestRoundPoints = highest
            };
            return View("Index",viewModel);
		}
		public IActionResult PickTeam()
		{
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
                FantasyTeam fantasyTeam = new FantasyTeam() { user = newUser, name = fantasyTeamName};
                dataContext.fantasyTeams.Add(fantasyTeam);
                dataContext.SaveChanges();
                CurrentUser.Instance = newUser;
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

//@foreach(var player in Model)
//                        {
//                            < tr class= "card-content" >
//                                < td style = "text-align:left" >
//                                    < div class= "row align-items-center" >
//                                        < div class= "col-2" >
//                                            @if(player.playerImageURL != "null")
//                                            {
//                                                < img src = "@player.playerImageURL" height = "40" width = "40" />
//                                            }
//                                            else
//{
//                                                < img src = "https://resources.premierleague.com/premierleague/photos/players/40x40/Photo-Missing.png" height = "40" width = "40" />
//                                            }
//                                        </ div >
//                                        < div class= "col" >
//                                            @*@Html.ActionLink($"{player.name}", "Home/PlayerPage", new { playerId = player.playerId }, null) *@
//                                            < a href = "https://localhost:7274/Home/PlayerPage?playerId=@player.playerId" > @player.name </ a >
//                                        </ div >
//                                    </ div >
//                                </ td >
//                                < td style = "text-align:center" > @player.position </ td >
//                                < td class= "row" style = "text-align:center" >
//                                    @*< img class= "col" src = "@player.nationURL" height = "118" width = "118" /> *@
//                                    < span class= "@player.nationCSS mr-7" ></ span >
//                                    < p class= "col" > @player.nationality </ p >
//                                </ td >
//                            </ tr >
//                        }

//         List<Player> players = dataContext.players.ToList();
//         FantasyTeam fantasyTeam = new FantasyTeam();
//PickTeamViewModel viewModel = new PickTeamViewModel()
//{
//	players = players,
//	fantasyTeam = fantasyTeam
//};