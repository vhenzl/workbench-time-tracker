namespace WorkbenchTimeTracker.Server.Application.Tasks.DTOs
{
    public record class TimeRecordDto
    {
        public Guid Id { get; init; }
        public Guid TaskId { get; init; }
        public Guid PersonId { get; init; }
        public string PersonName { get; init; } = default!;
        public DateOnly Date { get; init; }
        public TimeSpan Duration { get; init; }
    }
}
