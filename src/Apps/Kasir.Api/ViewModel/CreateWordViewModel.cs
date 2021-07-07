using Kasir.Application.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class CreateWordViewModel
    {

        [Display(Name = "الكلمة")]
        public string Name { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<WordCountryDto> WordCountryDtos { get; set; }

        [Display(Name = "Default Image")]
        public IFormFile WordImage { get; set; }
    }
}
