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
    public class DeleteWordCommand : IRequestWrapper<WordDto>
    {
        public int Id { get; set; }
    }

    public class DeleteWordCommandHandler : IRequestHandlerWrapper<DeleteWordCommand, WordDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public DeleteWordCommandHandler(IApplicationDbContext context,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WordDto>> Handle(DeleteWordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Words
                .Include(w=>w.WordImages)
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Word), request.Id);
            }

            _context.Words.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await mediator.Send(new DeleteCountryImageCommand() { OldImageName = entity.ImageName });

            foreach (var wordImage in entity.WordImages)
            {
                await mediator.Send(new DeleteCountryImageCommand() { OldImageName = wordImage.ImageName });
            }

            return ServiceResult.Success(_mapper.Map<WordDto>(entity));
        }
    }
}
