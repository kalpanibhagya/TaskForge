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

public class TasksService : ITasksService
{
    public void CreateTask(string name, string description, DateTime dueDate)
    {
        var task =  new TaskItem(name, description, dueDate);
        // Logic to save the task to a database or in-memory collection
    }

    public void UpdateTaskStatus(Guid taskId, TaskStatus newStatus)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public Task GetTask()
    {
        throw new NotImplementedException();
    }

    public void CreateSubtask(Guid parentTaskId, string name, string description)
    {
        var subtask = new Subtask(parentTaskId, name, description);
        // Logic to save the subtask to a database or in-memory collection
    }

    public void UpdateSubtaskStatus(Guid subtaskId, TaskStatus newStatus)
    {
        throw new NotImplementedException();
    }

    public void DeleteSubtask(Guid subtaskId) 
    {
        throw new NotImplementedException();
    }
    public Subtask GetSubtask()
    {
        throw new NotImplementedException();
    }



}