using Newtonsoft.Json;
using Questao2.Objetos;
using System;
using System.Net;

public class Program
{
    private const string URL_DADOS_BASE = @"https://jsonmock.hackerrank.com/api/football_matches";

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        Result result = GetCompetitionData(team, year);

        int total = result.Data.Sum(q => q.Team1Goals);

        return total;
    }

    public static Result GetCompetitionData(string team, int year)
    {
        Result result = new Result();

        var request = (HttpWebRequest)WebRequest.Create(string.Concat(URL_DADOS_BASE, $"?year={year}&team1={team}"));
        request.Method = "GET";

        using (var response = request.GetResponse())
        {
            var streamDados = response.GetResponseStream();
            StreamReader reader = new StreamReader(streamDados);
            object objResponse = reader.ReadToEnd();
            result = JsonConvert.DeserializeObject<Result>(objResponse.ToString());
            streamDados.Close();
            response.Close();
        }

        return result;
    }
}