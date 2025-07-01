using WorkbenchTimeTracker.Server.Application.BuildingBlocks;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public record CreateTimeRecordCommand(
        Guid TaskId,
        Guid PersonId,
        DateOnly Date,
        TimeSpan Duration
    ) : ICommand<Guid>;
}
