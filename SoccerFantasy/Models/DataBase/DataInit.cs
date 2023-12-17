using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoccerFantasy.Models;

namespace SoccerFantasy.Models
{
    /// <summary>
    /// Initialization of teams and players DB
    /// </summary>
    public class DataInit
    {
        ICollection<Player> players = new List<Player>();
        ICollection<Team> teams = new List<Team>();
        MatchesGenerator matchGenerator;
        Dictionary<string, string> nationalityMapping = new Dictionary<string, string>
        {
            { "ENG", "fi-gb-eng" },
            { "POR", "fi-pt" },
            { "WAL", "fi-gb-wls" },
            { "KOR", "fi-kr" },
            { "JAM", "fi-jm" },
            { "SWE", "fi-se" },
            { "SUI", "fi-ch" },
            { "KVX", "fi-xk" },
            { "URU", "fi-uy" },
            { "ZIM", "fi-zw" },
            { "TUR", "fi-tr" },
            { "TUN", "fi-tn" },
            { "SCO", "fi-gb-sct" },
            { "IRL", "fi-ie" },
            { "NED", "fi-nl" },
            { "CRO", "fi-hr" },
            { "NIR", "fi-gb-nir" },
            { "PAR", "fi-py" },
            { "POL", "fi-pl" },
            { "UKR", "fi-ua" },
            { "GER", "fi-de" }
        };

        public DataInit(DataContext context)
        {
            matchGenerator = new MatchesGenerator(context);
            string fileName = "teamsData.json";
            string fileContent = File.ReadAllText(fileName);
            teams = JsonConvert.DeserializeObject<List<Team>>(fileContent) ?? new List<Team>();
            context.Database.Migrate();

            if (context.users.Count() == 0 &&
                context.teams.Count() == 0 &&
                context.matches.Count() == 0 &&
                context.goals.Count() == 0 &&
                context.players.Count() == 0 &&
                context.fantasyLeagues.Count() == 0 &&
                context.fantasyTeams.Count() == 0 &&
                context.fantasyTeamLeagues.Count() == 0)
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var team in teams)
                        {
                            context.teams.Add(team);

                            foreach (Player player in team.players)
                            {
                                if (nationalityMapping.ContainsKey(player.nationality))
                                {
                                    player.nationCSS = $"fi {nationalityMapping[player.nationality]}";
                                }
                                else
                                {
                                    player.nationCSS = $"fi fi-{player.nationality.ToLower().Substring(0, 2)}";
                                }
                                player.position = player.position.Substring(0, 2);
                                players.Add(player);
                            }
                        }
                        context.SaveChanges();
                        var matches = matchGenerator.GenerateMatches();
                        context.matches.AddRange(matches);
                        context.SaveChanges();
                        transaction.Commit();
                    }catch(Exception error)
                    {
                        Console.WriteLine($"Initialization Error: {error.Message}");
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
