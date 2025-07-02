namespace WorkbenchTimeTracker.Server.Application.BuildingBlocks
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }

}