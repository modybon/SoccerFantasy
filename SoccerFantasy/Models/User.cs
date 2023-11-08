using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoccerFantasy.Models
{
    public class User
    {
        [Key,Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int totalPoints { get; set; }
        public int currentRoundPoints { get; set; }
    }
}
