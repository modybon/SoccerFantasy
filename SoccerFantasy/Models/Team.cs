using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
	public class Team
	{
        [Key]
        public string name { get; set; }
        public string clubLogo { get; set; }
        public virtual ICollection<Player> players { get; set; }
        public int gamesPlayed { get; set; } = 0;
        public int gamesWon { get; set; } = 0;
        public int gamesDraw { get; set; } = 0;
        public int gamesLost { get; set; } = 0;
        public int goalDiff { get; set; } = 0;
        public int points { get; set; } = 0;

        override public string ToString()
        {
            string playersString = "";
            foreach (var player in players)
            {
                playersString += player.ToString();
            }
            return $"Team: {this.name}\n " +
    $"Club Logo: {this.clubLogo}\n, players: [{playersString}]";
        }
    }
}

