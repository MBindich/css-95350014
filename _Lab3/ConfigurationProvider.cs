using System;

namespace SharpLab3
{
    class ConfigurationProvider
    {
        private IConfigurationParser configurationParser;
        public ConfigurationProvider(IConfigurationParser configurationParser)
        {
            this.configurationParser = configurationParser;
        }

        public T GetOptions<T>(string fileConfig) where T : new()
        {
            return configurationParser.Parse<T>(fileConfig);
        }
    }
}
