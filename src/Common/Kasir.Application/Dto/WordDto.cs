using System;
using Kasir.Domain.Entities;
using Mapster;

namespace Kasir.Application.Dto
{
    public class WordDto : IRegister
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Information { get; set; }

        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Word, WordDto>();
        }
    }
}
