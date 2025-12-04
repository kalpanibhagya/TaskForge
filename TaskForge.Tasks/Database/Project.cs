namespace TaskForge.Tasks.Database;
public class Project
{
    public Guid ProjectId { get; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid InitiatedBy { get; set; }
    public int BoardCount { get; set; }
    public List<Guid> Boards { get; set; } = new();

    public Project(string name, Guid initiatedBy)
    {
        ProjectId = Guid.NewGuid();
        Name = name;
        InitiatedBy = initiatedBy;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        BoardCount = 0;
    }
}