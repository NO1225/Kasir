using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Dto;
using Kasir.Application.Files.Commands;
using Kasir.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Words.Commands.Create
{
    public class CreateWordCommand : IRequestWrapper<WordDto>
    {
        public string Name { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<WordCountryDto> WordCountriesDtos { get; set; }


        internal IFileStream WordImage;

        public void AddWordImage(IFileStream fileStream)
        {
            WordImage = fileStream;
        }
    }

    public class CreateWordCommandHandler : IRequestHandlerWrapper<CreateWordCommand, WordDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateWordCommand> logger;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public CreateWordCommandHandler(IApplicationDbContext context,
            ILogger<CreateWordCommand> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            this.logger = logger;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WordDto>> Handle(CreateWordCommand request, CancellationToken cancellationToken)
        {
            var entity = new Word
            {
                Name = request.Name,
                WordLanguages = request.WordLanguageDtos.Select(cl => new WordLanguage
                {
                    LanguageId = cl.LanguageId,
                    Title = cl.Title,
                    Information = cl.Information,
                }).ToList(),
                WordCountries = new List<WordCountry>()
            };

            if (request.WordImage != null)
            {

                var res = await mediator.Send(new AddWordCountryImageCommand { WordImage = request.WordImage });

                if (res.Succeeded == false)
                {
                    return new ServiceResult<WordDto>(res.Error);
                }

                entity.ImageName = res.Data;
            }

            foreach (var wordCountry in request.WordCountriesDtos)
            {
                if (wordCountry.Checked =="on")
                    continue;

                entity.WordCountries.Add(new WordCountry
                {
                    CountryId = wordCountry.CountryId,
                });
            }

            await _context.Words.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<WordDto>(entity));
        }
    }
}
