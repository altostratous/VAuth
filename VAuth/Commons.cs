using NAudio.Wave;
using System;
using System.IO;

namespace VAuth
{
    public class Commons
    {
        public static void SpeechToText(string waveFileName)
        {
            throw new NotImplementedException();
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