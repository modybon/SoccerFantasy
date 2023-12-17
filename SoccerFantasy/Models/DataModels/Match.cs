using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
	public class Match
	{
        [Key,Column(TypeName = "uniqueidentifier")]
        public Guid matchId { get; set; }


        public string homeTeamName { get; set; }

        public string awayTeamName { get; set; }

        public virtual Team homeTeam { get; set; }

        public virtual Team awayTeam { get; set; }

        public virtual List<Player> homeStartingPlayers { get; set; }
        public virtual List<Player> homeSubPlayers { get; set; }

        public virtual List<Player> awayStartingPlayers { get; set; }
        public virtual List<Player> awaySubPlayers { get; set; }

        
        public string date { get; set; }

        public virtual List<Goal> homeGoals { get; set; }

        public virtual List<Goal> awayGoals { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte homePossesion { get; set; } = 0;

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte awayPossesion { get; set; } = 0;

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte homeTotalShots { get; set; } = 0;

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte awayTotalShots { get; set; } = 0;

        public bool matchPlayed { get; set; } = false;

        public int minute { get; set; } = 0;
    }
}

