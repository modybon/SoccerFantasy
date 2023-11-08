using System;
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
			var clubLogos = dataContext.teams.Select(team => team.clubLogo).ToList();
			var players = dataContext.players.ToList();

			var viewModel = new PlayersViewModel() { clubsLogos = clubLogos, players = players };

			context.Items["PlayersViewModel"] = viewModel;
			await _next(context);
		}

	}
}

