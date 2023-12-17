using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
	public class FantasyTeamLeague
	{
		[Key]
		public Guid FantasyTeamLeagueId { get; set; }
		public Guid fantasyTeamId { get; set; }
		public Guid fantasyLeagueId { get; set; }
		public virtual FantasyTeam FantasyTeam { get; set; }
		public virtual FantasyLeague FantasyLeague { get; set; }
	}
}

