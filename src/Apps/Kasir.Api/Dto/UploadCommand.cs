using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Dto
{
    public class UploadCommand
    {
        public IFormFile File { get; set; }
    }
}
