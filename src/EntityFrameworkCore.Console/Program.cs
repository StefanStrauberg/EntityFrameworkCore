using System.Linq.Expressions;
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello to EntiryFramework Core");

// First we need an instance of context
using var context = new FootballLeageDbContext()
                    ?? throw new ArgumentNullException(nameof(FootballLeageDbContext));

Console.WriteLine(new string('-', 10) + "\tGet All Teams");
GetAllTeams(context);
Console.WriteLine(new string('-', 50));

Console.WriteLine(new string('-', 10) + "\tGet First Team");
await GetFirstTeamAsync(context);
Console.WriteLine(new string('-', 50));

Console.WriteLine(new string('-', 10) + "\tGet First Team By Epression team == 2");
await GetFirstTeamByExpressionAsync(context, team => team.TeamId == 2);
Console.WriteLine(new string('-', 50));


static void GetAllTeams(FootballLeageDbContext context)
{
    // SELECT [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    var teams = context.Teams.ToList();
    PrintAllTeams(teams);
}

static async Task GetFirstTeamAsync(FootballLeageDbContext context)
{
    // SELECT TOP(1) [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    var teamOne = await context.Teams.FirstAsync();
    PrintTeam(teamOne);
}

static async Task GetFirstTeamByExpressionAsync(FootballLeageDbContext context,
                                                Expression<Func<Team, bool>> predicate)
{
        // SELECT TOP(1) [t].[TeamId], [t].[CreatedDate], [t].[Name]
        // FROM [Teams] AS [t]
        // WHERE [t].[TeamId] = 2
        var teamTwo = await context.Teams.FirstAsync(predicate);
        PrintTeam(teamTwo);
}

static void PrintTeam(Team team)
{
    Console.WriteLine($"{team.TeamId} - " +
        $"{team.Name} - " +
        $"{team.CreatedDate}");
}

static void PrintAllTeams(List<Team> teams)
{
    foreach (Team team in teams)
        Console.WriteLine($"{team.TeamId} - " +
            $"{team.Name} - " +
            $"{team.CreatedDate}");
}