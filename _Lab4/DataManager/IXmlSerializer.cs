using System;

namespace DataManager
{
    interface IXmlSerializer
    {
        void XmlSerialize<T>(string filePath, object obj);
    }
}
