using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAuth
{
    /// <summary>
    /// The speech recognizer used by VAuth for authentication. VAuth provides a simple Google implementation named GoogleSpeechRecognizer
    /// You can integrate any speech recognizer with this interface to VAuth.
    /// </summary>
    public interface ISpeechRecognizer
    {
        List<string> SpeechToText(string waveFileName);
    }
}
