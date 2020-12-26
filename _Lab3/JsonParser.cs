using System;
using System.IO;
using System.Text.Json;

namespace SharpLab3
{
    class JsonParser : IConfigurationParser
    {
        public T Parse<T>(string jsonConfigFile) where T : new()
        {
            T options = new T();

            try
            {
                string jsonText = File.ReadAllText(jsonConfigFile);
                JsonDocument document = JsonDocument.Parse(jsonText);
                JsonElement property = document.RootElement.GetProperty(typeof(FileManagerOptions).Name);

                if (typeof(T) != typeof(FileManagerOptions))
                {
                    property = property.GetProperty(typeof(T).Name);
                }
                options = JsonSerializer.Deserialize<T>(property.GetRawText());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errorlog.txt");
                using (StreamWriter sr = new StreamWriter(path, true))
                {
                    sr.Write(exception.Message);
                }
            }

            return options;
        }
    }
}
