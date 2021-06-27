using Kasir.Application.Common.Exceptions;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Files.Commands;
using Kasir.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Countries.Commands.Delete
{
    public class DeleteCountryCommand : IRequestWrapper<CountryDto>
    {
        public int Id { get; set; }
    }

    public class DeleteCityCommandHandler : IRequestHandlerWrapper<DeleteCountryCommand, CountryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public DeleteCityCommandHandler(IApplicationDbContext context,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CountryDto>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Countries
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            _context.Countries.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await mediator.Send(new DeleteCountryImageCommand() { OldImageName = entity.ImagePath });

            return ServiceResult.Success(_mapper.Map<CountryDto>(entity));
        }
    }
}
