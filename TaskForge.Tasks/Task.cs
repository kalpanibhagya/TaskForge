namespace TaskForge.Tasks;

public class Task
{
    public Guid TaskId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public int Priority { get; set; }
    public string Label { get; set; }
    public Task(Guid taskId, string name, string description, DateTime dueDate, TaskStatus status, int priority)
    {
        TaskId = taskId;
        Name = name;
        Description = description;
        DueDate = dueDate;
        Status = status;
        Priority = priority;
    }
}

public class Subtask
{
    public Guid SubtaskId { get; set; }
    public Guid ParentTaskId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public Subtask(Guid subtaskId, Guid parentTaskId, string name, TaskStatus status)
    {
        SubtaskId = subtaskId;
        ParentTaskId = parentTaskId;
        Name = name;
        Status = status;
    }
}

public enum TaskPriority
{
    Low,
    Medium,
    High
}

public enum TaskStatus
{
    Todo,
    InProgress,
    Completed,
    OnHold,
    Ignored
}