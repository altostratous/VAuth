using System;
using System.Collections.Generic;
using System.IO;

namespace VAuth
{
    public class VoiceIdentity
    {
        private string tag;
        private string password;
        private List<string> mfccFiles;

        public string Tag { get { return tag; } set { tag = value; } }

        public VoiceIdentity(string tag, string password, List<string> mfccFiles)
        {
            this.tag = tag;
            this.password = password;
            this.mfccFiles = mfccFiles;
        }

        internal void Add(string wavFileName)
        {
            mfccFiles.Add(wavFileName);
        }

        public double Distance(string waveFileName, string workingDirectory = ".")
        {
            string outputFileName = Path.Combine(workingDirectory, waveFileName + ".mfcc");
            PwdManagement.Voice.MFCC.getMfcc(waveFileName, outputFileName);
            double distance = 0;
            foreach(string mfccFile in mfccFiles) 
                distance += PwdManagement.Voice.Dtw.getDtw(mfccFile, outputFileName);
            return distance / mfccFiles.Count;
        }
    }
}