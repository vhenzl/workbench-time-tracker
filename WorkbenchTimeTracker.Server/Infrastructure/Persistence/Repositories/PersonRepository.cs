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
            return await FindAsync(id) ?? throw new NotFoundException($"Person {id} not found.");
        }

        public Task<Person?> FindAsync(Guid id)
        {
            return db.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Person>> FindByIdsAsync(IEnumerable<Guid> ids)
        {
            return db.People.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public Task<List<Person>> GetAllAsync()
        {
            return db.People.ToListAsync();
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
