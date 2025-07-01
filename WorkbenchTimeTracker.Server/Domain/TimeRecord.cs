namespace WorkbenchTimeTracker.Server.Domain
{
    public sealed class TimeRecord
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public Guid PersonId { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime CreatedAt { get; }

        private TimeRecord(Guid id, Guid taskId, Guid personId, DateOnly date, TimeSpan duration, DateTime createdAt)
        {
            ValidateDuration(duration);
            Id = id;
            TaskId = taskId;
            PersonId = personId;
            Date = date;
            Duration = duration;
            CreatedAt = createdAt;
        }

        internal static TimeRecord Create(Task task, Person person, DateOnly date, TimeSpan duration)
        {
            return new TimeRecord(Guid.NewGuid(), task.Id, person.Id, date, duration, DateTime.UtcNow);
        }

        internal void Update(Person person, DateOnly date, TimeSpan duration)
        {
            ValidateDuration(duration);
            PersonId = person.Id;
            Date = date;
            Duration = duration;
        }

        private static void ValidateDuration(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero || duration > TimeSpan.FromHours(24))
                throw new BusinessException("Duration must be greater than 0 and less than or equal to 24 hours.");
        }
    }
}
