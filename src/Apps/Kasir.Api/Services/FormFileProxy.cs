using Kasir.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Api.Services
{
    public class FormFileProxy : IFileStream
    {
        private readonly IFormFile file;

        public FormFileProxy(IFormFile file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public string Name => file.Name;

        public string FileName => file.FileName;

        public string ContentType => file.ContentType;

        public long Length => file.Length;

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            await file.CopyToAsync(target, cancellationToken);
        }
    }
}
