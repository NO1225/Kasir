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

        public string Title { get; set; }

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
                .Include(c => c.WordImages)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            if (!string.IsNullOrEmpty(request.Title))
                entity.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Information))
                entity.Information = request.Information;

            if (request.WordLanguageDtos != null && request.WordLanguageDtos.Count > 0)
            {
                foreach (var cl in entity.WordLanguages)
                {
                    var dto = request.WordLanguageDtos.FirstOrDefault(cldd => cldd.LanguageId == cl.LanguageId);
                    if (dto != null)
                    {
                        cl.Title = dto.Title;
                        cl.Name = dto.Name;
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

            if (request.WordImageDtos != null && request.WordImageDtos.Count > 0)
            {
                foreach (var wi in entity.WordImages)
                {
                    var dto = request.WordImageDtos.FirstOrDefault(cldd => cldd.CountryId == wi.CountryId);
                    if (dto != null)
                    {
                        if (dto.WordImage != null)
                        {
                            var wordImageRes = await mediator.Send(new UpdateWordCountryImageCommand
                            {
                                OldImageName = wi.ImageName,
                                WordImage = dto.WordImage,
                            });
                            if (wordImageRes.Succeeded == false)
                            {
                                logger.LogWarning("Image Upload Failed: " + wordImageRes.Error.Message);
                            }
                            else
                            {
                                wi.ImageName = wordImageRes.Data;
                            }
                        }
                    }
                }
                foreach (var wordImage in request.WordImageDtos.Where(wid=>entity.WordImages.Select(wi=>wi.CountryId).Contains(wid.CountryId)== false))
                {
                    if (wordImage.WordImage != null)
                    {
                        var wordImageRes = await mediator.Send(new UpdateWordCountryImageCommand
                        {
                            WordImage = wordImage.WordImage,
                        });
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
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<WordDto>(entity));
        }
    }
}
