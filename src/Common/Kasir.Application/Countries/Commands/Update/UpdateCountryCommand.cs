using System.Threading;
using System.Threading.Tasks;
using Kasir.Application.Common.Exceptions;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Domain.Entities;
using MapsterMapper;

namespace Kasir.Application.Countries.Commands.Update
{
    public class UpdateCountryCommand : IRequestWrapper<CountryDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class UpdateCountryCommandHandler : IRequestHandlerWrapper<UpdateCountryCommand, CountryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CountryDto>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Countries.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CountryDto>(entity));
        }
    }
}
