using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAuth
{
    public class CodeBook
    {
        private Dictionary<string, VoiceIdentity> identities;

        public CodeBook()
        {
            this.identities = new Dictionary<string, VoiceIdentity>();
        }

        public CodeBook(Dictionary<string, VoiceIdentity> identities)
        {
            this.identities = identities;
        }

        public void Add(string username, string waveFileName, string workingDirectory = ".")
        {
            string destination = Path.Combine(workingDirectory, username, DateTime.Now.Millisecond.ToString());
            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            if (!Directory.Exists(username))
                Directory.CreateDirectory(username);
            PwdManagement.Voice.MFCC.getMfcc("temp.wav", destination);
            if (!identities.ContainsKey(username))
            {
                identities.Add(username, new VoiceIdentity(username, "", new List<string>()));
            }
            identities[username].Add(waveFileName);
        }

        public void AddToModel(string username, string[] mfccFileNames, string workingDirectory = ".")
        {
            string destination = Path.Combine(workingDirectory, username, DateTime.Now.Millisecond.ToString());
            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            if (!Directory.Exists(username))
                Directory.CreateDirectory(username);
            if (!identities.ContainsKey(username))
            {
                identities.Add(username, new VoiceIdentity(username, "", new List<string>()));
            }
            foreach (string waveFileName in mfccFileNames)
                identities[username].Add(waveFileName);
        }

        public VoiceIdentity Identify(string waveFileName)
        {
            VoiceIdentity nearest = null;
            double leastDistance = Double.MaxValue;
            foreach (VoiceIdentity identity in identities.Values)
            {
                double distance = identity.Distance(waveFileName);
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
