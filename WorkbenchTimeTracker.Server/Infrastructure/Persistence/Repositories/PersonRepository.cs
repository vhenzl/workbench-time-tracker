using Microsoft.EntityFrameworkCore;
using WorkbenchTimeTracker.Server.Domain;
using Task = System.Threading.Tasks.Task;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence.Repositories
{
    public class PersonRepository(
        TimeTrackerDbContext db
    ) : IPersonRepository
    {
        public async Task<Person> GetAsync(Guid id)
        {
            return await FindAsync(id) ?? throw new BusinessException($"Person {id} not found.");
        }

        public Task<Person?> FindAsync(Guid id)
        {
            return db.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task AddAsync(Person person)
        {
            db.People.Add(person);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Person person)
        {
            db.People.Remove(person);
            return Task.CompletedTask;
        }
    }
}
