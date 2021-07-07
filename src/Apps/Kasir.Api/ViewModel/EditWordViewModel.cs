using Kasir.Application.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class EditWordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الاسم الافتراضي")]
        public string Name { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<WordCountryDto> WordCountryDtos { get; set; }

        [Display(Name = "الصورة الافتراضية")]
        public IFormFile WordImage { get; set; }
    }
}
