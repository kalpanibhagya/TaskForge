using TaskForge.Tasks.Database;

namespace TaskForge.Tasks.Services;
public interface IProjectsService
{
    Task<Project> CreateProject(string name, Guid initiatedBy);
    Task<Project> GetProject(Guid projectId);
    Task DeleteProject(Guid projectId);
    Task<Board> CreateBoard(Guid projectId, Guid owner, string name);
    Task<Board> GetBoard(Guid boardId);
    Task DeleteBoard(Guid boardId);
}