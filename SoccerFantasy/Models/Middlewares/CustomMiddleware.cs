using System;
using SoccerFantasy.Models.ViewModels;

namespace SoccerFantasy.Models
{
	public class CustomMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, DataContext dataContext)
		{
			var teams = dataContext.teams.Select(team => new {name = team.name, clubLogo = team.clubLogo}).ToList();
		
			var viewModel = new SharedLayoutViewModel() { teamsNames = teams.Select(team=> team.name).ToList(),
				teamsLogos = teams.Select(team=> team.clubLogo).ToList()};

            context.Items["SharedViewModel"] = viewModel;
			await _next(context);
		}

	}
}

