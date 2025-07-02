using FluentValidation;
using WorkbenchTimeTracker.Server.Application.BuildingBlocks;

namespace WorkbenchTimeTracker.Server.Infrastructure.Processing
{
    public class ValidatorCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly IValidator<TCommand>? _validator;

        public ValidatorCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> inner,
            IValidator<TCommand>? validator = null)
        {
            _inner = inner;
            _validator = validator;
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(command);
                if (!result.IsValid)
                    throw new InvalidCommandException(result.Errors.Select(e => e.ErrorMessage));
            }
            return await _inner.HandleAsync(command);
        }
    }
}
