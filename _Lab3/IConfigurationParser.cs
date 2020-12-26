using System;

namespace SharpLab3
{
    public interface IConfigurationParser
    {
        public T Parse<T>(string configFile) where T : new();
    }
}
