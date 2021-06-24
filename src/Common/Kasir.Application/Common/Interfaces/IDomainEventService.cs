using System.Threading.Tasks;
using Kasir.Domain.Common;

namespace Kasir.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
