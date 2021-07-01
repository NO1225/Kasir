using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.PushTokens.Commands.Create
{
    public class CreatePushTokenCommand : IRequestWrapper<PushTokenDto>
    {
        public string Token { get; set; }

    }

    public class CreatePushTokenCommandHandler : IRequestHandlerWrapper<CreatePushTokenCommand, PushTokenDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePushTokenCommandHandler(IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PushTokenDto>> Handle(CreatePushTokenCommand request, CancellationToken cancellationToken)
        {
            PushToken entity;

            if (await _context.PushTokens.AnyAsync(pt => pt.Token == request.Token))
            {
                entity = await _context.PushTokens.FirstOrDefaultAsync(pt => pt.Token == request.Token);

                entity.Valid = true;
            }
            else
            {
                entity = new PushToken
                {
                    Token = request.Token,
                    Valid = true,
                };

                await _context.PushTokens.AddAsync(entity, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<PushTokenDto>(entity));
        }
    }
}
