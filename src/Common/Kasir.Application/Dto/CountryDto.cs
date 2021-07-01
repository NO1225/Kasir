using System;
using System.Collections.Generic;
using Kasir.Domain.Entities;
using Mapster;

namespace Kasir.Application.Dto
{
    public class CountryDto : IRegister 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreateDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Country, CountryDto>();
        }
    }    
}
