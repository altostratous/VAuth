using System.Collections.Generic;

namespace VAuth
{
    class VoiceIdentity
    {
        public string tag;
        private string password;
        private List<string> mfccFiles;

        public VoiceIdentity(string tag, string password, List<string> mfccFiles)
        {
            this.tag = tag;
            this.password = password;
            this.mfccFiles = mfccFiles;
        }
    }
}