namespace WorkbenchTimeTracker.Server.Domain
{
    public sealed class Person
    {
        public Guid Id { get; }
        public string Name { get; }

        private Person(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Person Create(string name)
        {
            return new(Guid.NewGuid(), name);
        }
    }
}
