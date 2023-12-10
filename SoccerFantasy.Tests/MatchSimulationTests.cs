using System;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoccerFantasy.Models;
using Match = SoccerFantasy.Models.Match;

namespace SoccerFantasy.Tests
{

	public class MatchSimulationTests
	{
        int seed = 2;
        Random random;
        public MatchSimulationTests()
        {
            random = new Random(seed);
        }
        [Fact]
        private List<Player> GenerateTeamPlayers()
        { 
            int totalPlayersNum = int.MaxValue;
            int maxPlayersNum = 25;
            int goalKeepers = 0;
            int defenders = 0;
            int midfielders = 0;
            int forwards = 0;
            List<Player> players = new List<Player>();
            while (totalPlayersNum > maxPlayersNum)
            {
                goalKeepers = random.Next(2, 3);
                defenders = random.Next(4, 8);
                midfielders = random.Next(4,8);
                forwards = random.Next(3, 5);
                totalPlayersNum = goalKeepers + defenders + midfielders + forwards;
            }
            while (goalKeepers >= 1 || defenders >= 1 || midfielders >= 1 || forwards >= 1)
            {
                if (goalKeepers >= 1) { players.Add(new Player() { position = "GK" }); goalKeepers -= 1; }
                if (defenders >= 1) { players.Add(new Player() { position = "DF" }); defenders -= 1; }
                if (midfielders >= 1) { players.Add(new Player() { position = "MF" }); midfielders -= 1; }
                if (forwards >= 1) { players.Add(new Player() { position = "FW" }); forwards -= 1; }
            }
            Assert.Equal(totalPlayersNum, players.Count());
            return players;
        }

        [Fact]
        public List<Player> GenerateStarters()
        {
            List<Player> Allplayers = GenerateTeamPlayers();
            List<Player> starters = new List<Player>();
            int totalStarters = 11;
            int goalKeeper = 1;
            int defenders = 4;
            int midfielders = 3;
            int forwards = 3;
            while(goalKeeper >= 1 || defenders >= 1 || midfielders >= 1 || forwards >= 1)
            {
                if (goalKeeper >= 1) { starters.Add(new Player() { position = "GK" }); goalKeeper -= 1; }
                if (defenders >= 1) { starters.Add(new Player() { position = "DF" }); defenders -= 1; }
                if (midfielders >= 1) { starters.Add(new Player() { position = "MF" }); midfielders -= 1; }
                if (forwards >= 1) { starters.Add(new Player() { position = "FW" }); forwards -= 1; }
            }
            Assert.Equal(totalStarters, starters.Count());
            return starters;
        }

        [Fact]
        public List<Player> GenerateSubs()
        {
            List<Player> allPlayers = GenerateTeamPlayers();
            List<Player> starters = GenerateStarters();
            List<Player> subs = new List<Player>();
            int availableSubsNum = allPlayers.Count() - starters.Count();
            int subsCount = availableSubsNum - random.Next(1,2);
            int susbNum = subsCount;
            while (subsCount >= 1)
            {
                subs.Add(new Player());
                subsCount -= 1;
            }
            Assert.Equal(susbNum, subs.Count());
            return subs;
        }

        private IEnumerable<string> generateMatchesDates(int daysNum)
        {
            DateTime today = DateTime.Today;
            DateTime endDate = today.AddDays(daysNum);

            Console.WriteLine($"Date range from {today.ToShortDateString()} to {endDate.ToShortDateString()}:");

            for (DateTime currentDate = today; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                yield return currentDate.ToString("MMMM d");
            }
        }

        [Fact]
        public void GenerateMatches()
        {
            Team[] teams = new Team[]
            {
                new Team
                {
                    name = "Arsenal",
                    players = new List<Player>{}
                },
                new Team
                {
                    name = "Manchester United",
                    players = new List<Player>{}
                },
                new Team
                {
                    name = "Manchester City",
                    players = new List<Player>{}
                },
                new Team
                {
                    name = "Liverpool",
                    players = new List<Player>{}
                },
                new Team
                {
                    name = "Aston Villa",
                    players = new List<Player>{}
                },
                new Team
                {
                    name = "NewCastle United",
                    players = new List<Player>{}
                }
            };
            int matchesAmount = teams.Count() * (teams.Count() - 1);
            int homeMatchesAmount = teams.Count() - 1;
            int awayMatchesAmount = teams.Count() - 1;
            int gamesADayAmount = 1;
            int daysAmount = matchesAmount / (teams.Count()/2);  // will only work if num of teams are even
            List<Match> matches = new List<Match>();
            
            IEnumerable<string> dates = generateMatchesDates(daysAmount);
            foreach (Team team in teams)
            {
                if(team.name != "Arsenal")
                {
                    Console.WriteLine();
                }
                List<string> unAvailableHomeTeamDays = matches
                    .Where(m => m.awayTeamName == team.name)
                    .Select(m => m.date)
                    .Distinct()
                    .ToList();
                foreach(Team opponent in teams.Where(t=> t.name != team.name))
                {
                    List<string> unAvailableAwayTeamDays = matches
                    .Where(m => m.awayTeamName == opponent.name || m.homeTeamName == opponent.name)
                    .Select(m => m.date)
                    .Distinct()
                    .ToList();
                    List<string> availableDates = dates.Except(unAvailableHomeTeamDays).Except(unAvailableAwayTeamDays).ToList();
                    //List<string> availableDates = unavailableDays.Count() > 0
                    //    ? dates.Except(unavailableDays).ToList()
                    //    : dates.ToList();
                    string randomDate = availableDates.ElementAt(random.Next(0, availableDates.Count()));
                    Match match = new Match
                    {
                        date = randomDate,
                        homeTeamName = team.name,
                        homeTeam = team,
                        awayTeamName = opponent.name,
                        awayTeam = opponent,
                        awayStartingPlayers = GenerateStarters(),
                        awaySubPlayers = GenerateSubs(),
                        homeStartingPlayers = GenerateStarters(),
                        homeSubPlayers = GenerateSubs()
                    };
                    matches.Add(match);
                    unAvailableHomeTeamDays.Add(randomDate);
                }
            }
            Assert.Equal(matches.Count,matchesAmount);
            foreach (Team team in teams)
            {
                int homeGames = 0;
                int awayGames = 0;
                foreach (Match match in matches.Where(m=> m.homeTeamName == team.name || m.awayTeamName == team.name))
                {
                    if (match.homeTeamName == team.name){ homeGames += 1; }
                    else if(match.awayTeamName == team.name){ awayGames += 1; }
                }
                Assert.Equal(homeGames, homeMatchesAmount);
                Assert.Equal(awayGames, awayMatchesAmount);

                foreach(string currentDate in matches.Select(m=> m.date).Distinct())
                {
                    int gamesADay = 0;
                    foreach (Match match in matches.Where(m=> m.date == currentDate))
                    {
                        if(match.homeTeamName == team.name || match.awayTeamName == team.name)
                        {
                            gamesADay += 1;
                        }
                    }
                    Assert.Equal(gamesADayAmount, gamesADay);
                }
            }
            //Assert
            foreach (Match match in matches)
            {
                foreach (Match otherMatch in matches.Where(m => m != match))
                {
                    Assert.Equal(match, otherMatch,
                        Comparer.Get<Match>((match, otherMatch) => match != otherMatch));
                }
            }
        }
    }
}