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

namespace Kasir.Application.Countries.Queries.GetCountries
{
    public class GetAllCountriesQuery : IRequestWrapper<List<CountryDto>>
    {
        public int LanguageId { get; set; }
    }

    public class GetAllCountriesQueryHandler : IRequestHandlerWrapper<GetAllCountriesQuery, List<CountryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            List<CountryDto> list = await _context.Countries
                .Include(c => c.CountryLanguages)
                .Include(c => c.WordImages)
                .Where(c => c.WordImages.Count > 0)
                .Select(c => new CountryDto
                {
                    CreateDate = c.CreateDate,
                    Id = c.Id,
                    ImagePath = UploadDownloadHelper.ShowCountryImage(c.ImagePath),
                    Name = c.CountryLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId) == null ? c.Name : c.CountryLanguages.FirstOrDefault(cl => cl.LanguageId == request.LanguageId).Name
                })
                .ToListAsync(cancellationToken);

            return ServiceResult.Success(list);
        }
    }
}
