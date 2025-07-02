using FluentValidation;
namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class CreateTimeRecordCommandValidator : AbstractValidator<CreateTimeRecordCommand>
    {
        public CreateTimeRecordCommandValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.PersonId).NotEmpty();
            RuleFor(x => x.Duration).GreaterThan(TimeSpan.Zero);
        }
    }
}