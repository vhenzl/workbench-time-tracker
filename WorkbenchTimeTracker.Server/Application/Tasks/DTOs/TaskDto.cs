namespace WorkbenchTimeTracker.Server.Application.Tasks.DTOs
{
    public record class TaskDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = default!;
        public string? Description { get; init; }
        public Guid? AssigneeId { get; init; }
        public string? AssigneeName { get; init; }
        public List<TimeRecordDto> TimeRecords { get; init; } = new();
    }
}
