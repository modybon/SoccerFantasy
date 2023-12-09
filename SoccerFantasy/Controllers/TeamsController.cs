using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;
using SoccerFantasy.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerFantasy.Controllers
{
    public class TeamsController : Controller
    {
        DataContext dataContext;
        public TeamsController(DataContext data)
        {
            dataContext = data;
        }

        [HttpGet]
        public IActionResult OverView(string teamName)
        {
            Team team = dataContext.teams.First(team => team.name == teamName);
            return View(team);
        }

        [HttpGet]
        public IActionResult Squad(string teamName)
        {
            Team team = dataContext.teams
                .Include(team => team.players)
                .First(team => team.name == teamName);
            return View(team);
        }

        [HttpGet]
        public IActionResult Fixtures(string teamName)
        {
            Team team = dataContext.teams.First(team => team.name == teamName);
            List<Match> matches = dataContext.matches
                .Include(p=> p.homeTeam)
                .Include(p=> p.awayTeam)
                .Where(match =>
            match.homeTeamName == teamName || match.awayTeamName == teamName).ToList();
            FixturesViewModel fixturesViewModel = new FixturesViewModel()
            {
                team = team,
                matches = matches
            };
            Console.WriteLine($"Matches Count: {matches.Count}");
            return View(fixturesViewModel);
        }

        [HttpGet]
        public IActionResult Results(string teamName)
        {
            Team team = dataContext.teams.First(team => team.name == teamName);
            return View(team);
        }

        [HttpGet]
        public IActionResult Stats(string teamName)
        {
            Team team = dataContext.teams.First(team => team.name == teamName);
            return View(team);
        }
    }
}

