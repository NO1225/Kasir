using System.Threading.Tasks;

namespace Kasir.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtSecurityTokenAsync(string id);
    }
}
