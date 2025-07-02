using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.Tasks;
using WorkbenchTimeTracker.Server.Application.Tasks.DTOs;

namespace WorkbenchTimeTracker.Server.Api
{
    [ApiController]
    [Route("api/time-records")]
    public class TimeRecordsController : ControllerBase
    {
        private readonly ICommandHandler<CreateTimeRecordCommand, Guid> _createTimeRecordHandler;
        private readonly ICommandHandler<UpdateTimeRecordCommand, Unit> _updateTimeRecordHandler;
        private readonly ICommandHandler<DeleteTimeRecordCommand, Unit> _deleteTimeRecordHandler;
        private readonly IQueryHandler<GetTimeRecordQuery, TimeRecordDto> _getTimeRecordHandler;

        public TimeRecordsController(
            ICommandHandler<CreateTimeRecordCommand, Guid> createTimeRecordHandler,
            ICommandHandler<UpdateTimeRecordCommand, Unit> updateTimeRecordHandler,
            ICommandHandler<DeleteTimeRecordCommand, Unit> deleteTimeRecordHandler,
            IQueryHandler<GetTimeRecordQuery, TimeRecordDto> getTimeRecordHandler)
        {
            _createTimeRecordHandler = createTimeRecordHandler;
            _updateTimeRecordHandler = updateTimeRecordHandler;
            _deleteTimeRecordHandler = deleteTimeRecordHandler;
            _getTimeRecordHandler = getTimeRecordHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeRecordDto>> Get(Guid id)
        {
            var dto = await _getTimeRecordHandler.HandleAsync(new GetTimeRecordQuery(id));
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TimeRecordDto>> Post([FromBody] CreateTimeRecordCommand command)
        {
            var id = await _createTimeRecordHandler.HandleAsync(command);
            var dto = await _getTimeRecordHandler.HandleAsync(new GetTimeRecordQuery(id));
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TimeRecordDto>> Put(Guid id, [FromBody] UpdateTimeRecordCommand command)
        {
            await _updateTimeRecordHandler.HandleAsync(command with { TimeRecordId = id });
            var dto = await _getTimeRecordHandler.HandleAsync(new GetTimeRecordQuery(id));
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteTimeRecordHandler.HandleAsync(new DeleteTimeRecordCommand(id));
            return NoContent();
        }

    }

}
