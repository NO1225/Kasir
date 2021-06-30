using Kasir.Application.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Kasir.Api.ViewModel
{
    public class CreateWordViewModel
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Information { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<CreateWordImage> WordImageDtos { get; set; }

        public IFormFile WordImage { get; set; }
    }
}
