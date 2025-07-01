using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class CreateTimeRecordCommandHandler : ICommandHandler<CreateTimeRecordCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;

        public CreateTimeRecordCommandHandler(ITaskRepository taskRepository, IPersonRepository personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public async Task<Guid> HandleAsync(CreateTimeRecordCommand command)
        {
            var task = await _taskRepository.GetAsync(command.TaskId);
            var person = await _personRepository.GetAsync(command.PersonId);

            var timeRecord = task.CreateTimeRecord(person, command.Date, command.Duration);

            return timeRecord.Id;
        }
    }
}
