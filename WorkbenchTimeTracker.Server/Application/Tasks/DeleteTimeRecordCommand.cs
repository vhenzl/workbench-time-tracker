using WorkbenchTimeTracker.Server.Application.BuildingBlocks;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public record DeleteTimeRecordCommand(
        Guid TimeRecordId
    ) : ICommand<Unit>;
}
