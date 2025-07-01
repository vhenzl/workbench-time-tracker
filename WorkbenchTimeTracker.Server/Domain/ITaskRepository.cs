using AsyncTask = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Domain
{
    public interface ITaskRepository
    {
        Task<Domain.Task> GetAsync(Guid id);
        Task<Domain.Task?> FindAsync(Guid id);
        Task<Domain.Task?> FindByTimeRecordAsync(Guid timeRecordId);
        Task<Domain.Task> GetByTimeRecordAsync(Guid timeRecordId);
        AsyncTask AddAsync(Domain.Task task);
        AsyncTask RemoveAsync(Domain.Task task);
    }
}
