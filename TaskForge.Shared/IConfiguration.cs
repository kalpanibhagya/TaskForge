namespace TaskForge.Shared;
public interface IPostgresConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Configuration : IPostgresConfiguration
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "taskforge";
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "password";

    public string ToConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
    }
}