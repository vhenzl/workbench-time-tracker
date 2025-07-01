using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IPersonRepository personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public async Task<Guid> HandleAsync(CreateTaskCommand command)
        {
            Person? assignee = null;
            if (command.AssigneeId.HasValue)
                assignee = await _personRepository.GetAsync(command.AssigneeId.Value);

            var task = Domain.Task.Create(
                command.Title,
                command.Description ?? string.Empty,
                assignee
            );

            await _taskRepository.AddAsync(task);
            return task.Id;
        }
    }
}
