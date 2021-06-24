using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Common.Security;
using Kasir.Application.Dto;
using Kasir.Domain.Entities;
using MapsterMapper;

namespace Kasir.Application.Countries.Commands.Create
{
    [Authorize]
    public class CreateCountryCommand : IRequestWrapper<CountryDto>
    {
        public string Name { get; set; }

        internal IFileStream CountryImage;

        public void AddCountryImage(IFileStream fileStream)
        {
            CountryImage = fileStream;
        }
    }

    public class CreateCountryCommandHandler : IRequestHandlerWrapper<CreateCountryCommand, CountryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CountryDto>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Country
            {
                Name = request.Name,
            };

            //entity.DomainEvents.Add(new CityCreatedEvent(entity));

            await _context.Countries.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CountryDto>(entity));
        }
    }
}
