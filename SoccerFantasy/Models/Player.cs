using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
    //player class
    [Table("Players")]
    public class Player : IEquatable<Player>
    {
        [Key,Column(TypeName = "uniqueidentifier")]
        public Guid playerId { get; set; }

        public string name { get; set; }

        public string playerImageURL { get; set;}

        public string position  { get; set; }

        public string teamName { get; set; }

        public virtual Team teamRef { get; set; }
        
        public string age { get; set; }

        public int fantasy_round_points { get; set; } = 0;

        public int goals { get; set; } = 0;

        public int assists { get; set; } = 0;

        public int appearances { get; set; } = 0;

        public int cleanSheets { get; set; } = 0;

        //public int playerNumber { get; set; } = 0;

        public string nationality { get; set; }

        public string nationURL { get; set; }

        public string nationCSS { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Player otherPlayer = (Player)obj;
            return playerId.Equals(otherPlayer.playerId);
        }

        public bool Equals(Player? other)
        {
            if(other == null)
            {
                return false;
            }
            return playerId == other.playerId;
        }

        public override int GetHashCode()
        {
            return playerId.GetHashCode();
        }
    }    
}

