using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
    [Table("FantasyTeams")]
	public class FantasyTeam
	{
        [Key,Column(TypeName = "uniqueidentifier")]
        public Guid fantasyTeamId { get; set; }

        public Guid userId { get; set; }

        public virtual User user { get; set; }

        public virtual ICollection<FantasyTeamLeague> FantasyTeamLeagues { get; set; }

        public virtual ICollection<Player> players { get; set; }

        public string name { get; set; }

        public int totalPoints { get; set; }

        public int current_round_points { get; set; }
    }
}

