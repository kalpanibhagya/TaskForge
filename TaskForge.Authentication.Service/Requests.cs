using System.ComponentModel.DataAnnotations;

namespace TaskForge.Authentication.Service;

public class RegisterRequest 
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

public class LoginRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
