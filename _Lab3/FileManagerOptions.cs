using System;

namespace SharpLab3
{
    [Serializable]
    public class FileManagerOptions
    {
        public EncryptOptions EncryptOptions { get; set; }
        public StorageOptions StorageOptions { get; set; }

        public FileManagerOptions()
        {
            EncryptOptions = new EncryptOptions();
            StorageOptions = new StorageOptions();
        }
    }
}
