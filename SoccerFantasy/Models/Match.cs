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


        public Guid homeTeamNameId { get; set; }

        public Guid awayTeamNameId { get; set; }

        public virtual Team homeTeamName { get; set; }

        public virtual Team awayTeamName { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte homeScore { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte awayScore { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte homePossesion { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte awayPossesion { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte homeTotalShots { get; set; }

        [Column(TypeName = "TINYINT UNSIGNED")]
        public byte awayTotalShots { get; set; }
    }
}

