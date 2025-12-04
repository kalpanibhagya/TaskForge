using TaskForge.Tasks.Database;
using TaskStatus = TaskForge.Tasks.Database.TaskStatus;

namespace TaskForge.Tasks.Services;

public interface ITasksService
{
    Task<TaskItem> CreateTask(Guid projectId, Guid boardId, string name, string description, DateTime dueDate);
    Task UpdateTaskStatus(Guid taskId, TaskStatus newStatus);
    Task DeleteTask(Guid taskId);
    Task<TaskItem> GetTask(Guid taskId);
    Task<Subtask> CreateSubtask(Guid parentTaskId, string name, string description);
    Task UpdateSubtaskStatus(Guid subtaskId, TaskStatus newStatus);
    Task DeleteSubtask(Guid subtaskId);
    Task<Subtask> GetSubtask(Guid taskId);
}