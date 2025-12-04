namespace TaskForge.Tasks.Database;

public class BoardBase
{
    public Guid BoardId { get; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public BoardBase(string name)
    {
        BoardId = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class Board : BoardBase
{
    public Guid ProjectId { get; set; }
    public Guid Owner { get; set; }
    public int TaskItemCount { get; set; } = 0;
    public Board(Guid projectId, Guid owner, string name)
        : base(name) 
    {
        ProjectId = projectId;
        Owner = owner;
    }
}