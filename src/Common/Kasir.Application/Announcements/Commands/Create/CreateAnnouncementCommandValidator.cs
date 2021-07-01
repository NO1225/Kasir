using FluentValidation;

namespace Kasir.Application.Announcements.Commands.Create
{
    public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
    {

        public CreateAnnouncementCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .NotEmpty().WithMessage("Name is required.");


            RuleFor(v => v.Body)
                .MaximumLength(450).WithMessage("Name must not exceed 450 characters.")
                .NotEmpty().WithMessage("Name is required.");
        }

    }
}
