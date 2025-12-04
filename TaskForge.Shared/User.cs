namespace TaskForge.Shared;

public class User
{
    public Guid UserId { get; protected set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public PermissionLevel Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<Guid> Projects { get; set; } = new(); // membership in projects
    public List<Guid> Boards { get; set; } = new(); // membership in boards
    public User(string username, string email, string passwordHash)
    {
        UserId = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }
}

public enum PermissionLevel
{
    Admin,
    User,
    Guest
}