using TaskForge.Tasks.Database;
using TaskStatus = TaskForge.Tasks.Database.TaskStatus;

namespace TaskForge.Tasks.Service.Requests;

public class CreateTaskRequest 
{
    [Required]
    public Guid ProjectId { get; set; }
    [Required]
    public Guid BoardId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
}
public class UpdateStatusRequest 
{
    [Required] 
    public Guid TaskId { get; set; }

    [Required]
    public TaskStatus Status { get; set; }
}

public class GetTaskRequest
{
    [Required]
    public Guid TaskId { get; set; }
}

public class DeleteTaskRequest
{
    [Required]
    public Guid TaskId { get; set; }
}

public class CreateSubtaskRequest
{
    [Required]
    public Guid ParentId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string? Description { get; set; }
}