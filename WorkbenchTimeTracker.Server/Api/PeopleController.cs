using Microsoft.AspNetCore.Mvc;
using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.People.DTOs;
using WorkbenchTimeTracker.Server.Application.People;

namespace WorkbenchTimeTracker.Server.Api
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IQueryHandler<GetPeopleQuery, List<PersonDto>> _getPeopleHandler;

        public PeopleController(
            IQueryHandler<GetPeopleQuery, List<PersonDto>> getPeopleHandler)
        {
            _getPeopleHandler = getPeopleHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetAllPeople()
        {
            var result = await _getPeopleHandler.HandleAsync(new GetPeopleQuery());
            return Ok(result);
        }
    }
}
