using System.Threading.Tasks;
using Kasir.Application.Common.Models;

namespace Kasir.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
