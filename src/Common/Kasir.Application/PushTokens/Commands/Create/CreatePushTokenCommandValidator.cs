using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Kasir.Application.PushTokens.Commands.Create
{
    public class CreatePushTokenCommandValidator : AbstractValidator<CreatePushTokenCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePushTokenCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Token)
                .MaximumLength(450).WithMessage("Name must not exceed 100 characters.")
                .NotEmpty().WithMessage("Name is required.");
        }

    }
}
