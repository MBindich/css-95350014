using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace WinServLab2
{
    static class FileCoder
    {
        public static void Archive(string source, string target)
        {
            using (FileStream sourceStream = new FileStream(source, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(target))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        public static void UnArchive(string source, string target)
        {
            using (FileStream sourceStream = new FileStream(source, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(target))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
        }

        public static void EncryptTo(string source, string target)
        {
            using (StreamReader sr = new StreamReader(source, Encoding.Default))
            {
                using (StreamWriter sw = new StreamWriter(target, false, Encoding.ASCII))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        public static void DecryptTo(string source, string target)
        {
            using (StreamReader sr = new StreamReader(source, Encoding.ASCII))
            {
                using (StreamWriter sw = new StreamWriter(target, false, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
