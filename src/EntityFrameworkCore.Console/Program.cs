// First we need an instance of context
using var context = new FootballLeageDbContext();

GetAllTeams(context);
await GetFirstTeamAsync(context);
await GetFirstTeamByExpressionAsync(context, team => team.TeamId == 2);

// In this situation we'll get exception - Sequence contains no elements.
// Because in the table stored no one elements
// await GetFirstCoachAsync(context);

// In this situation we'll get default value
await GetFirstOrDefaultCoachAsync(context);
await GetFirstOrDefaultTeamByExpressionAsync(context, team => team.TeamId == 2);

// In this situation we'll get exception - Sequence contains more than one element.
// Because in the table stored many elements
//await GetSingleTeamAsync(context);

// In this situation we'll get a record that meets a condition
// Because in the table stored only one record that meets a condition
await GetSingleTeamByExpressionAsync(context, team => team.TeamId == 2);
await GetSingleOrDefaultTeamByExpressionAsync(context, team => team.TeamId == 2);

// static async Task GetFirstCoachAsync(FootballLeageDbContext context)
// {
//     Console.WriteLine(new string('-', 10) + 
//         "\tSelect a single coache record");
//     // SELECT TOP(1) [c].[Id], [c].[CreatedDate], [c].[Name]
//     // FROM [Coaches] AS [c]
//     var coach = await context.Coaches.FirstAsync();
//     PrintOneCoache(coach);
//     Console.WriteLine(new string('-', 50));
// }

// static async Task GetSingleTeamAsync(FootballLeageDbContext context)
// {
//     Console.WriteLine(new string('-', 10) + 
//         "\tSelect a single team record");
//     // SELECT TOP(2) [t].[TeamId], [t].[CreatedDate], [t].[Name]
//     // FROM [Teams] AS [t]
//     var team = await context.Teams.SingleAsync();
//     PrintOneTeam(team);
//     Console.WriteLine(new string('-', 50));
// }

static async Task GetSingleTeamByExpressionAsync(FootballLeageDbContext context,
                                                 Expression<Func<Team, bool>> predicate)
{
    Console.WriteLine(new string('-', 10) + 
        "\tSelect a single record that meets a condition");
    // SELECT TOP(2) [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    // WHERE [t].[TeamId] = 2
    var team = await context.Teams.SingleAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static async Task GetSingleOrDefaultTeamByExpressionAsync(FootballLeageDbContext context,
                                                          Expression<Func<Team, bool>> predicate)
{
    Console.WriteLine(new string('-', 10) + 
        "\tSelect a single or default record that meets a condition");
    // SELECT TOP(2) [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    // WHERE [t].[TeamId] = 2
    var team = await context.Teams.SingleOrDefaultAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstOrDefaultCoachAsync(FootballLeageDbContext context)
{
    Console.WriteLine(new string('-', 10) + 
        "\tSelect a first or default coache record");
    // SELECT TOP(1) [c].[Id], [c].[CreatedDate], [c].[Name]
    // FROM [Coaches] AS [c]
    var coach = await context.Coaches.FirstOrDefaultAsync();
    PrintOneCoache(coach);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstOrDefaultTeamByExpressionAsync(FootballLeageDbContext context,
                                                         Expression<Func<Team, bool>> predicate)
{
    Console.WriteLine(new string('-', 10) + 
        "\tSelect a first record or default that meets a condition");
    // SELECT TOP(1) [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    // WHERE [t].[TeamId] = 2
    var team = await context.Teams.FirstOrDefaultAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}


static void GetAllTeams(FootballLeageDbContext context)
{
    Console.WriteLine(new string('-', 10) +
        "\tSelect all team records");
    // SELECT [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    var teams = context.Teams.ToList();
    PrintAllTeams(teams);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstTeamAsync(FootballLeageDbContext context)
{
    Console.WriteLine(new string('-', 10) + 
        "\tSelect a first team record");
    // SELECT TOP(1) [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    var team = await context.Teams.FirstAsync();
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstTeamByExpressionAsync(FootballLeageDbContext context,
                                                Expression<Func<Team, bool>> predicate)
{
    Console.WriteLine(new string('-', 10) + 
        "\tSelect a first team record that meets a condition");
    // SELECT TOP(1) [t].[TeamId], [t].[CreatedDate], [t].[Name]
    // FROM [Teams] AS [t]
    // WHERE [t].[TeamId] = 2
    var team = await context.Teams.FirstAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static void PrintOneTeam(Team? team)
{
    if (team is null)
    {
        Console.WriteLine("Team is null");
        return;
    }
    Console.WriteLine($"{team.TeamId} - " +
        $"{team.Name} - " +
        $"{team.CreatedDate}");
}

static void PrintOneCoache(Coach? coach)
{
    if (coach is null)
    {
        Console.WriteLine("Coach is null");
        return;
    }
    Console.WriteLine($"{coach.Id} - " +
        $"{coach.Name} - " +
        $"{coach.CreatedDate}");
}

static void PrintAllTeams(List<Team> teams)
{
    foreach (Team team in teams)
        Console.WriteLine($"{team.TeamId} - " +
            $"{team.Name} - " +
            $"{team.CreatedDate}");
}