namespace WorkbenchTimeTracker.Server.Application.BuildingBlocks
{
    public class InvalidCommandException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public InvalidCommandException(IEnumerable<string> errors)
            : base("The command is invalid.")
        {
            Errors = errors.ToList();
        }

        public InvalidCommandException(string error)
            : this(new[] { error }) { }
    }
}
