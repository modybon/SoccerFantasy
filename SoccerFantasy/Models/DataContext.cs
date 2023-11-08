using System;
using Microsoft.EntityFrameworkCore;

namespace SoccerFantasy.Models
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions opts) : base(opts)
		{

		}

		public DbSet<Goal> goals => Set<Goal>();
        public DbSet<FantasyLeague> fantasyLeagues => Set<FantasyLeague>();
        public DbSet<Match> matches => Set<Match>();
        public DbSet<FantasyTeam> fantasyTeams => Set<FantasyTeam>();
        public DbSet<FantasyTeamLeague> fantasyTeamLeagues => Set<FantasyTeamLeague>();
        public DbSet<Player> players => Set<Player>();
        public DbSet<Team> teams => Set<Team>();
        public DbSet<User> users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

}

