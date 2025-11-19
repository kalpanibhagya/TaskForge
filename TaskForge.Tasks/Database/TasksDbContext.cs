using Microsoft.EntityFrameworkCore;
using TaskForge.Tasks;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Subtask> SubTasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>().HasKey(t => t.TaskId);
        base.OnModelCreating(modelBuilder);
    }

    // Update UpdatedAt on save
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
            .Where(e => e.Entity is TaskItem || e.Entity is Subtask)
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var e in entries)
        {
            if (e.State == EntityState.Added)
                ((dynamic)e.Entity).CreatedAt = DateTime.UtcNow;

            ((dynamic)e.Entity).UpdatedAt = DateTime.UtcNow;
        }
    }
}
