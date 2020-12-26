using System;

namespace SharpLab3
{
    [Serializable]
    public class EncryptOptions
    {
        public bool ToEncrypt { get; set; } = false;
        public bool ToArchive { get; set; } = false;
        
        public EncryptOptions()
        {

        }
    }
}
