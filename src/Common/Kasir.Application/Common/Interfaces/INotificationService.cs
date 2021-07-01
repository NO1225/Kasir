using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task CleanTicketsAsync(CancellationToken cancellationToken = default);
        Task SendNotifiactionAsync(string title, string body, object content = null, CancellationToken cancellationToken = default);
    }
}
