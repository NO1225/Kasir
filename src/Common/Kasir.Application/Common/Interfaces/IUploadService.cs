using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Common.Interfaces
{
    public interface IUploadService
    {
        Task UploadAsync(IFileStream file, CancellationToken cancellationToken = default);
    }
}
