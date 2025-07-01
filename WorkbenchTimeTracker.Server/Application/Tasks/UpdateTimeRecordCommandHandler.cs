using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class UpdateTimeRecordCommandHandler : ICommandHandler<UpdateTimeRecordCommand, Unit>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;

        public UpdateTimeRecordCommandHandler(ITaskRepository taskRepository, IPersonRepository personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public async Task<Unit> HandleAsync(UpdateTimeRecordCommand command)
        {
            var task = await _taskRepository.GetByTimeRecordAsync(command.TimeRecordId);
            var person = await _personRepository.GetAsync(command.PersonId);

            task.UpdateTimeRecord(command.TimeRecordId, person, command.Date, command.Duration);

            return Unit.Value;
        }
    }
}
