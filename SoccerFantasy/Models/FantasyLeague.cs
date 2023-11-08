using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
	public class FantasyLeague
	{
		[Key,Column(TypeName = "uniqueidentifier")]
        public Guid fantasyLeagueId { get; set;}

        public string name { get; set; }
        
        public virtual ICollection<FantasyTeamLeague> FantasyTeamLeagues { get; set; }

        public string fantasyLeagueCode { get; set; }
    }
}

