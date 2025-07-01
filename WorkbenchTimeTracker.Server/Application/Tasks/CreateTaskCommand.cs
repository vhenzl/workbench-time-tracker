using WorkbenchTimeTracker.Server.Application.BuildingBlocks;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public record CreateTaskCommand(
        string Title,
        string? Description,
        Guid? AssigneeId
    ) : ICommand<Guid>;
}
