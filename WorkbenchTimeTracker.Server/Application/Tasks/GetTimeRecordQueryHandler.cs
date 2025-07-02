using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.Tasks.DTOs;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class GetTimeRecordQueryHandler : IQueryHandler<GetTimeRecordQuery, TimeRecordDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;

        public GetTimeRecordQueryHandler(ITaskRepository taskRepository, IPersonRepository personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public async Task<TimeRecordDto> HandleAsync(GetTimeRecordQuery query)
        {
            var task = await _taskRepository.GetByTimeRecordAsync(query.Id);
            var record = task.TimeRecords.FirstOrDefault(tr => tr.Id == query.Id)
                ?? throw new NotFoundException($"TimeRecord {query.Id} not found.");

            var person = await _personRepository.FindAsync(record.PersonId);

            return new TimeRecordDto
            {
                Id = record.Id,
                TaskId = record.TaskId,
                PersonId = record.PersonId,
                PersonName = person?.Name ?? string.Empty,
                Date = record.Date,
                Duration = record.Duration
            };
        }
    }
}
