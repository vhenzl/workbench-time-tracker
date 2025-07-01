using AsyncTask = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Domain
{
    public interface IUnitOfWork
    {
        AsyncTask CommitAsync();
    }
}
