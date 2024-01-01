using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data;

public class FootballLeageDbContext : DbContext
{
    public DbSet<Team> Teams { get; set; }
    public DbSet<Coach> Coaches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost; " + 
            "Initial Catalog=FootballLeage_EfCore; " +
            "User ID=sa; " +
            "Password=!QAZxsw2; " +
            "Encrypt=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasData(
            new Team
            {
                TeamId = 1,
                Name = "Tivoli Gardens F.C.",
                CreatedDate = DateTimeOffset.UtcNow.DateTime
            },
            new Team
            {
                TeamId = 2,
                Name = "Waterhouse F.C.",
                CreatedDate = DateTimeOffset.UtcNow.DateTime
            },
            new Team
            {
                TeamId = 3,
                Name = "Humble Lions F.C.",
                CreatedDate = DateTimeOffset.UtcNow.DateTime
            }
        );
    }
}
