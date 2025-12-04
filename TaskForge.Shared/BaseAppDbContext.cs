using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace TaskForge.Shared;

public abstract class BaseAppDbContext : DbContext
{
    protected BaseAppDbContext(DbContextOptions options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e =>
                e.Entity is ITrackableEntity &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));
        foreach (var e in entries)
        {
            var entity = (ITrackableEntity)e.Entity;
            if (e.State == EntityState.Added)
                entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }

    //private void UpdateTimestamps()
    //{
    //    var entries = ChangeTracker.Entries()
    //        .Where(e =>
    //            e.Entity is TaskItem ||
    //            e.Entity is Subtask
    //        )
    //        .Where(e =>
    //            e.State == EntityState.Added ||
    //            e.State == EntityState.Modified
    //        );
    //    foreach (var e in entries)
    //    {
    //        if (e.State == EntityState.Added)
    //            e.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
    //        e.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
    //    }
    //}
}

public interface ITrackableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

