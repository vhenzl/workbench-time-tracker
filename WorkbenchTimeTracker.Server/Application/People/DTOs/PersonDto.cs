namespace WorkbenchTimeTracker.Server.Application.People.DTOs
{
    public record class PersonDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
    }
}
