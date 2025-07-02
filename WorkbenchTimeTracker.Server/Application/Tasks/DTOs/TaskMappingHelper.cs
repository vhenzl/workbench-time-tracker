namespace WorkbenchTimeTracker.Server.Application.Tasks.DTOs
{
    public static class TaskMappingHelper
    {
        public static TaskDto MapToDto(Domain.Task task, Dictionary<Guid, string> personCache)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssigneeId = task.AssigneeId,
                AssigneeName = task.AssigneeId.HasValue && personCache.TryGetValue(task.AssigneeId.Value, out var assigneeName)
                    ? assigneeName
                    : null,
                TimeRecords = task.TimeRecords.Select(tr =>
                    new TimeRecordDto
                    {
                        Id = tr.Id,
                        TaskId = tr.TaskId,
                        PersonId = tr.PersonId,
                        PersonName = personCache.TryGetValue(tr.PersonId, out var name) ? name : string.Empty,
                        Date = tr.Date,
                        Duration = tr.Duration
                    }).ToList()
            };
        }
    }
}
