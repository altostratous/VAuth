using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAuth;

namespace VAuthDemo
{
    public partial class Main : Form
    {
        private CodeBook codeBook = new CodeBook();
        private WaveIn recorder;
        private BufferedWaveProvider bufferedWaveProvider;
        private SavingWaveProvider savingWaveProvider;
        private WaveOut player;
        private double authenticationThreshold = 8;
        public Main()
        {
            InitializeComponent();

            UserCredential credential;
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "951192255070-c1393soeosq20o30nua8o734cjc8p3vm.apps.googleusercontent.com",
                    ClientSecret = "R8bxbr1XQVLCAuU8vtl6Hy-u"
                },
                new[] { "https://www.googleapis.com/auth/cloud-platform" },
                "user", CancellationToken.None, new FileDataStore("VAuth.Credentials")
            ).Result;


            foreach (string folderName in Directory.GetDirectories("."))
            {
                if (folderName == ".\\x86" || folderName == ".\\x64") continue;
                string username = folderName;
                codeBook.AddToModel(Path.GetFileName(username), Directory.GetFiles(username));
            }

        }

        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }


        private void recordButton_MouseDown(object sender, MouseEventArgs e)
        {
            // set up the recorder
            recorder = new WaveIn() { DeviceNumber = comboWaveInDevice.SelectedIndex - 1 };
            recorder.DataAvailable += RecorderOnDataAvailable;

            // set up our signal chain
            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            savingWaveProvider = new SavingWaveProvider(bufferedWaveProvider, "temp.wav");

            // set up playback
            player = new WaveOut();
            player.Init(savingWaveProvider);

            // begin playback & record
            // player.Volume = 0;
            player.Play();
            recorder.StartRecording();

            // tell the user
            recordButton.Text = "Recording ...";

        }

        private void recordButton_MouseUp(object sender, MouseEventArgs e)
        {
            // stop recording
            recorder.StopRecording();
            // stop playback
            player.Stop();
            // finalise the WAV file
            savingWaveProvider.Dispose();

            // tell the user
            recordButton.Text = "Hold To Record";

            // perform google speech to text
            // VAuth.Commons.SpeechToText("temp.wav");
        }

        private void deviceTimer_Tick(object sender, EventArgs e)
        {
            try {
                var devices = Enumerable.Range(-1, WaveIn.DeviceCount + 1).Select(n => WaveIn.GetCapabilities(n)).ToArray();

                if (devices.Length != comboWaveInDevice.Items.Count)
                {
                    comboWaveInDevice.DataSource = devices;
                    comboWaveInDevice.DisplayMember = "ProductName";
                }
            } catch (Exception) { }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer();
            simpleSound.SoundLocation = "temp.wav";
            simpleSound.Play();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            codeBook.Add(username, "temp.wav");
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            VoiceIdentity nearestIdentity = codeBook.Identify("temp.wav");
            double distance = nearestIdentity.Distance("temp.wav");
            if (distance < authenticationThreshold && new AudioFileReader("temp.wav").TotalTime < new TimeSpan(0, 0, 5))
                MessageBox.Show("Welcome, " + nearestIdentity.Tag + "!");
            else
                MessageBox.Show("Authentication Failed!");
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void testVoxCeleb_Click(object sender, EventArgs e)
        {
            string voxPath = voxPathTextBox.Text;
            int miss = 0, hit = 0;
            foreach (string celebrityFolder in Directory.GetDirectories(voxPath))
            {
                foreach (string referenceFolder in Directory.GetDirectories(Path.Combine(voxPath, celebrityFolder)))
                {
                    foreach (string piece in Directory.GetFiles(Path.Combine(celebrityFolder, referenceFolder)))
                    {
                        string sentenceWaveFileName = Path.Combine(referenceFolder, piece);
                        using (AudioFileReader reader = new AudioFileReader(sentenceWaveFileName)) {
                            var totalTime = reader.TotalTime;
                            if (totalTime > new TimeSpan(0, 0, 5))
                            {
                                continue;
                            }
                        }
                        VoiceIdentity candidate = codeBook.Identify(sentenceWaveFileName);
                        if (candidate.Distance(sentenceWaveFileName) < authenticationThreshold)
                        {
                            miss++;
                        }
                        else
                        {
                            hit++;
                        }
                    }
                }
            }


            MessageBox.Show("Accuracy: " + 100 * (hit / (double)(hit + miss)) + "%");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}