using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskForge.Tasks;

public abstract class TaskBase
{
    [Key]
    public Guid TaskId { get; protected set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    public string Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

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
    [Required, ForeignKey(nameof(TaskItem))]
    public Guid ParentTaskId { get; set; }

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