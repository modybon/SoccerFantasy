using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SoccerFantasy.Models
{
	public class Goal
	{
        [Key, Column(TypeName = "uniqueidentifier")]
        public Guid goalId { get; set; }

        public Guid matchId { get; set; }

        public Guid goalScorerId { get; set; }

        public virtual Match match { get; set; }

        public virtual Player goalScorer { get; set; }

        public byte minute { get; set; }
	}
}

