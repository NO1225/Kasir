using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Helpers;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Languages.Queries.GetLanguages
{
    public class GetAllLanguagesQuery : IRequestWrapper<List<LanguageDto>>
    {
    }

    public class GetAllLanguagesQueryHandler : IRequestHandlerWrapper<GetAllLanguagesQuery, List<LanguageDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllLanguagesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<LanguageDto>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            List<LanguageDto> list = await _context.Languages
                .Select(c => new LanguageDto
                {
                    CreateDate = c.CreateDate,
                    Id = c.Id,
                    Name = c.Name,
                    ImagePath = UploadDownloadHelper.ShowLanguageImage(c.ImageName)
                }).ToListAsync(cancellationToken);

            return ServiceResult.Success(list);
        }
    }
}
