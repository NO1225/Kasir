using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Kasir.Application.Words.Commands.Create
{
    public class CreateWordCommandValidator : AbstractValidator<CreateWordCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateWordCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified code already exists.")
                .NotEmpty().WithMessage("Name is required.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            //TODO: Control by uppercase and CultureInfo
            return await _context.Words.AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}
