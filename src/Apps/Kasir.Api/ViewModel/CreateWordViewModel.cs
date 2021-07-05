using Kasir.Application.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class CreateWordViewModel
    {
        [Display(Name = "العنوان الافتراضي")]
        public string Title { get; set; }

        [Display(Name = "الكلمة الافتراضية")]
        public string Name { get; set; }

        [Display(Name = "التفاصيل الافتراضية")]
        public string Information { get; set; }

        public List<WordLanguageDto> WordLanguageDtos { get; set; }

        public List<CreateWordImage> WordImageDtos { get; set; }

        [Display(Name = "Default Image")]
        public IFormFile WordImage { get; set; }
    }
}
