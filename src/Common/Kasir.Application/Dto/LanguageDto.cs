using System;
using Kasir.Domain.Entities;
using Mapster;

namespace Kasir.Application.Dto
{
    public class LanguageDto : IRegister
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreateDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Language, LanguageDto>()
                .Map(dest=>dest.ImagePath,src=>src.ImageName)
                .Map(dest=>dest.CreateDate,src=>src.CreateDate);

            config.NewConfig<LanguageDto, Language>()
                .Map(dest => dest.ImageName, src => src.ImagePath)
                .Map(dest => dest.CreateDate, src => src.CreateDate);
        }
    }
}
