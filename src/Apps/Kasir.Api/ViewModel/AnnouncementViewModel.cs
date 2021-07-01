using System;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class AnnouncementViewModel
    {
        [Display(Name = "الوقت")]
        public DateTime? CreatedAt { get; set; }


        [Display(Name = "العنوان")]
        public string Title { get; set; }

        [Display(Name = "التفاصيل")]
        public string Body { get; set; }
    }
}
