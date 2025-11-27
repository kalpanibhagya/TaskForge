using Microsoft.EntityFrameworkCore;
using TaskForge.Tasks.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=dev-postgres;Username=postgres;Password=pasward"));
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskForge APIs",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseExceptionHandler("/error"); // Optional
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); //http://127.0.0.1:5000/swagger/index.html
    options.RoutePrefix = "swagger";
});
app.MapControllers();
app.Run();
