using WorkbenchTimeTracker.Server.Domain;
using AsyncTask = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence
{
    public class UnitOfWork(TimeTrackerDbContext db) : IUnitOfWork
    {
        public AsyncTask CommitAsync()
        {
            return db.SaveChangesAsync();
        }
    }
}
