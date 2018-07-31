using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAuth
{
    class CodeBook
    {
        private List<VoiceIdentity> identities;

        public CodeBook(List<VoiceIdentity> identities)
        {
            this.identities = identities;
        }
    }
}
