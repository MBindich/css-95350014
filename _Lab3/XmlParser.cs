using System;
using System.IO;
using System.Xml.Serialization;

namespace SharpLab3
{
    public class XmlParser : IConfigurationParser
    {
        public T Parse<T>(string xmlConfigFile) where T : new()
        {
            T options = new T();

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (FileStream fs = new FileStream(xmlConfigFile, FileMode.OpenOrCreate))
                {
                    options = (T)formatter.Deserialize(fs);
                }
                return options;
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
