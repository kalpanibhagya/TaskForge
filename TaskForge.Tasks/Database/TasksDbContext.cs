using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskForge.Tasks.Database;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> tasks { get; set; }
    public DbSet<Subtask> subtasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.TaskId);
        });
        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.HasKey(s => s.TaskId);
        });
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e =>
                e.Entity is TaskItem ||
                e.Entity is Subtask
            )
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified
            );

        foreach (var e in entries)
        {
            if (e.State == EntityState.Added)
                e.Property("CreatedAt").CurrentValue = DateTime.UtcNow;

            e.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }
    }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseNpgsql("Host=localhost;Port=5432;Database=dev-postgres;Username=postgres;Password=pasward");
        return new AppDbContext(builder.Options);
    }
}