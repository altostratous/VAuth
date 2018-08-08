using System;
using System.Collections.Generic;
using System.IO;

namespace VAuth
{
    public class VoiceIdentity
    {
        // to have the username 
        private string tag;
        // to store the password
        private string password;
        // to store list of MFCC files for the user
        private List<string> mfccFiles;

        /// <summary>
        /// The label of this identity. It can be used as username.
        /// </summary>
        public string Tag { get { return tag; } set { tag = value; } }

        /// <summary>
        /// The number of MFCC files for this identity.
        /// </summary>
        public int Size { get { return mfccFiles.Count; } }

        /// <summary>
        /// The password set for this identity
        /// </summary>
        public string Password {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Creates new Voice Identity
        /// </summary>
        /// <param name="tag">The identity label</param>
        /// <param name="password">The password</param>
        /// <param name="mfccFiles">List of MFCC files associated with the voice identity</param>
        public VoiceIdentity(string tag, string password, List<string> mfccFiles)
        {
            // set the feilds
            this.tag = tag;
            this.password = password;
            this.mfccFiles = mfccFiles;
        }

        /// <summary>
        /// Adds an MFCC file as a valid voice for the identity
        /// </summary>
        /// <param name="mfccFile"></param>
        internal void Add(string mfccFile)
        {
            mfccFiles.Add(mfccFile);
        }

        /// <summary>
        /// Calculates mean distance of the voice from this voice identity
        /// </summary>
        /// <param name="fileName">Path to the wave file containing the voice to be compared to this identity</param>
        /// <param name="workingDirectory">The working directory for VAuth library. It is used to create temporary MFCC file to compare to other MFCC files from the model</param>
        /// <returns></returns>
        public double Distance(string fileName, string workingDirectory = ".")
        {
            // set the temporary MFCC output file name to create MFCC for the input voice
            string outputFileName = Path.Combine(workingDirectory, "temp.mfcc");
            // calculate MFCC for the input voice
            PwdManagement.Voice.MFCC.getMfcc(fileName, outputFileName);

            // calculate and return mean of distance
            double totalDistance = 0;
            foreach (string mfccFile in mfccFiles)
            { 
                double distance = PwdManagement.Voice.Dtw.getDtw(mfccFile, outputFileName);
                totalDistance += distance;
            }
            return totalDistance / mfccFiles.Count;
        }
    }
}