using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.Tasks.DTOs;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class GetTasksQueryHandler : IQueryHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository, IPersonRepository personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public async Task<List<TaskDto>> HandleAsync(GetTasksQuery query)
        {
            var tasks = await _taskRepository.GetAllAsync();

            var allPersonIds = tasks
                .SelectMany(t => t.TimeRecords.Select(tr => tr.PersonId))
                .Concat(tasks.Where(t => t.AssigneeId.HasValue).Select(t => t.AssigneeId!.Value))
                .Distinct();

            var persons = await _personRepository.FindByIdsAsync(allPersonIds);
            var personCache = persons.ToDictionary(x => x.Id, x => x.Name);

            return tasks.Select(task => TaskMappingHelper.MapToDto(task, personCache)).ToList();
        }
    }
}
