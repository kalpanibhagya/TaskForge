namespace TaskForge.Tasks.Service.Requests;

public class CreateTaskRequest 
{
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
public class CreateSubtaskRequest
{
    [Required]
    public Guid ParentId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string? Description { get; set; }
}