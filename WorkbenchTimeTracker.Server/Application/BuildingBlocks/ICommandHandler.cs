namespace WorkbenchTimeTracker.Server.Application.BuildingBlocks
{
    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}