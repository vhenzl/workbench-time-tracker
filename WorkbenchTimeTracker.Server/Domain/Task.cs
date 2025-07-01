namespace WorkbenchTimeTracker.Server.Domain
{
    public sealed class Task
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public Guid? AssigneeId { get; }
        public DateTime CreatedAt { get; }

        private readonly List<TimeRecord> _timeRecords = new();
        public IReadOnlyCollection<TimeRecord> TimeRecords => _timeRecords.AsReadOnly();

        private Task(Guid id, string title, string description, Guid? assigneeId, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            AssigneeId = assigneeId;
            CreatedAt = createdAt;
        }

        public static Task Create(string title, string description, Person? assignee = null)
        {
            return new(Guid.NewGuid(), title, description, assignee?.Id, DateTime.UtcNow);
        }

        public TimeRecord CreateTimeRecord(Person person, DateOnly date, TimeSpan duration)
        {
            var timeRecord = TimeRecord.Create(this, person, date, duration);
            _timeRecords.Add(timeRecord);
            return timeRecord;
        }
    }
}
