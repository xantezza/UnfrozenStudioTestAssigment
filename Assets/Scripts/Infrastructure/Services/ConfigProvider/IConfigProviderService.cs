using Configs;

namespace Infrastructure.Services.ConfigProvider
{
    public interface IConfigProviderService : IService
    {
        public Config Config { get; }
    }
}