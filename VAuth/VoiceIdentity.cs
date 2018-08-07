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

        public int Size { get { return mfccFiles.Count; } }

        public VoiceIdentity(string tag, string password, List<string> mfccFiles)
        {
            this.tag = tag;
            this.password = password;
            this.mfccFiles = mfccFiles;
        }

        internal void Add(string mfccFile)
        {
            mfccFiles.Add(mfccFile);
        }

        public double Distance(string fileName, string workingDirectory = ".")
        {
            string outputFileName = Path.Combine(workingDirectory, "temp.mfcc");
            PwdManagement.Voice.MFCC.getMfcc(fileName, outputFileName);
            double totalDistance = 0;
            foreach (string mfccFile in mfccFiles)
            { 
                double distance = Math.Pow(PwdManagement.Voice.Dtw.getDtw(mfccFile, outputFileName), 1);
                totalDistance += distance;
            }
            return totalDistance / mfccFiles.Count;
        }
    }
}