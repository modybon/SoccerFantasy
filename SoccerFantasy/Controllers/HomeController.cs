using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoccerFantasy.Models;

namespace SoccerFantasy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private DataContext dataContext;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Players()
    {
        return View();
    }
}

