using System;
using System.IO;
using System.Xml.Serialization;

namespace DataManager
{
    public class XmlDbSerializer : IXmlSerializer
    {
        public void XmlSerialize<T>(string filePath, object obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, obj);
            }
        }
    }
}
