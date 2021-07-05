using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Helpers;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.AppInfos.Queries.GetAppInfo
{
    public class GetAppInfoQuery : IRequestWrapper<AppInfoDto>
    {
        public int LanguageId { get; set; }
    }

    public class GetAppInfoQueryHandler : IRequestHandlerWrapper<GetAppInfoQuery, AppInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAppInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AppInfoDto>> Handle(GetAppInfoQuery request, CancellationToken cancellationToken)
        {
            AppInfoDto entity = await _context.AppInfos
                .Include(c => c.AppInfoLanguages)
                .Select(c => new AppInfoDto
                {                     
                    Title = c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null
                    ? "Kasir"
                    : c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Title,
                    Description = c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null
                    ? "Kasir"
                    : c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Description,
                    Disclaimer = c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null
                    ? "Kasir"
                    : c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Disclaimer,
                    Welcome = c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null
                    ? "Kasir"
                    : c.AppInfoLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Welcome
                })
                .FirstOrDefaultAsync(cancellationToken);

            return ServiceResult.Success(entity);
        }
    }
}
