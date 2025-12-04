using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskForge.Shared;

namespace TaskForge.Tasks.Database;

public class TasksDbContext : BaseAppDbContext
{
    public DbSet<Project> projects { get; set; }
    public DbSet<Board> boards { get; set; }
    public DbSet<TaskItem> tasks { get; set; }
    public DbSet<Subtask> subtasks { get; set; }

    public TasksDbContext(DbContextOptions<TasksDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
           entity.HasKey(p => p.ProjectId);
        });
        modelBuilder.Entity<Board>(entity => 
        {
            entity.HasKey(b => b.BoardId);
        });
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
}

public class TasksDbContextFactory : IDesignTimeDbContextFactory<TasksDbContext>
{
    public TasksDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TasksDbContext>();
        builder.UseNpgsql("Host=localhost;Port=5432;Database=dev-postgres;Username=postgres;Password=pasward");
        return new TasksDbContext(builder.Options);
    }
}