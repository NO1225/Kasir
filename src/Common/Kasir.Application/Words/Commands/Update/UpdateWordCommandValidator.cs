using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Kasir.Application.Words.Commands.Update
{
    public class UpdateWordCommandValidator : AbstractValidator<UpdateWordCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateWordCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                //.MustAsync(BeUniqueName).WithMessage("The specified country already exists. If you just want to activate the city leave the name field blank!")
                ;

            RuleFor(v => v.Id).NotNull();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            //TODO: Control by uppercase and CultureInfo
            return await _context.Countries.AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}
