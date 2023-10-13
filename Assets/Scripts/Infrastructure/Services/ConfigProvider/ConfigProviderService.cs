using Configs;

namespace Infrastructure.Services.ConfigProvider
{
    public class ConfigProviderService : IConfigProviderService
    {
        public Config Config { get; }

        public ConfigProviderService(Config config)
        {
            Config = config;
        }
    }
}