using Kasir.Domain.Entities;
using Mapster;

namespace Kasir.Application.Dto
{
    public class PushTokenDto : IRegister 
    {
        public string Token { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PushToken, PushTokenDto>();
        }
    }
}
