using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Files.Commands;
using Kasir.Domain.Entities;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Countries.Commands.Create
{
    public class CreateCountryCommand : IRequestWrapper<CountryDto>
    {
        public string Name { get; set; }

        public List<CountryLanguageDto> CountryLanguageDtos { get; set; }


        internal IFileStream CountryImage;

        public void AddCountryImage(IFileStream fileStream)
        {
            CountryImage = fileStream;
        }
    }

    public class CreateCountryCommandHandler : IRequestHandlerWrapper<CreateCountryCommand, CountryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IApplicationDbContext context,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CountryDto>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Country
            {
                Name = request.Name,
                CountryLanguages = request.CountryLanguageDtos.Select(cl => new CountryLanguage
                {
                    LanguageId = cl.LanguageId,
                    Name = cl.Name,
                }).ToList()
            };

            var res = await mediator.Send(new AddCountryImageCommand { CountryImage = request.CountryImage });

            if(res.Succeeded == false)
            {
                return new ServiceResult<CountryDto>(res.Error);
            }

            entity.ImagePath = res.Data;

            await _context.Countries.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CountryDto>(entity));
        }
    }
}
