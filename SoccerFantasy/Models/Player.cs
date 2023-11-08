using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
    //player class
    [Table("Players")]
    public class Player
    {
        [Key,Column(TypeName = "uniqueidentifier")]
        public Guid playerId { get; set; }

        public string name { get; set; }

        public string position  { get; set; }

        public string teamName { get; set; }

        public string team { get; set; }

        public virtual Team Club { get; set; }
        
        public string age { get; set; }
        public int fantasy_round_points { get; set; } = 0;
        public int goals { get; set; } = 0;
        public string nationality { get; set; }
        public string nationURL { get; set; }
        public string nationCSS { get; set; }
        
        override public string ToString()
        {
            return $"name: {this.name}\n " +
                $"club: {this.team}\n, " +
                $"age: {this.age}\n " +
                $"nationality:{this.nationality}\n";
        }

    }

    
}

