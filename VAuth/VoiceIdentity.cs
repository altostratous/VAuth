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
        private double standardDeviation;
        private double mean;

        public string Tag { get { return tag; } set { tag = value; } }

        public int Size { get { return mfccFiles.Count; } }

        public VoiceIdentity(string tag, string password, List<string> mfccFiles)
        {
            this.tag = tag;
            this.password = password;
            this.mfccFiles = mfccFiles;
            this.standardDeviation = 1;
            this.mean = 0;
        }

        internal void Add(string mfccFile)
        {
            mfccFiles.Add(mfccFile);
        }

        public double Distance(string fileName, bool calculateMFCC = true, string workingDirectory = ".")
        {
            string outputFileName;
            if (calculateMFCC)
            {
                outputFileName = Path.Combine(workingDirectory, "temp.mfcc");
                PwdManagement.Voice.MFCC.getMfcc(fileName, outputFileName);
            }
            else
            {
                outputFileName = fileName;
            }
            double totalDistance = 0;
            foreach (string mfccFile in mfccFiles)
            { 
                double distance = Math.Pow(PwdManagement.Voice.Dtw.getDtw(mfccFile, outputFileName), 2);
                // double distance = Math.Pow(PwdManagement.Voice.Dtw.getDtw(mfccFile, outputFileName), 1);
                totalDistance += distance;
            }
            //return totalDistance / standardDeviation;
            return Math.Sqrt(totalDistance) / mfccFiles.Count;
        }

        public void Cut(int minimumSize)
        {
            while (mfccFiles.Count > minimumSize)
                mfccFiles.RemoveAt(mfccFiles.Count - 1);
        }

        public void Normalize()
        {
            double sum = 0;
            double squaredSum = 0;
            foreach(string mfccFile in mfccFiles)
            {
                double distance = Distance(mfccFile, false);
                sum += distance;
                squaredSum += distance * distance;
            }
            this.mean = sum / mfccFiles.Count;
            this.standardDeviation = Math.Sqrt(mfccFiles.Count / (double)(mfccFiles.Count - 1) * ((squaredSum / mfccFiles.Count) - Math.Pow(sum / mfccFiles.Count, 2)));
            Console.WriteLine(standardDeviation);
        }
    }
}