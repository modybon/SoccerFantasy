using System;
using Microsoft.AspNetCore.Mvc;

namespace SoccerFantasy.Models.ViewComponents
{
	public class TeamPageHeaderViewComponent : ViewComponent
	{
		public TeamPageHeaderViewComponent()
		{
		}

		public IViewComponentResult Invoke(Team team)
		{
			return View(team);
		}
	}
}

