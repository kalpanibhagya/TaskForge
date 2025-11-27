using Microsoft.EntityFrameworkCore;
using TaskForge.Tasks.Database;

namespace TaskForge.Tasks.Service;

class Program
{
    private const string ServerErrorTitle = "Internal server error (500)";
    static int Main(string[] args)
    {
        var tasksAssembly = typeof(TasksController).Assembly;
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                //services.AddEndpointsApiExplorer(); // for Swagger
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5432;Database=dev-postgres;Username=postgres;Password=pasward"));
                services.AddScoped<ITasksService, TasksService>();
                services
                .AddControllers() // Register controllers at the Host level
                .AddApplicationPart(tasksAssembly)
                .AddControllersAsServices();
                // register Swagger generator
                services.AddSwaggerGen(options =>
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "TaskForge APIs",
                        Version = "v1"
                    }));
            })
            .ConfigureWebHostDefaults(webHost =>
            {
                webHost.Configure(app =>
                {
                    app.UseExceptionHandler(exceptionHandlerApp =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                        exceptionHandlerApp.Run(async context =>
                        {
                            context.Response.ContentType = MediaTypeNames.Text.Plain;
                            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                            var exception = exceptionHandlerPathFeature?.Error;
                            if (exception is BadHttpRequestException)
                            {
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                if (!string.IsNullOrWhiteSpace(exception.Message)) await context.Response.WriteAsync(exception.Message);
                            }
                            else
                            {
                                var route = $"{context.Request.PathBase}{context.Request.Path}";
                                var message = $"{ServerErrorTitle} \n" +
                                            $"Request: {route} \n" +
                                            $"Stack trace: {exception}";
                                Trace.TraceError(message);
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                if (env.IsDevelopment() || Debugger.IsAttached)
                                {
                                    await context.Response.WriteAsync(message);
                                }
                                else
                                {
                                    await context.Response.WriteAsync($"{ServerErrorTitle}");
                                }
                            }
                        });
                    });

                    app.UseForwardedHeaders();
                    app.UseRouting();
                    //app.UseAuthentication();
                    //app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                    //app.UseMvc();
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); //http://127.0.0.1:5000/swagger/index.html
                        options.RoutePrefix = "swagger";
                    });

                });
            })
            .Build()
            .Run();
        return 0;
    }
}
