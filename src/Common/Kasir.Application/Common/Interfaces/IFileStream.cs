using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Common.Interfaces
{
    public interface IFileStream
    {
        string Name { get; }

        string FileName { get; }

        string ContentType { get; }

        long Length { get; }

        Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);

        Stream OpenReadStream();
    }
}
