namespace TaskForge.Authentication.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(string username, string email, string password);
    Task<User> GetUser(Guid userId);
    Task<User> UpdateUserAsync(Guid userId, string? email = null, string? password = null);
    Task DeleteUserAsync(Guid userId);
}

public class UserService : IUserService
{
    private readonly AuthDbContext _db;
    public UserService(AuthDbContext db)
    {
        _db = db;
    }

    public async Task<User> CreateUserAsync(string username, string email, string password)
    {
        var user = new User(username, email, password);
        _db.users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _db.users.FindAsync(userId);
        if (user != null)
        {
            _db.users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<User> GetUser(Guid userId)
    {
        var user = await _db.users.FindAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }

    public async Task<User> UpdateUserAsync(Guid userId, string? email, string? password)
    {
        var user = await _db.users.FindAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        if (email != null)
        {
            user.Email = email;
        }
        if (password != null)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hashed;
        }
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return user;
    }
}