using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using SoccerFantasy.Models;

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
}

