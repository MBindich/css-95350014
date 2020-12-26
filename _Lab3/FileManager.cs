using System.Text;
using System.IO;
using System.IO.Compression;

namespace SharpLab3
{
    class FileManager
    {
        private FileManagerOptions options;
        private FileInfo file;
        private bool isArchived = false;
        private bool isEncrypted = false;
        private string archiveDirectory;
        private string targetDirectory;
        private string sourceDirectory;

        public FileManager(FileManagerOptions options, FileInfo file)
        {
            this.options = options;
            this.file = file;
            archiveDirectory = options.StorageOptions.ArchiveDirectory;
            targetDirectory = options.StorageOptions.TargetDirectory;
            sourceDirectory = options.StorageOptions.SourceDirectory;
        }

        public void Archive()
        {
            if (options.EncryptOptions.ToArchive)
            {
                Directory.CreateDirectory($@"{archiveDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\" +
                     $@"{file.LastWriteTime.Day} day\");
                string source = file.FullName;
                string target = $@"{archiveDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                    $@"{file.Name.Substring(0, file.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.zip";

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

                isArchived = true;
            }
        }

        public void UnArchive()
        {
            if (isArchived)
            {
                string source = $@"{archiveDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                    $@"{file.Name.Substring(0, file.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.zip";
                string target = $@"{sourceDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                    $@"{file.Name.Substring(0, file.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.txt";

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
        }

        public void Encrypt()
        {
            if (options.EncryptOptions.ToEncrypt)
            {
                Directory.CreateDirectory($@"{targetDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\" +
                   $@"{file.LastWriteTime.Day} day\");
                string source = file.FullName;
                string target = $@"{targetDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                    $@"{file.Name.Substring(0, file.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.txt";

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

                isEncrypted = true;
            }
        }

        public void Decrypt()
        {
            if (isEncrypted)
            {
                string source = $@"{targetDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                    $@"{file.Name.Substring(0, file.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.txt";
                string target = $@"{sourceDirectory}\{file.LastWriteTime.Year} year\{file.LastWriteTime.Month} month\{file.LastWriteTime.Day} day\" +
                    $@"{file.Name.Substring(0, file.Name.Length - 4)}_{file.LastWriteTime.Hour}_{file.LastWriteTime.Minute}_{file.LastWriteTime.Second}.txt";
                
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
}
