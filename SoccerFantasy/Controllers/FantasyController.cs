﻿using System;
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
		private PlayersController playersController;
		public FantasyController(DataContext dc)
		{
			dataContext = dc;
            playersController = new PlayersController(dataContext);
        }

		public IActionResult Index()
		{
			//CurrentUser.Instance = new User()
			//{
			//	username = "fahmy",
			//	//fantasyTeam = new FantasyTeam()
			//	//{
			//	//	players = dataContext.teams.Include(t => t.players).First(t => t.name == "Arsenal").players
			//	//}
			//}; // Remove when u are done with this page
			

            return View();
		}
		public IActionResult PickTeam()
		{
            List<Player> players = dataContext.players.ToList();
            FantasyTeam fantasyTeam = new FantasyTeam();
			PickTeamViewModel viewModel = new PickTeamViewModel()
			{
				players = players,
				fantasyTeam = fantasyTeam
			};
            return View("PickTeam",viewModel);
        }

		public void addPlayer(string id)
		{
			playersController.addPlayerToFantasyTeam(id);
			PickTeam();
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