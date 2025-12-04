namespace TaskForge.Authentication.Services;

public interface IAuthService
{
    Task<AuthenticatedResponse> LoginAsync(string username, string password);
    Task RegisterAsync(string username, string email, string password);
    Task<AuthenticatedResponse> RefreshTokenAsync(TokenApiModel tokenApiModel);
    bool Revoke(string username);
}

public class AuthenticationService : IAuthService
{
    private readonly AuthDbContext _db;
    private readonly ITokenService _tokenService;
    private readonly int RefreshTokenExpiryDays = 7;

    public AuthenticationService(AuthDbContext db, ITokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(string username, string email, string password)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User(username, email, hashed);
        await _db.users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<AuthenticatedResponse> LoginAsync(string username, string password)
    {
        var user = await _db.users.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password.");
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenExpiryDays);
        await _db.SaveChangesAsync();
        return new AuthenticatedResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthenticatedResponse> RefreshTokenAsync(TokenApiModel tokenApiModel)
    {
        var accessToken = tokenApiModel.AccessToken;
        var refreshToken = tokenApiModel.RefreshToken;
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var username = principal.Identity.Name; //this is mapped to the Name claim by default
        var user = _db.users.SingleOrDefault(u => u.Username == username);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new HttpRequestException("Invalid client request");
        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _db.SaveChangesAsync();
        return new AuthenticatedResponse()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }


    public bool Revoke(string username)
    {
        var user = _db.users.SingleOrDefault(u => u.Username == username);
        if (user == null) return false;
        user.RefreshToken = null;
        _db.SaveChanges();
        return true;
    }
}

public class AuthenticatedResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}


public class TokenApiModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}