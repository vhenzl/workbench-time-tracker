using Microsoft.EntityFrameworkCore;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence
{
    public class TimeTrackerDbContext : DbContext
    {
        public DbSet<Domain.Person> People => Set<Domain.Person>();
        public DbSet<Domain.Task> Tasks => Set<Domain.Task>();
        public DbSet<Domain.TimeRecord> TimeRecords => Set<Domain.TimeRecord>();

        public TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimeTrackerDbContext).Assembly);
        }
    }
}
