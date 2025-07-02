using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.People.DTOs;

namespace WorkbenchTimeTracker.Server.Application.People
{
    public record class GetPeopleQuery() : IQuery<List<PersonDto>>;
}
