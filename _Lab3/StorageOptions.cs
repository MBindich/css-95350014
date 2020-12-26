using System;

namespace SharpLab3
{
    [Serializable]
    public class StorageOptions
    {
        public string SourceDirectory { get; set; } = $@"‪G:\WorkDefault";
        public string TargetDirectory { get; set; } = $@"‪G:\WorkDefault\Encrypted";
        public string ArchiveDirectory { get; set; } = $@"‪G:\WorkDefault\Archived";

        public StorageOptions()
        {

        }
    }
}
