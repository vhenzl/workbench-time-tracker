using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.Tasks.DTOs;

namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public record class GetTimeRecordQuery(Guid Id) : IQuery<TimeRecordDto>;
}
