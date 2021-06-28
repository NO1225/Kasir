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

        public string Information { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<WordImageDto> WordImageDtos { get; set; }


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
                Information = request.Information,
                WordLanguages = request.WordLanguageDtos.Select(cl => new WordLanguage
                {
                    LanguageId = cl.LanguageId,
                    Name = cl.Name,
                    Information = cl.Information,
                }).ToList(),
                WordImages = new List<WordImage>()
            };

            var res = await mediator.Send(new AddWordCountryImageCommand { WordImage = request.WordImage });

            if (res.Succeeded == false)
            {
                return new ServiceResult<WordDto>(res.Error);
            }

            entity.ImageName = res.Data;

            foreach (var wordImage in request.WordImageDtos)
            {

                var wordImageRes = await mediator.Send(new AddWordCountryImageCommand { WordImage = wordImage.WordImage });

                if (wordImageRes.Succeeded == false)
                {
                    logger.LogWarning("Image Upload Failed: " + wordImageRes.Error.Message);
                }
                else
                {
                    entity.WordImages.Add(new WordImage
                    {
                        CountryId = wordImage.CountryId,
                        ImageName = wordImageRes.Data,
                    });
                }

            }

            await _context.Words.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<WordDto>(entity));
        }
    }
}
