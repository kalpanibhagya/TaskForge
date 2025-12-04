using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using TaskForge.Authentication.Database;
using TaskForge.Authentication.Services;
using TaskForge.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(new Configuration()
    {
        Host = builder.Configuration["PostgresConnectionString:Host"],
        Port = int.Parse(builder.Configuration["PostgresConnectionString:Port"]),
        Database = builder.Configuration["PostgresConnectionString:Database"],
        Username = builder.Configuration["PostgresConnectionString:Username"],
        Password = builder.Configuration["PostgresConnectionString:Password"]
    }.ToConnectionString()));

builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskForge APIs",
        Version = "v1"
    });
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); //http://127.0.0.1:5000/swagger/index.html
    options.RoutePrefix = "swagger";
});
app.MapControllers();

app.Run();
