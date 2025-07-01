using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence.Converters
{
    public class TimeSpanConverter : ValueConverter<TimeSpan, long>
    {
        public TimeSpanConverter()
            : base(
                timeSpan => timeSpan.Ticks,
                ticks => TimeSpan.FromTicks(ticks)
            )
        { }
    }
}
