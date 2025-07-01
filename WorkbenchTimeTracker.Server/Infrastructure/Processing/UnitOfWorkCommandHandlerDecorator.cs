using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly IUnitOfWork _uow;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> inner,
            IUnitOfWork uow)
        {
            _inner = inner;
            _uow = uow;
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            var result = await _inner.HandleAsync(command);
            await _uow.CommitAsync();
            return result;
        }
    }
}
