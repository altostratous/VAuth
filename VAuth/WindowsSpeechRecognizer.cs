﻿using System;
using System.Collections.Generic;
using System.Speech.Recognition;

namespace VAuth
{
    public class WindowsSpeechRecognizer : ISpeechRecognizer
    {
        /// <summary>
        /// This is implementation of SpeechToText function using Windows speech recognition, for more information see ISpeechRecognizer.SpeechToText
        /// </summary>
        /// <param name="waveFileName">Path to a wav voice file containing the voice to be recognized.</param>
        /// <returns></returns>
        public List<string> SpeechToText(string waveFileName)
        {

            // the collection to put results inside
            List<string> results = new List<string>();

            try
            {
                // copied from here: https://stackoverflow.com/questions/25917966/speech-to-text-from-wav-file-c-sharp
                using (var sre = new SpeechRecognitionEngine())
                {
                    sre.SetInputToWaveFile(waveFileName);
                    sre.LoadGrammar(new DictationGrammar());

                    sre.BabbleTimeout = new TimeSpan(0, 0, 5);
                    sre.InitialSilenceTimeout = new TimeSpan(0, 0, 5);
                    sre.EndSilenceTimeout = new TimeSpan(0, 0, 5);
                    sre.EndSilenceTimeoutAmbiguous = new TimeSpan(0, 0, 5);

                    var result = sre.Recognize();
                    foreach (var alternative in result.Alternates)
                    {
                        results.Add(alternative.Text);
                    }
                }
                
            } catch (Exception ex)
            {
                // if something went wrong just rais an error
                throw new Exception("Could not perform Windows speech recognition.", ex);
            }

            // return the results
            return results;
        }
    }
}
