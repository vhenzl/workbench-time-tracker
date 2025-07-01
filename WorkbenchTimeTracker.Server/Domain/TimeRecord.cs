namespace WorkbenchTimeTracker.Server.Domain
{
    public sealed class TimeRecord
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public Guid PersonId { get; }
        public DateOnly Date { get; }
        public TimeSpan Duration { get; }
        public DateTime CreatedAt { get; }

        private TimeRecord(Guid id, Guid taskId, Guid personId, DateOnly date, TimeSpan duration, DateTime createdAt)
        {
            Id = id;
            TaskId = taskId;
            PersonId = personId;
            Date = date;
            Duration = duration;
            CreatedAt = createdAt;
        }

        internal static TimeRecord Create(Task task, Person person, DateOnly date, TimeSpan duration)
        {
            return new (
                Guid.NewGuid(),
                task.Id,
                person.Id,
                date,
                duration,
                DateTime.UtcNow
            );
        }
    }
}
