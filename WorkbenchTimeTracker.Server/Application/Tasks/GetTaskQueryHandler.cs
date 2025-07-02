using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.Tasks.DTOs;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class GetTaskQueryHandler : IQueryHandler<GetTaskQuery, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;

        public GetTaskQueryHandler(ITaskRepository taskRepository, IPersonRepository personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public async Task<TaskDto> HandleAsync(GetTaskQuery query)
        {
            var task = await _taskRepository.GetAsync(query.Id);

            var personIds = task.TimeRecords.Select(tr => tr.PersonId)
                .Concat(task.AssigneeId.HasValue ? new[] { task.AssigneeId.Value } : Enumerable.Empty<Guid>())
                .Distinct();

            var persons = await _personRepository.FindByIdsAsync(personIds);
            var personCache = persons.ToDictionary(x => x.Id, x => x.Name);

            return TaskMappingHelper.MapToDto(task, personCache);
        }
    }
}
