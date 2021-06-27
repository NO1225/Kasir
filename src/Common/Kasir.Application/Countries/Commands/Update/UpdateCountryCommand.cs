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

namespace Kasir.Application.Countries.Commands.Update
{
    public class UpdateCountryCommand : IRequestWrapper<CountryDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CountryLanguageDto> CountryLanguageDtos { get; set; }

        internal IFileStream CountryImage;

        public void AddCountryImage(IFileStream fileStream)
        {
            CountryImage = fileStream;
        }
    }

    public class UpdateCountryCommandHandler : IRequestHandlerWrapper<UpdateCountryCommand, CountryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IApplicationDbContext context, 
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CountryDto>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Countries
                .Include(c=>c.CountryLanguages)
                .FirstOrDefaultAsync(c=>c.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;

            if(request.CountryLanguageDtos!=null && request.CountryLanguageDtos.Count > 0)
            {
                foreach (var cl in entity.CountryLanguages)
                {
                    var dto = request.CountryLanguageDtos.FirstOrDefault(cldd => cldd.LanguageId == cl.LanguageId);
                    if(dto != null)
                    {
                        cl.Name = dto.Name;
                    }
                }
            }

            if(request.CountryImage!= null)
            {
                var res = await mediator.Send(new UpdateCountryImageCommand
                {
                    CountryImage = request.CountryImage,
                    OldImageName = entity.ImagePath,
                });

                if (res.Succeeded == false)
                {
                    return new ServiceResult<CountryDto>(res.Error);
                }

                entity.ImagePath = res.Data;
            }
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CountryDto>(entity));
        }
    }
}
