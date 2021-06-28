using Microsoft.AspNetCore.Http;

namespace Kasir.Api.ViewModel
{
    public class CreateWordImage
    {
        public int CountryId { get; set; }

        public IFormFile WordImage { get; set; }
    }
}
