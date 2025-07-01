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
            if (_timeRecords.Any(x => x.PersonId == person.Id && x.Date == date))
                throw new BusinessException("A time record for this person and date already exists for this task.");

            var timeRecord = TimeRecord.Create(this, person, date, duration);
            _timeRecords.Add(timeRecord);
            return timeRecord;
        }

        public void UpdateTimeRecord(Guid timeRecordId, Person person, DateOnly date, TimeSpan duration)
        {
            var record = _timeRecords.FirstOrDefault(x => x.Id == timeRecordId);
            if (record == null)
                throw new BusinessException("Time record cannot be updated because it does not exist.");

            if (_timeRecords.Any(x => x.Id != timeRecordId && x.PersonId == person.Id && x.Date == date))
                throw new BusinessException("A time record for this person and date already exists for this task.");

            record.Update(person, date, duration);
        }

        public void DeleteTimeRecord(Guid timeRecordId)
        {
            var record = _timeRecords.FirstOrDefault(x => x.Id == timeRecordId);
            if (record == null)
                throw new BusinessException("Time record cannot be deleted because it does not exist.");
            _timeRecords.Remove(record);
        }
    }
}
