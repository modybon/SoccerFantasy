using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SoccerFantasy.Models
{
	public class Goal
	{
        [Key]
        public Guid goalId { get; set; }

        public Guid matchId { get; set; }

        public Guid goalScorerId { get; set; }

        public Guid goalAssisterId { get; set; }

        public virtual Match match { get; set; }

        public virtual Player goalScorer { get; set; }

        public virtual Player goalAssister { get; set; }

        public byte minute { get; set; }
	}
}

