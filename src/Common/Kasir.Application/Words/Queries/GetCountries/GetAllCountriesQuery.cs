//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using Kasir.Application.Common.Interfaces;
//using Kasir.Application.Common.Models;
//using Kasir.Application.Dto;
//using Mapster;
//using MapsterMapper;
//using Microsoft.EntityFrameworkCore;

//namespace Kasir.Application.Countries.Queries.GetCountries
//{
//    public class GetAllCountriesQuery : IRequestWrapper<List<CountryDto>>
//    {

//    }

//    public class GetCitiesQueryHandler : IRequestHandlerWrapper<GetAllCountriesQuery, List<CountryDto>>
//    {
//        private readonly IApplicationDbContext _context;
//        private readonly IMapper _mapper;

//        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<ServiceResult<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
//        {
//            List<CountryDto> list = await _context.Countries
//                .ProjectToType<CountryDto>(_mapper.Config)
//                .ToListAsync(cancellationToken);

//            return ServiceResult.Success(list);
//        }
//    }
//}
