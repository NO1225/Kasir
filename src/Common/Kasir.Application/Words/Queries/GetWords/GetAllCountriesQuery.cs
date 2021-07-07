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

namespace Kasir.Application.Words.Queries.GetWords
{
    public class GetAllWordsQuery : IRequestWrapper<List<WordDto>>
    {
        public int LanguageId { get; set; }
        public int CountryId { get; set; }
    }

    public class GetAllWordsQueryHandler : IRequestHandlerWrapper<GetAllWordsQuery, List<WordDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllWordsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<WordDto>>> Handle(GetAllWordsQuery request, CancellationToken cancellationToken)
        {
            List<WordDto> list = await _context.Words
                .Include(w => w.WordLanguages)
                .Include(w => w.WordCountries)
                .Where(w => w.WordCountries.Any(wi => wi.CountryId == request.CountryId))
                .Select(c => new WordDto
                {
                    CreateDate = c.CreateDate,
                    Id = c.Id,
                    ImageName = UploadDownloadHelper.ShowWordImage(c.ImageName),
                    Title = c.WordLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null 
                    ? ""
                    : c.WordLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Title,
                    Name = c.Name,
                    Information = c.WordLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null
                    ? ""
                    : c.WordLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Information
                }).ToListAsync(cancellationToken);

            return ServiceResult.Success(list);
        }
    }
}
