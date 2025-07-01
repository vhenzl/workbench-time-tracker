using WorkbenchTimeTracker.Server.Application.BuildingBlocks;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public record UpdateTimeRecordCommand(
        Guid TimeRecordId,
        Guid PersonId,
        DateOnly Date,
        TimeSpan Duration
    ) : ICommand<Unit>;
}
