namespace TaskForge.Tasks.Services;

public interface ITasksService
{
    void CreateTask(string name, string description, DateTime dueDate);
    void UpdateTaskStatus(Guid taskId, TaskStatus newStatus);
    void DeleteTask(Guid taskId);
    TaskItem GetTask();
    void CreateSubtask(Guid parentTaskId, string name, string description);
    void UpdateSubtaskStatus(Guid subtaskId, TaskStatus newStatus);
    void DeleteSubtask(Guid subtaskId);
    Subtask GetSubtask();
}