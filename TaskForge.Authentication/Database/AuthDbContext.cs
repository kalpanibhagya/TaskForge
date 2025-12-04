namespace TaskForge.Authentication.Database;

public class AuthDbContext : BaseAppDbContext
{
    public DbSet<User> users { get; set; }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
        });
        base.OnModelCreating(modelBuilder);
    }
}

public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
{
    public AuthDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AuthDbContext>();
        builder.UseNpgsql("Host=localhost;Port=5432;Database=dev-postgres;Username=postgres;Password=pasward");
        return new AuthDbContext(builder.Options);
    }
}