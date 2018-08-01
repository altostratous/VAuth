using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using NAudio.Wave;
using System;
using System.IO;

namespace VAuth
{
    public class Commons
    {
        public static void SpeechToText(string waveFileName)
        {
            var speech = SpeechClient.Create();

            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = WavSampleRate(waveFileName),
                LanguageCode = "fa",
            }, RecognitionAudio.FromFile(waveFileName));
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }
        }

        private static int WavSampleRate(string waveFileName)
        {
            int sampleRate = -1;
            using (var reader = new WaveFileReader(waveFileName))
            {
                sampleRate = reader.WaveFormat.SampleRate * 8;
            }
            return sampleRate;
        }
    }
}