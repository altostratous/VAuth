using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAuth
{
    /// <summary>
    /// The model used for Voice authentication.
    /// </summary>
    public class CodeBook
    {
        // list of identities stored in the model
        private Dictionary<string, VoiceIdentity> identities;

        // the speech recognizer used by this model to authenticate input voice
        private ISpeechRecognizer speechRecognizer;

        /// <summary>
        /// Creates a new CoodeBook using a speech recognizer.
        /// </summary>
        /// <param name="speechRecognizer">The speech recognizer used by the CoodBook to authenticate</param>
        public CodeBook(ISpeechRecognizer speechRecognizer)
        {
            // initialize identities
            this.identities = new Dictionary<string, VoiceIdentity>();
            // just set the recognizer
            this.speechRecognizer = speechRecognizer;
        }

        /// <summary>
        /// Adds a voice file to the model labeled by the tag, supported with password
        /// </summary>
        /// <param name="username">The label used for the identity of the input voice</param>
        /// <param name="waveFileName">The path to the wave file as the voice input</param>
        /// <param name="password">The supporting password with this identity. Please note that using different password with the same identity leads the prior passwords be owerriten</param>
        /// <param name="workingDirectory">The working directory to store the MFCC file generated from the input file</param>
        public void Add(string username, string waveFileName, string password = "", string workingDirectory = ".")
        {
            // calculate the destination file name for the MFCC file, include time to prevent owerwriting
            string destination = Path.Combine(workingDirectory, username, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-" + DateTime.Now.Millisecond));
            // if the working directory does not exist
            if (!Directory.Exists(workingDirectory))
                // create it
                Directory.CreateDirectory(workingDirectory);
            // if the directory for the username does not exist
            if (!Directory.Exists(Path.Combine(workingDirectory, username)))
                // create it
                Directory.CreateDirectory(username);
            // create MFCC model from the input voice
            PwdManagement.Voice.MFCC.getMfcc(waveFileName, destination);
            // save the password
            File.WriteAllText(Path.Combine(workingDirectory, username) + ".password", password);
            // if the label(username) does not already exist
            if (!identities.ContainsKey(username))
            {
                // create it
                identities.Add(username, new VoiceIdentity(username, password, new List<string>()));
            }
            // add the MFCC model file to the identity model
            identities[username].Add(destination);
            // overwrite the password
            identities[username].Password = password;
        }

        /// <summary>
        /// Adds an MFCC file to the model directly (It means it does not calculate MFCC internally. This method is useful when you want to recover the model from files.)
        /// </summary>
        /// <param name="username">the identity label (username)</param>
        /// <param name="mfccFileNames">list of MFCC files associated with the identity label (username)</param>
        /// <param name="password">the passowrd associated with the identity</param>
        /// <param name="workingDirectory">the working direcotry to save the passwords in</param>
        public void AddToModel(string username, string[] mfccFileNames, string password = "", string workingDirectory = ".")
        {
            // it is almost like the method above
            string destination = Path.Combine(workingDirectory, username, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-" + DateTime.Now.Millisecond));
            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            if (!Directory.Exists(username))
                Directory.CreateDirectory(username);
            if (!identities.ContainsKey(username))
            {
                identities.Add(username, new VoiceIdentity(username, password, new List<string>()));
            }
            foreach (string mfccFileNaem in mfccFileNames)
                identities[username].Add(mfccFileNaem);
            // save the password
            File.WriteAllText(Path.Combine(workingDirectory, username) + ".password", password);
        }

        /// <summary>
        /// Find the narest voice with the same password told in the voice file
        /// </summary>
        /// <param name="waveFileName">the path to input wave file</param>
        /// <returns></returns>
        public VoiceIdentity Identify(string waveFileName)
        {
            // obtain the password candidates told in the input voice
            HashSet<string> passwords = new HashSet<string>(speechRecognizer.SpeechToText(waveFileName));

            // initially set the result to null
            VoiceIdentity nearest = null;
            // find the identity with the same password and least distance
            double leastDistance = Double.MaxValue;
            foreach (VoiceIdentity identity in identities.Values)
            {
                if (!passwords.Contains(identity.Password))
                    continue;
                // calculate input file distance from the identity
                double distance = identity.Distance(waveFileName);
                Console.WriteLine(distance);
                if (distance < leastDistance)
                {
                    nearest = identity;
                    leastDistance = distance;
                }
            }
            return nearest;
        }
    }
}
