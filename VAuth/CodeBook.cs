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

        public CodeBook(Dictionary<string, VoiceIdentity> identities) : this()
        {
            this.identities = identities;
        }

        public void Add(string username, string waveFileName, string workingDirectory = ".")
        {
            string destination = Path.Combine(workingDirectory, username, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-" + DateTime.Now.Millisecond));
            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            if (!Directory.Exists(username))
                Directory.CreateDirectory(username);
            PwdManagement.Voice.MFCC.getMfcc(waveFileName, destination);
            if (!identities.ContainsKey(username))
            {
                identities.Add(username, new VoiceIdentity(username, "", new List<string>()));
            }
            identities[username].Add(destination);
        }

        public void AddToModel(string username, string[] mfccFileNames, string workingDirectory = ".")
        {
            string destination = Path.Combine(workingDirectory, username, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-" + DateTime.Now.Millisecond));
            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            if (!Directory.Exists(username))
                Directory.CreateDirectory(username);
            if (!identities.ContainsKey(username))
            {
                identities.Add(username, new VoiceIdentity(username, "", new List<string>()));
            }
            foreach (string mfccFileNaem in mfccFileNames)
                identities[username].Add(mfccFileNaem);
        }

        public VoiceIdentity Identify(string waveFileName)
        {
            VoiceIdentity nearest = null;
            double leastDistance = Double.MaxValue;
            foreach (VoiceIdentity identity in identities.Values)
            {
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

        public void Balance()
        {
            int minimumSize = Int32.MaxValue;
            foreach(VoiceIdentity identity in identities.Values)
            {
                if (identity.Size < minimumSize)
                    minimumSize = identity.Size;
            }
            foreach (VoiceIdentity identity in identities.Values)
            {
                identity.Cut(minimumSize);
            }
        }

        public void Normalize()
        {
            foreach (VoiceIdentity identity in identities.Values)
                identity.Normalize();
        }
    }
}
