using System;
using Microsoft.EntityFrameworkCore;

namespace SoccerFantasy.Models
{
	public class SimMatch
	{
        Random random;
        DataContext dataContext;

        public SimMatch(DataContext context)
        {
            random = new Random();
            dataContext = context;
        }
        public List<Player> GenerateStarters(List<Player> allPlayers)
        {
            List<Player> starters = new List<Player>();
            int availableGoalKeepers = allPlayers.Where(p => p.position.Contains("GK")).ToList().Count;
            int availableDefenders = allPlayers.Where(p => p.position.Contains("DF")).ToList().Count;
            int availableMidfielders = allPlayers.Where(p => p.position.Contains("MF")).ToList().Count;
            int availableForwards = allPlayers.Where(p => p.position.Contains("FW")).ToList().Count;
            int goalKeeper = 1;
            int defenders = availableDefenders >= 4 ? 4 : availableDefenders;
            int midfielders = availableMidfielders >= 3 ? 3 : availableMidfielders;
            int forwards = availableForwards >= 3 ? 3 : availableForwards;
            int playersTotal = goalKeeper + defenders + midfielders + forwards;
            while (playersTotal != 11)
            {
                if (availableDefenders > defenders && playersTotal != 11) { defenders += 1; }
                if (availableMidfielders > midfielders && playersTotal != 11) { midfielders += 1; }
                if (availableForwards > forwards && playersTotal != 11) { forwards += 1; }
            }
            while (goalKeeper >= 1 || defenders >= 1 || midfielders >= 1 || forwards >= 1)
            {
                if (goalKeeper >= 1) {
                    Player player = allPlayers.First(p => p.position.Contains("GK"));
                    player.appearances += 1;
                    starters.Add(player);
                    goalKeeper -= 1;
                }
                if (defenders >= 1)
                {
                    Player player = allPlayers.First(p => p.position.Contains("DF"));
                    player.appearances += 1;
                    starters.Add(player);
                    allPlayers.Remove(player);
                    defenders -= 1;
                    
                }
                if (midfielders >= 1)
                {
                    Player player = allPlayers.First(p => p.position.Contains("MF"));
                    player.appearances += 1;
                    starters.Add(player);
                    allPlayers.Remove(player);
                    midfielders -= 1;
                }
                if (forwards >= 1)
                {
                    Player player = allPlayers.First(p => p.position.Contains("FW"));
                    player.appearances += 1;
                    starters.Add(player);
                    allPlayers.Remove(player);
                    forwards -= 1;
                }
            }
            return starters;
        }
        public List<Player> GenerateSubs(List<Player> allPlayers, List<Player> starters)
        {
            int availableSubsNum = allPlayers.Count() - starters.Count();
            int subsCount = availableSubsNum - random.Next(1, 2);
            List<Player> availableSubs = allPlayers.Except(starters).Take(subsCount).ToList();
            return availableSubs;
        }
        public void SimMatches(List<Match> matches)
        {
            using(var transaction = dataContext.Database.BeginTransaction())
            {
                try
                {
                    dataContext.players.ToList().ForEach(p=> p.fantasy_round_points = 0);
                    dataContext.SaveChanges();
                    dataContext.fantasyTeams.ToList().ForEach(ft => ft.current_round_points = 0);
                    dataContext.SaveChanges();
                    foreach (Match match in matches)
                    {
                        match.homeStartingPlayers = GenerateStarters(match.homeTeam.players.ToList());
                        match.awayStartingPlayers = GenerateStarters(match.awayTeam.players.ToList());
                        var homeGoals = random.Next(0, 5); ;
                        var awayGoals = random.Next(0, 5); ;
                        while (homeGoals != 0 || awayGoals != 0)
                        {
                            if (homeGoals != 0)
                            {
                                Goal goal = generateGoal(match.matchId, match.homeStartingPlayers);
                                goal.goalScorer.goals.Add(goal);
                                goal.goalScorer.appearances += 1;
                                goal.goalScorer.fantasy_round_points += 3;
                                dataContext.fantasyTeams
                                    .Include(ft => ft.players)
                                    .Where(ft => ft.players.Contains(goal.goalScorer))
                                    .ToList()
                                    .ForEach(ft=> {
                                        ft.totalPoints += goal.goalScorer.fantasy_round_points;
                                        ft.current_round_points += goal.goalScorer.fantasy_round_points;
                                        });
                                dataContext.SaveChanges();
                                //goal.match = match;
                                match.homeGoals.Add(goal);
                                //dataContext.SaveChanges();
                                homeGoals -= 1;
                            }
                            if (awayGoals != 0)
                            {
                                Goal goal = generateGoal(match.matchId, match.awayStartingPlayers);
                                goal.goalScorer.goals.Add(goal);
                                goal.goalScorer.appearances += 1;
                                goal.goalScorer.fantasy_round_points += 3;
                                dataContext.fantasyTeams
                                    .Include(ft => ft.players)
                                    .Where(ft => ft.players.Contains(goal.goalScorer))
                                    .ToList()
                                    .ForEach(ft => {
                                        ft.totalPoints += goal.goalScorer.fantasy_round_points;
                                        ft.current_round_points += goal.goalScorer.fantasy_round_points;
                                    });
                                dataContext.SaveChanges();
                                //goal.match = match;
                                match.awayGoals.Add(goal);
                                //dataContext.SaveChanges();
                                awayGoals -= 1;
                            }
                        }
                        if (match.homeGoals.Count() > match.awayGoals.Count())
                        {
                            if(match.homeGoals.Count() == 0)
                            {
                                foreach(Player player in match.homeStartingPlayers.Where(p => p.position == "GK" || p.position == "DF"))
                                {
                                    player.fantasy_round_points += 6;
                                    dataContext.SaveChanges();
                                }
                            }
                            match.homeTeam.points += 3;
                            match.homeTeam.gamesWon += 1;
                            match.awayTeam.gamesLost += 1;
                            match.homeTeam.goalDiff = match.homeTeam.goalDiff + (match.homeGoals.Count() - match.awayGoals.Count());
                            dataContext.SaveChanges();
                        }
                        else if (match.awayGoals.Count() > match.homeGoals.Count())
                        {
                            if (match.awayGoals.Count() == 0)
                            {
                                foreach (Player player in match.awayStartingPlayers.Where(p => p.position == "GK" || p.position == "DF"))
                                {
                                    player.fantasy_round_points += 6;
                                    dataContext.SaveChanges();
                                }
                            }
                            match.awayTeam.points += 3;
                            match.awayTeam.gamesWon += 1;
                            match.homeTeam.gamesLost += 1;
                            match.awayTeam.goalDiff = match.awayTeam.goalDiff + (match.awayGoals.Count() - match.homeGoals.Count());
                            dataContext.SaveChanges();
                        }
                        else
                        {
                            match.homeTeam.points += 1;
                            match.awayTeam.points += 1;
                            match.homeTeam.gamesDraw += 1;
                            match.awayTeam.gamesDraw += 1;
                            dataContext.SaveChanges();
                        }
                        match.homeTeam.gamesPlayed += 1;
                        match.awayTeam.gamesPlayed += 1;
                        match.matchPlayed = true;

                        dataContext.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch(Exception error)
                {
                    Console.WriteLine($"SimMatches: {error.Message}");
                    transaction.Rollback();
                }

            }
        }

        public Goal generateGoal(Guid matchId, List<Player> players)
        {
            var potentialGoalScorer = players.Where(p => p.position == "MF" || p.position == "FW").ToList();
            var rand = random.Next(0, potentialGoalScorer.Count());
            Player goalScorer = potentialGoalScorer[rand];

            // Ensure goals list is initialized if it's null
            if (goalScorer.goals == null)
            {
                goalScorer.goals = new List<Goal>();
            }

            Goal goal = new Goal()
            {
                minute = (byte)random.Next(0, 90),
                goalScorer = goalScorer,
                goalScorerId = goalScorer.playerId,
                matchId = matchId
            };

            return goal;
        }

    }


}

