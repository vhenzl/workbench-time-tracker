using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class DeleteTimeRecordCommandHandler : ICommandHandler<DeleteTimeRecordCommand, Unit>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTimeRecordCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> HandleAsync(DeleteTimeRecordCommand command)
        {
            var task = await _taskRepository.GetByTimeRecordAsync(command.TimeRecordId);
            task.DeleteTimeRecord(command.TimeRecordId);
            return Unit.Value;
        }
    }
}
