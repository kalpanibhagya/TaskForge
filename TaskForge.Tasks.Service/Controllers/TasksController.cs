namespace TaskForge.Tasks.Service.Controllers;

[ApiController]
[Route("Tasks")]
public class TasksController : ControllerBase
{
    public required ITasksService TasksService { protected get; init; }

    //[HttpGet]
    //public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllTasksAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var task = await TasksService.GetTask(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var task = await TasksService.CreateTask(request.Name, request.Description, request.DueDate);
        return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
    {
        await TasksService.UpdateTaskStatus(request.TaskId, request.Status);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await TasksService.DeleteTask(id);
        return NoContent();
    }

    // Subtasks
    [HttpPost("{id:guid}/subtasks")]
    public async Task<IActionResult> CreateSubtask([FromBody] CreateSubtaskRequest request)
    {
        var sub = await TasksService.CreateSubtask(request.ParentId, request.Name, request.Description);
        return CreatedAtAction(nameof(GetSubtask), new { subtaskId = sub.TaskId }, sub);
    }

    [HttpGet("subtasks/{subtaskId:guid}")]
    public async Task<IActionResult> GetSubtask(Guid subtaskId)
    {
        var s = await TasksService.GetSubtask(subtaskId);
        return s is null ? NotFound() : Ok(s);
    }

    [HttpPatch("subtasks/{subtaskId:guid}/status")]
    public async Task<IActionResult> UpdateSubtaskStatus([FromBody] UpdateStatusRequest request)
    {
        await TasksService.UpdateSubtaskStatus(request.TaskId, request.Status);
        return NoContent();
    }

    [HttpDelete("subtasks/{subtaskId:guid}")]
    public async Task<IActionResult> DeleteSubtask(Guid subtaskId)
    {
        await TasksService.DeleteSubtask(subtaskId);
        return NoContent();
    }
   
}