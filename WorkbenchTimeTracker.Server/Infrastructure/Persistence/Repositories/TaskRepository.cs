using Microsoft.EntityFrameworkCore;
using WorkbenchTimeTracker.Server.Domain;
using Task = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence.Repositories
{
    public class TaskRepository(
        TimeTrackerDbContext db
    ) : ITaskRepository
    {
        public async Task<Domain.Task> GetAsync(Guid id)
        {
            return await FindAsync(id) ?? throw new NotFoundException($"Task {id} not found.");
        }

        public Task<Domain.Task?> FindAsync(Guid id)
        {
            return db.Tasks
                        .Include(x => x.TimeRecordsForEf)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Domain.Task> GetByTimeRecordAsync(Guid timeRecordId)
        {
            return await FindByTimeRecordAsync(timeRecordId) ?? throw new NotFoundException($"Task for TimeRecord {timeRecordId} not found.");
        }

        public Task<Domain.Task?> FindByTimeRecordAsync(Guid timeRecordId)
        {
            return db.Tasks
                        .Include(x => x.TimeRecordsForEf)
                        .FirstOrDefaultAsync(x => x.TimeRecordsForEf.Any(tr => tr.Id == timeRecordId));
        }

        public Task<List<Domain.Task>> GetAllAsync()
        {
            return db.Tasks
                        .Include(x => x.TimeRecordsForEf)
                        .ToListAsync();
        }

        public Task AddAsync(Domain.Task task)
        {
            db.Tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Domain.Task task)
        {
            db.Tasks.Remove(task);
            return Task.CompletedTask;
        }
    }
}
