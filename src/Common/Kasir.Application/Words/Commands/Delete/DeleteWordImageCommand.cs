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

namespace Kasir.Application.Words.Commands.Delete
{
    public class DeleteWordImageCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteWordImageCommandHandler : IRequestHandlerWrapper<DeleteWordImageCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public DeleteWordImageCommandHandler(IApplicationDbContext context,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<bool>> Handle(DeleteWordImageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.WordImages
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Word), request.Id);
            }

            _context.WordImages.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await mediator.Send(new DeleteCountryImageCommand() { OldImageName = entity.ImageName });

            await mediator.Send(new DeleteCountryImageCommand() { OldImageName = entity.ImageName });


            return ServiceResult.Success(true);
        }
    }
}
