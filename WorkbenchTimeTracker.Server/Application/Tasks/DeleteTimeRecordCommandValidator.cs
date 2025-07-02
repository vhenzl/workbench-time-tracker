using FluentValidation;
namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class DeleteTimeRecordCommandValidator : AbstractValidator<DeleteTimeRecordCommand>
    {
        public DeleteTimeRecordCommandValidator()
        {
            RuleFor(x => x.TimeRecordId).NotEmpty();
        }
    }
}