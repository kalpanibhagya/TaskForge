using Microsoft.EntityFrameworkCore;
using TaskForge.Tasks.Database;

namespace TaskForge.Tasks.Services;

public class TasksService : ITasksService
{
    private readonly AppDbContext _db;
    public TasksService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<TaskItem> CreateTask(string name, string description, DateTime dueDate)
    {
        var task = new TaskItem(name, description, dueDate);
        _db.tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task UpdateTaskStatus(Guid taskId, TaskStatus newStatus)
    {
        var task = await _db.tasks.FindAsync(taskId);
        task.Status = newStatus;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteTask(Guid taskId)
    {
        var task = await _db.tasks.FindAsync(taskId);
        _db.tasks.Remove(task);
        await _db.SaveChangesAsync();
    }

    public async Task<TaskItem> GetTask(Guid id)
    {
        return await _db.tasks.FirstOrDefaultAsync(t => t.TaskId == id);
    }

    public async Task<Subtask> CreateSubtask(Guid parentTaskId, string name, string description)
    {
        var parent = await _db.tasks.FindAsync(parentTaskId);
        if (parent == null)
            throw new KeyNotFoundException("Parent task not found");
        var sub = new Subtask(parentTaskId, name, description);
        _db.subtasks.Add(sub);
        await _db.SaveChangesAsync();
        return sub;

    }

    public async Task UpdateSubtaskStatus(Guid subtaskId, TaskStatus newStatus)
    {
        var sub = await _db.subtasks.FindAsync(subtaskId);
        sub.Status = newStatus;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteSubtask(Guid subtaskId)
    {
        var sub = await _db.tasks.FindAsync(subtaskId);
        _db.tasks.Remove(sub);
        await _db.SaveChangesAsync();
    }
    public async Task<Subtask> GetSubtask(Guid id)
    {
        return await _db.subtasks.FirstOrDefaultAsync(t => t.TaskId == id);
    }
}
