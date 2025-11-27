namespace TaskForge.Tasks;

public abstract class TaskBase
{
    public Guid TaskId { get; protected set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    protected TaskBase() { }

    public TaskBase(string name, string description)
    {
        TaskId = Guid.NewGuid();
        Name = name;
        Description = description ?? "";
    }

    public void MarkCompleted() => Status = TaskStatus.Completed;
    public void MarkInProgress() => Status = TaskStatus.InProgress;
    public void Hold() => Status = TaskStatus.OnHold;

}

public class TaskItem : TaskBase
{
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public string Label { get; set; }
    public bool IsRecurring { get; set; } = false;
    protected TaskItem() : base() { }
    public TaskItem(string name, string description, DateTime dueDate)
        : base(name, description)
    {
        DueDate = dueDate;
    }

    public void MakeHighPrioty() => Priority = TaskPriority.High;
    public void MarkLowPriority() => Priority = TaskPriority.Low;
}

public class Subtask : TaskBase
{
    public Guid ParentTaskId { get; set; }
    protected Subtask() : base() { }
    public Subtask(Guid parentId, string name, string description)
        : base(name, description)
    {
        ParentTaskId = parentId;
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