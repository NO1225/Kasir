using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Exceptions;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Files.Commands;
using Kasir.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kasir.Application.AppInfos.Commands.Update
{
    public class UpdateAppInfoCommand : IRequestWrapper<AppInfoDto>
    {

        public List<AppInfoLanguageDto> AppInfoLanguageDtos { get; set; }

    }

    public class UpdateAppInfoCommandHandler : IRequestHandlerWrapper<UpdateAppInfoCommand, AppInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public UpdateAppInfoCommandHandler(IApplicationDbContext context, 
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AppInfoDto>> Handle(UpdateAppInfoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.AppInfos
                .Include(c=>c.AppInfoLanguages)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request);
            }

            if(request.AppInfoLanguageDtos!=null && request.AppInfoLanguageDtos.Count > 0)
            {
                foreach (var cl in entity.AppInfoLanguages)
                {
                    var dto = request.AppInfoLanguageDtos.FirstOrDefault(cldd => cldd.LanguageId == cl.LanguageId);
                    if(dto != null)
                    {
                        cl.Title = dto.Title;
                        cl.Description = dto.Description;
                        cl.Disclaimer = dto.Disclaimer;
                        cl.Welcome = dto.Welcome;
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<AppInfoDto>(entity));
        }
    }
}
