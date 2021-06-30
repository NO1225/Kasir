using Kasir.Domain.Entities;
using Mapster;

namespace Kasir.Application.Dto
{
    public class AppInfoDto : IRegister
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AppInfo, AppInfoDto>();
        }
    }
}
