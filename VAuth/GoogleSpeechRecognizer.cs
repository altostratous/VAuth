﻿using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAuth
{
    public class GoogleSpeechRecognizer : ISpeechRecognizer
    {
        private SpeechClient speechClient;

        ///
        /// this can be "en", "fa", ...
        /// 
        private string languageCode;

        /// <summary>
        /// Creates new Speech recognizer from appropriate google credentials and language code
        /// </summary>
        /// <param name="credential">Credential with access to Google Cloud Speech API</param>
        /// <param name="languageCode">Language for speech recognition. Default is 'fa'.</param>
        public GoogleSpeechRecognizer(GoogleCredential credential, string languageCode = "fa")
        {
            // create channel to connect to Google API
            var channel = new Grpc.Core.Channel(
                SpeechClient.DefaultEndpoint.Host,
                credential.ToChannelCredentials()
            );

            // create speech client for future use
            this.speechClient = SpeechClient.Create(channel);
            // set the language code to use when calling Google API
            this.languageCode = languageCode;
        }

        /// <summary>
        /// This is implementation of SpeechToText function using Google Cloud Speech API, for more information see ISpeechRecognizer.SpeechToText
        /// </summary>
        /// <param name="waveFileName">Path to a wav voice file containing the voice to be recognized.</param>
        /// <returns></returns>
        public List<string> SpeechToText(string waveFileName)
        {
            // call Google API
            var response = speechClient.Recognize(
                // with this specific configuration
                new RecognitionConfig()
                {
                    // specifying that the file format is linear (wave),
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    // and specifying the language of the voice
                    LanguageCode = languageCode,
                },
                // pass the audio file to be recognized 
                RecognitionAudio.FromFile(waveFileName)
            );

            // the collection to put results inside
            List<string> results = new List<string>();

            // for each result returened from Google
            foreach (var result in response.Results)
            {
                // for each text alternation
                foreach (var alternative in result.Alternatives)
                {
                    // add the text to results
                    results.Add(alternative.Transcript);
                }
            }
            // return the results
            return results;
        }
    }
}