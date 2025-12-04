namespace TaskForge.Tasks.Service.Controllers;

[ApiController]
[Route("Tasks")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService ?? 
            throw new ArgumentNullException(nameof(tasksService));
    }

    // todo: error handling and add a controller to get all tasks

    [HttpGet("getTask")]
    public async Task<IActionResult> GetTask([FromQuery] GetTaskRequest request)
    {
        var task = await _tasksService.GetTask(request.TaskId);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost("createTask")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var task = await _tasksService.CreateTask(request.ProjectId, request.BoardId,
            request.Name, request.Description, request.DueDate);
        return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
    }

    [HttpPatch("updateStatus")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
    {
        await _tasksService.UpdateTaskStatus(request.TaskId, request.Status);
        return NoContent();
    }

    [HttpDelete("deleteTask")]
    public async Task<IActionResult> Delete([FromBody] DeleteTaskRequest request)
    {
        await _tasksService.DeleteTask(request.TaskId);
        return NoContent();
    }

    #region subtasks

    [HttpPost("createSubtask")]
    public async Task<IActionResult> CreateSubtask([FromBody] CreateSubtaskRequest request)
    {
        var sub = await _tasksService.CreateSubtask(request.ParentId, request.Name, request.Description);
        return CreatedAtAction(nameof(GetSubtask), new { subtaskId = sub.TaskId }, sub);
    }

    [HttpGet("getSubtask")]
    public async Task<IActionResult> GetSubtask([FromQuery] GetTaskRequest request)
    {
        var s = await _tasksService.GetSubtask(request.TaskId);
        return s is null ? NotFound() : Ok(s);
    }

    [HttpPatch("updateSubtaskStatus")]
    public async Task<IActionResult> UpdateSubtaskStatus([FromBody] UpdateStatusRequest request)
    {
        await _tasksService.UpdateSubtaskStatus(request.TaskId, request.Status);
        return NoContent();
    }

    [HttpDelete("deleteSubtask")]
    public async Task<IActionResult> DeleteSubtask(Guid subtaskId)
    {
        await _tasksService.DeleteSubtask(subtaskId);
        return NoContent();
    }

    #endregion
}