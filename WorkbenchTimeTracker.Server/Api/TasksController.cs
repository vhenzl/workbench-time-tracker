using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.Tasks;
using WorkbenchTimeTracker.Server.Application.Tasks.DTOs;

namespace WorkbenchTimeTracker.Server.Api
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly IQueryHandler<GetTaskQuery, TaskDto> _getTaskHandler;
        private readonly IQueryHandler<GetTasksQuery, List<TaskDto>> _getTasksHandler;
        private readonly ICommandHandler<CreateTaskCommand, Guid> _createTaskHandler;

        public TasksController(
            IQueryHandler<GetTaskQuery, TaskDto> getTaskHandler,
            IQueryHandler<GetTasksQuery, List<TaskDto>> getTasksHandler,
            ICommandHandler<CreateTaskCommand, Guid> createTaskHandler)
        {
            _getTaskHandler = getTaskHandler;
            _getTasksHandler = getTasksHandler;
            _createTaskHandler = createTaskHandler;
        }    

        [HttpGet]
        public async Task<List<TaskDto>> Get()
        {
            return await _getTasksHandler.HandleAsync(new GetTasksQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Get(Guid id)
        {
            var dto = await _getTaskHandler.HandleAsync(new GetTaskQuery(id));
            return Ok(dto);
        }   
        
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] CreateTaskCommand command)
        {
            var taskId = await _createTaskHandler.HandleAsync(command);
            var dto = await _getTaskHandler.HandleAsync(new GetTaskQuery(taskId));

            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                dto
            );
        }
    }

}
