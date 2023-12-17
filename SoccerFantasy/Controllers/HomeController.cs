using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;
using SoccerFantasy.Models.ViewModels;

namespace SoccerFantasy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private DataContext dataContext;
    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        Console.WriteLine("This is Home Controller");
        _logger = logger;
        dataContext = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult TeamsPage()
    {
        List<Team> teams = dataContext.teams.ToList();
        return View(teams);
    }

    [HttpGet]
    public IActionResult PlayerPage(Guid playerId)
    {
        Player playerQuery = dataContext.players
            .Include(p=> p.goals)
            .Include(p => p.teamRef)
            .First(p => p.playerId == playerId);

        Console.WriteLine($"Player is: {playerQuery.name}");
        return View(playerQuery);
    }

    [HttpGet]
    public IActionResult Players()
    {
        var players = dataContext.players.ToList();
        PlayersViewModel viewModel = new PlayersViewModel()
        {
            players = players
        };
        return View(viewModel);
    }

    [HttpPost]
    public ViewResult Players(PlayersViewModel playersViewModel)
    {
        Console.WriteLine($"name searched for: {playersViewModel.playerName}");
        Console.WriteLine($"team searched for: {playersViewModel.teamName}");
        List<Player> playersQuery;
        PlayersViewModel viewModel;
        if (playersViewModel.teamName == "All Clubs")
        {
            if(playersViewModel.playerName != null) {
                playersQuery = dataContext.players.Where(player =>
                player.name.ToLower().Contains(playersViewModel.playerName.ToLower())
                ).ToList();
            }
            else
            {
                playersQuery = dataContext.players.ToList();
            }
            
            viewModel = new PlayersViewModel()
            {
                players = playersQuery,
                playerName = playersViewModel.playerName,
                teamName = playersViewModel.teamName
            };
            return View(viewModel);
        }
        else
        {
            if(playersViewModel.playerName != null)
            {
                playersQuery = dataContext.players.Where(player =>
                player.name.ToLower().Contains(playersViewModel.playerName.ToLower())
                && player.teamName == playersViewModel.teamName
                ).ToList();
            }
            else
            {
                playersQuery = dataContext.players.Where(player => player.teamName == playersViewModel.teamName).ToList();
            }
            
            viewModel = new PlayersViewModel()
            {
                    players = playersQuery,
                    playerName = playersViewModel.playerName
                    ,
                    teamName = playersViewModel.teamName
            };
            return View("Players", viewModel);
        }
    }

    [HttpGet]
    public IActionResult Fixtures()
    {
        var count = (dataContext.teams.Count() / 2) - 1;
        var matches = dataContext.matches
            .Include(m => m.homeTeam)
            .Include(m => m.awayTeam)
            .Include(m => m.homeGoals)
            .Include(m => m.awayGoals)
            .Where(m => m.matchPlayed == false)
            .OrderBy(m => m.date)
            .Take(count)
            .ToList();
        return View(matches);
    }
    [HttpGet]
    public IActionResult SimulateMatches()
    {
        SimMatch simMatch = new SimMatch(dataContext);
        var count = (dataContext.teams.Count() / 2) - 1;
        var matches = dataContext.matches
            .Include(m => m.homeTeam)
            .ThenInclude(m=> m.players)
            .Include(m => m.awayTeam)
            .ThenInclude(m=> m.players)
            .Include(m => m.homeGoals)
            .Include(m => m.awayGoals)
            .Where(m => m.matchPlayed == false)
            .OrderBy(m => m.date)
            .Take(count)
            .ToList();
        simMatch.SimMatches(matches);
        var matches2 = dataContext.matches
    .Include(m => m.homeTeam)
    .Include(m => m.awayTeam)
    .Include(m => m.homeGoals)
    .Include(m => m.awayGoals)
    .Where(m => m.matchPlayed == false)
    .OrderBy(m => m.date)
    .Take(count)
    .ToList();
        return Json(new { success = true, message = "Simulation successful" });
    }
    [HttpGet]
    public IActionResult Table()
    {
        var teams = dataContext.teams
            .OrderByDescending(t => t.points)
            .ToList();
        return View(teams);
    }
    [HttpGet]
    public IActionResult Stats()
    {
        var topScorers = dataContext.players
            .Include(p=> p.goals)
            .Include(p=> p.teamRef)
            .OrderByDescending(p => p.goals.Count())
            .Take(20)
            .ToList();
        List<Team> teams = dataContext.teams
            .Include(t=> t.players)
            .OrderByDescending(t => t.goalDiff)
            .ToList();
        StatsViewModel viewModel = new StatsViewModel()
        {
            topScorers = topScorers,
            teamsGoals = teams
        };
        return View(viewModel);
    }
}

