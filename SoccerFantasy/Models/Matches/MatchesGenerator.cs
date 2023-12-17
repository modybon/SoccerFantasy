using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;

namespace SoccerFantasy.Models
{
	// Change the name of this class
	// this class generates matches doesn't simulate them
	public class MatchesGenerator
	{
		DataContext dataContext;
        Random random;
		public MatchesGenerator(DataContext dc)
		{
			dataContext = dc;
            random = new Random(2);
        }

        //public List<Player> GenerateStarters(List<Player> allPlayers)
        //{
        //    List<Player> starters = new List<Player>();
        //    int availableGoalKeepers = allPlayers.Where(p => p.position.Contains("GK")).ToList().Count;
        //    int availableDefenders = allPlayers.Where(p => p.position.Contains("DF")).ToList().Count;
        //    int availableMidfielders = allPlayers.Where(p => p.position.Contains("MF")).ToList().Count;
        //    int availableForwards = allPlayers.Where(p => p.position.Contains("FW")).ToList().Count;
        //    int goalKeeper = 1;
        //    int defenders = availableDefenders>=4 ? 4: availableDefenders;
        //    int midfielders = availableMidfielders >= 3 ? 3 : availableMidfielders;
        //    int forwards = availableForwards >= 3 ? 3 : availableForwards;
        //    int playersTotal = goalKeeper + defenders + midfielders + forwards;
        //    while(playersTotal != 11)
        //    {
        //        if(availableDefenders > defenders && playersTotal!= 11){defenders += 1;}
        //        if (availableMidfielders > midfielders && playersTotal != 11) { midfielders += 1; }
        //        if (availableForwards > forwards && playersTotal != 11) { forwards += 1; }
        //    }
        //    while (goalKeeper >= 1 || defenders >= 1 || midfielders >= 1 || forwards >= 1)
        //    {
        //        if (goalKeeper >= 1) { starters.Add(allPlayers.First(p=> p.position.Contains("GK"))); goalKeeper -= 1; }
        //        if (defenders >= 1) {
        //            Player player = allPlayers.First(p => p.position.Contains("DF"));
        //            starters.Add(player);
        //            allPlayers.Remove(player);
        //            defenders -= 1;
        //        }
        //        if (midfielders >= 1) {
        //            Player player = allPlayers.First(p=> p.position.Contains("MF"));
        //            starters.Add(player);
        //            allPlayers.Remove(player);
        //            midfielders -= 1;
        //        }
        //        if (forwards >= 1) {
        //            Player player = allPlayers.First(p => p.position.Contains("FW"));
        //            starters.Add(player);
        //            allPlayers.Remove(player);
        //            forwards -= 1;
        //        }
        //    }
        //    return starters;
        //}
        //public List<Player> GenerateSubs(List<Player> allPlayers,List<Player> starters)
        //{
        //    int availableSubsNum = allPlayers.Count() - starters.Count();
        //    int subsCount = availableSubsNum - random.Next(1, 2);
        //    List<Player> availableSubs = allPlayers.Except(starters).Take(subsCount).ToList();
        //    return availableSubs;
        //}

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

        public List<Match> GenerateMatches()
        {
            var teams = dataContext.teams.Include(t=> t.players);
            int matchesAmount = teams.Count() * (teams.Count() - 1);
            int homeMatchesAmount = teams.Count() - 1;
            int awayMatchesAmount = teams.Count() - 1;
            int gamesADayAmount = 1;
            int daysAmount = matchesAmount / (teams.Count() / 2);  // will only work if num of teams are even
            List<Match> matches = new List<Match>();
            IEnumerable<string> dates = generateMatchesDates(daysAmount);
            foreach (Team homeTeam in teams)
            {
                //List<Player> homeStarters = GenerateStarters(homeTeam.players.ToList());
                //List<Player> homeSubs = GenerateSubs(homeTeam.players.ToList(), homeStarters);
                List<string> unAvailableDays = matches
                    .Where(m => m.homeTeamName == homeTeam.name || m.awayTeamName == homeTeam.name)
                    .Select(m => m.date)
                    .Distinct()
                    .ToList();

                foreach (Team awayTeam in teams.Where(t => t.name != homeTeam.name))
                {
                    //List<Player> awayStarters = GenerateStarters(awayTeam.players.ToList());
                    //List<Player> awaySubs = GenerateSubs(awayTeam.players.ToList(), awayStarters);
                    List<string> unAvailableOpponentDays = matches
                        .Where(m => m.homeTeamName == awayTeam.name || m.awayTeamName == awayTeam.name)
                        .Select(m => m.date)
                        .Distinct()
                        .ToList();

                    List<string> unavailableDays = unAvailableDays.Union(unAvailableOpponentDays).ToList();
                    List<string> availableDates = dates.Except(unavailableDays).ToList();

                    if (availableDates.Any())
                    {
                        string randomDate = availableDates.ElementAt(random.Next(0, availableDates.Count()));

                        Match match = new Match
                        {
                            date = randomDate,
                            homeTeamName = homeTeam.name,
                            homeTeam = homeTeam,
                            awayTeamName = awayTeam.name,
                            awayTeam = awayTeam
                        };

                        matches.Add(match);
                        unAvailableDays.Add(randomDate);
                    }
                    
                }
            }
            return matches;
        }

    }
}

