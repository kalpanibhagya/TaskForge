using Microsoft.EntityFrameworkCore;
using TaskForge.Tasks.Database;

namespace TaskForge.Tasks.Services;
public class ProjectsService : IProjectsService
{
    private readonly TasksDbContext _db;
    public ProjectsService(TasksDbContext db)
    {
        _db = db;
    }

    public async Task<Project> CreateProject(string name, Guid initiatedBy)
    {
        var project = new Project(name, initiatedBy);
        _db.projects.Add(project);
        await _db.SaveChangesAsync();
        return project;
    }

    public async Task DeleteProject(Guid projectId)
    {
        var project = await _db.projects.FindAsync(projectId);
        _db.projects.Remove(project);
        await _db.SaveChangesAsync();
        // todo: delete all boards, tasks and sub tasks associated with this project
    }
    public async Task<Project> GetProject(Guid projectId)
    {
        return await _db.projects.FirstOrDefaultAsync(project =>
            project.ProjectId == projectId);
    }

    public async Task<Board> CreateBoard(Guid projectId, Guid owner, string name)
    {
        var board = new Board(projectId, owner, name);
        _db.boards.Add(board);
        await _db.SaveChangesAsync();
        return board;

    }

    public async Task<Board> GetBoard(Guid boardId)
    {
        return await _db.boards.FirstOrDefaultAsync(board =>
            board.BoardId == boardId);
    }

    public async Task DeleteBoard(Guid boardId)
    {
        var board = await _db.boards.FindAsync(boardId);
        _db.boards.Remove(board);
        await _db.SaveChangesAsync();
        // todo: delete all tasks and sub tasks associated with this project
    }

}
