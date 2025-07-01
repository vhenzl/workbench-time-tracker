using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkbenchTimeTracker.Server.Domain;
using WorkbenchTimeTracker.Server.Infrastructure.Persistence.Converters;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence.Configurations
{   
    public class TimeRecordConfiguration : IEntityTypeConfiguration<TimeRecord>
    {
        public void Configure(EntityTypeBuilder<TimeRecord> builder)
        {
            builder.ToTable("TimeRecords");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Date)
                .HasConversion(new DateOnlyConverter());

            builder.Property(x => x.Duration)
                .HasConversion(new TimeSpanConverter());

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.TaskId).IsRequired();
            builder.HasOne<Domain.Task>()
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.PersonId).IsRequired();
            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.TaskId, x.PersonId, x.Date }).IsUnique();

        }
    }
}