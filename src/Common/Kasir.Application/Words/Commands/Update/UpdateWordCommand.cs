using Kasir.Application.Common.Exceptions;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Files.Commands;
using Kasir.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Words.Commands.Update
{
    public class UpdateWordCommand : IRequestWrapper<WordDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<WordCountryDto> WordCountryDtos { get; set; }

        internal IFileStream WordImage;

        public void AddWordImage(IFileStream fileStream)
        {
            WordImage = fileStream;
        }
    }

    public class UpdateWordCommandHandler : IRequestHandlerWrapper<UpdateWordCommand, WordDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateWordCommand> logger;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public UpdateWordCommandHandler(IApplicationDbContext context,
            ILogger<UpdateWordCommand> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.logger = logger;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WordDto>> Handle(UpdateWordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Words
                .Include(c => c.WordLanguages)
                .Include(c => c.WordCountries)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;


            if (request.WordLanguageDtos != null && request.WordLanguageDtos.Count > 0)
            {
                foreach (var cl in entity.WordLanguages)
                {
                    var dto = request.WordLanguageDtos.FirstOrDefault(cldd => cldd.LanguageId == cl.LanguageId);
                    if (dto != null)
                    {
                        cl.Title = dto.Title;
                        cl.Information = dto.Information;
                    }
                }
            }

            if (request.WordImage != null)
            {
                var res = await mediator.Send(new UpdateWordCountryImageCommand
                {
                    WordImage = request.WordImage,
                    OldImageName = entity.ImageName,
                });

                if (res.Succeeded == false)
                {
                    return new ServiceResult<WordDto>(res.Error);
                }

                entity.ImageName = res.Data;
            }

            if (request.WordCountryDtos != null && request.WordCountryDtos.Count > 0)
            {
                foreach (var wc in entity.WordCountries)
                {
                    var dto = request.WordCountryDtos.FirstOrDefault(cldd => cldd.CountryId == wc.CountryId);
                    if (dto != null)
                    {
                        if (dto.Checked != "on")
                        {
                           _context.WordCountries.Remove(wc);
                        }
                    }
                }
                foreach (var wordCountry in request.WordCountryDtos.Where(wcd=>wcd.Checked == "on"
                && entity.WordCountries.Select(wi=>wi.CountryId).Contains(wcd.CountryId)== false))
                {
                    entity.WordCountries.Add(new WordCountry
                    {
                        CountryId = wordCountry.CountryId,
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<WordDto>(entity));
        }
    }
}
