using AsyncTask = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Domain
{
    public interface IPersonRepository
    {
        Task<Person> GetAsync(Guid id);
        Task<Person?> FindAsync(Guid id);
        AsyncTask AddAsync(Person person);
        AsyncTask RemoveAsync(Person person);
    }
}
