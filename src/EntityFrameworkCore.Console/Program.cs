// First we need an instance of context

using var context = new FootballLeageDbContext();

GetAllTeams(context);
await GetFirstTeamAsync(context);
await GetFirstTeamByExpressionAsync(context,
                                    team => team.TeamId == 2);

// In this situation we'll get exception - Sequence contains no elements.
// Because in the table stored no one elements
// await GetFirstCoachAsync(context);

// In this situation we'll get default value
await GetFirstOrDefaultCoachAsync(context);
await GetFirstOrDefaultTeamByExpressionAsync(context,
                                             team => team.TeamId == 2);

// In this situation we'll get exception - Sequence contains more than one element.
// Because in the table stored many elements
//await GetSingleTeamAsync(context);

// In this situation we'll get a record that meets a condition
// Because in the table stored only one record that meets a condition
await GetSingleTeamByExpressionAsync(context,
                                     team => team.TeamId == 2);
await GetSingleOrDefaultTeamByExpressionAsync(context,
                                              team => team.TeamId == 2);
await GetAllTeamsByExpressionAsync(context,
                                   team => team.Name == "Tivoli Gardens F.C.");
await GetAllTeamsByQuerySyntaxAsync(from team in context.Teams
                                    where team.Name == "Tivoli Gardens F.C."
                                    select team);
await GetAllTeamsByExpressionAsync(context,
                                   team => team.Name!
                                               .Contains("F.C."));                                 
await GetAllTeamsByExpressionAsync(context,
                                   team => EF.Functions
                                             .Like(team.Name, "%F.C.%"));
await GetAllTeamsByQuerySyntaxAsync(from team in context.Teams
                                    where EF.Functions
                                            .Like(team.Name, "%F.C.%")
                                    select team);

// --Agregate functions
// Count
await GetCountOfAllTeamsAsync(context);
await GetCountOfAllTeamsByExpressionAsync(context,
                                          team => EF.Functions
                                                    .Like(team.Name, "%F.C.%"));
// Max
// Average
// Sum

// static async Task GetFirstCoachAsync(FootballLeageDbContext context)
// {
//     Console.WriteLine(new string('-', 10) + 
//         "\tSelect a single coache record");
//     var coach = await context.Coaches.FirstAsync();
//     PrintOneCoache(coach);
//     Console.WriteLine(new string('-', 50));
// }

// static async Task GetSingleTeamAsync(FootballLeageDbContext context)
// {
//     Console.WriteLine(new string('-', 10) + 
//         "\tSelect a single team record");
//     var team = await context.Teams.SingleAsync();
//     PrintOneTeam(team);
//     Console.WriteLine(new string('-', 50));
// }

static async Task GetCountOfAllTeamsAsync(FootballLeageDbContext context)
{
    var count = await context.Teams.CountAsync();
    Console.WriteLine($"Number of Teams: {count}");
    Console.WriteLine(new string('-', 50));
}

static async Task GetCountOfAllTeamsByExpressionAsync(FootballLeageDbContext context,
                                                      Expression<Func<Team, bool>> predicate)
{
    var count = await context.Teams.CountAsync(predicate);
    Console.WriteLine($"Number of Teams with condition: {count}");
    Console.WriteLine(new string('-', 50));
}

static async Task GetAllTeamsByQuerySyntaxAsync(IQueryable<Team> query)
{
    var teams = await query.ToListAsync();
    PrintAllTeams(teams);
    Console.WriteLine(new string('-', 50));
}

static async Task GetAllTeamsByExpressionAsync(FootballLeageDbContext context,
                                               Expression<Func<Team, bool>> predicate)
{
    var teams = await context.Teams
                             .Where(predicate)
                             .ToListAsync();
    PrintAllTeams(teams);
    Console.WriteLine(new string('-', 50));
}

static async Task GetSingleTeamByExpressionAsync(FootballLeageDbContext context,
                                                 Expression<Func<Team, bool>> predicate)
{
    var team = await context.Teams
                            .SingleAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static async Task GetSingleOrDefaultTeamByExpressionAsync(FootballLeageDbContext context,
                                                          Expression<Func<Team, bool>> predicate)
{
    var team = await context.Teams
                            .SingleOrDefaultAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstOrDefaultCoachAsync(FootballLeageDbContext context)
{
    var coach = await context.Coaches
                             .FirstOrDefaultAsync();
    PrintOneCoache(coach);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstOrDefaultTeamByExpressionAsync(FootballLeageDbContext context,
                                                         Expression<Func<Team, bool>> predicate)
{
    var team = await context.Teams
                            .FirstOrDefaultAsync(predicate);
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}


static void GetAllTeams(FootballLeageDbContext context)
{
    var teams = context.Teams
                       .ToList();
    PrintAllTeams(teams);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstTeamAsync(FootballLeageDbContext context)
{
    var team = await context.Teams
                            .FirstAsync();
    PrintOneTeam(team);
    Console.WriteLine(new string('-', 50));
}

static async Task GetFirstTeamByExpressionAsync(FootballLeageDbContext context,
                                                Expression<Func<Team, bool>> predicate)
{
    var team = await context.Teams
                            .FirstAsync(predicate);
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