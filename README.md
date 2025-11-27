# TaskForge

TaskForge is a small task management solution composed of a Blazor UI and a backend Tasks service. This repository contains a Blazor client (`TaskForge.UI`) and an ASP.NET Core Web API service (`TaskForge.Tasks.Service`) that stores tasks and subtasks using Entity Framework Core with PostgreSQL.

- `TaskForge.UI` - Blazor Server UI.
- `TaskForge.Tasks.Service` - ASP.NET Core Web API (controllers).
- `TaskForge.Tasks` - Shared domain models, EF Core database context and migrations.

## Features
- CRUD for tasks and subtasks
- Task status & priority
- PostgreSQL using EF Core
- Swagger/OpenAPI for API exploration (`{hostaddress:port}/swagger/index.html`)

## Prerequisites
- .NET 8 SDK
- PostgreSQL 
- Docker Desktop (for running PostgreSQL in a container)

## Quickstart
- Configure the database connection:
   - Edit `TaskForge.Tasks.Service/appsettings.json` (or environment variables) and set the `DefaultConnection` string to a valid PostgreSQL connection, for example:
     ```
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=taskforge;Username=postgres;Password=yourpassword"
     }
     ```

- EF Core migrations (example)
   - If migrations are not present, create and apply them from repo root:
     - Add migration:
       `dotnet ef migrations add InitialCreate -p TaskForge.Tasks.Service -s TaskForge.Tasks.Service`
     - Apply:
       `dotnet ef database update -p TaskForge.Tasks.Service -s TaskForge.Tasks.Service`
   - (If you use Visual Studio, use the Package Manager Console and set Default Project to `TaskForge.Tasks.Service`.)

- Run
   - From solution root:
     - `dotnet run --project TaskForge.Tasks.Service`
     - `dotnet run --project TaskForge.UI`
   - Or set both projects as startup projects (note: UI is still in progress).

- API docs
   - When running in Development, open the Swagger UI:
     - `https://{hostaddress}:{port}/swagger/index.html`


- If you change models, remember to add a new EF migration.

## Testing
- Add unit/integration tests in a `tests/` folder and run with:
  - `dotnet test`

## Contributing
- Fork, create a branch, implement changes, add/verify migrations, and open a PR.

## Useful files
- API service entry: `TaskForge.Tasks.Service/Program.cs`
- DB context: `TaskForge.Tasks/Database/TasksDbContext.cs` (or `AppDbContext`)
- Models: `TaskForge.Tasks/TaskItem.cs`, `Subtask.cs`
- UI layout/pages: `TaskForge.UI/Components/...`

