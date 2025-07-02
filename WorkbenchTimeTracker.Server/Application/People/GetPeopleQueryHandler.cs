using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Application.People.DTOs;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Application.People
{
    public class GetPeopleQueryHandler : IQueryHandler<GetPeopleQuery, List<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;

        public GetPeopleQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<PersonDto>> HandleAsync(GetPeopleQuery query)
        {
            var people = await _personRepository.GetAllAsync();
            return people
                .Select(x => new PersonDto { Id = x.Id, Name = x.Name })
                .ToList();
        }
    }
}
