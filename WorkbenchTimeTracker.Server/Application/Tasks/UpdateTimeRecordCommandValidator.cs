using FluentValidation;
namespace WorkbenchTimeTracker.Server.Application.Tasks
{
    public class UpdateTimeRecordCommandValidator : AbstractValidator<UpdateTimeRecordCommand>
    {
        public UpdateTimeRecordCommandValidator()
        {
            RuleFor(x => x.TimeRecordId).NotEmpty();
            RuleFor(x => x.PersonId).NotEmpty();
            RuleFor(x => x.Duration).GreaterThan(TimeSpan.Zero);
        }
    }
}