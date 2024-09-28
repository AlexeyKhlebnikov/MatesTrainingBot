using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class TrainingDbContext : DbContext
{
    public const string DefaultSchema = "training";

    public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainingDbContext).Assembly);
    }
}