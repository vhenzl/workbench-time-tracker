using System;
using AsyncTask = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Domain
{
    public interface IPersonRepository
    {
        Task<Person> GetAsync(Guid id);
        Task<Person?> FindAsync(Guid id);
        Task<List<Person>> FindByIdsAsync(IEnumerable<Guid> ids);
        Task<List<Person>> GetAllAsync();
        AsyncTask AddAsync(Person person);
        AsyncTask RemoveAsync(Person person);
    }
}
