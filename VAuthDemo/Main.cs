﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Cloud.Speech.V1;
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
using Grpc.Auth;

namespace VAuthDemo
{
    public partial class Main : Form
    {
        // the model containing people vocal MFCC model and password
        private CodeBook codeBook;

        // the speech recognizer to be used by the code book when identifying people
        private WindowsSpeechRecognizer speechRecognizer;

        // Copied from: https://markheath.net/post/how-to-record-and-play-audio-at-same
        private WaveIn recorder;
        private BufferedWaveProvider bufferedWaveProvider;
        private SavingWaveProvider savingWaveProvider;
        private WaveOut player;

        // The threshold under which voice is authenticated
        private double authenticationThreshold = 14;


        public Main()
        {
            // this is generated by VisualStudio
            InitializeComponent();

            // create new windows speech recognizer
            speechRecognizer = new WindowsSpeechRecognizer();

            // initialize codeBook with the speech recognizer
            codeBook = new CodeBook(speechRecognizer, authenticationThreshold);

            // for each folder next to the program, each folder contains MFCC files for a person
            foreach (string folderName in Directory.GetDirectories("."))
            {
                // obtail username from folder path, (folder path may start with .\ or sth like that)
                string username = Path.GetFileName(folderName);
                // add the saved user to code book
                codeBook.AddToModel(
                    username,
                    // pass all MFCC files inside the folder
                    Directory.GetFiles(folderName), 
                    // pass the password priorly saved for that user
                    File.ReadAllText(username + ".password")
                );
            }
        }

        // Copied from: https://markheath.net/post/how-to-record-and-play-audio-at-same
        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }


        // almost copied from: https://markheath.net/post/how-to-record-and-play-audio-at-same, it is triggered when record is pushed
        private void recordButton_MouseDown(object sender, MouseEventArgs e)
        {
            // set up the recorder
            recorder = new WaveIn() { DeviceNumber = comboWaveInDevice.SelectedIndex - 1 };
            recorder.DataAvailable += RecorderOnDataAvailable;

            // set up our signal chain
            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            savingWaveProvider = new SavingWaveProvider(
                bufferedWaveProvider, 
                // use temp.wav as temporary file name for saving audio through the project
                "temp.wav"
            );

            // set up playback
            player = new WaveOut();
            player.Init(savingWaveProvider);

            // begin playback & record
            player.Volume = 0;
            player.Play();
            recorder.StartRecording();

            // update record button status
            recordButton.Text = "Recording ...";

            // reset passwork field 
            passwordComboBox.Text = "";

        }

        // almost copied from: https://markheath.net/post/how-to-record-and-play-audio-at-same, it is triggered when record is released
        private void recordButton_MouseUp(object sender, MouseEventArgs e)
        {
            // stop recording
            recorder.StopRecording();
            // stop playback
            player.Stop();
            // finalise the WAV file
            savingWaveProvider.Dispose();

            // update record button status
            recordButton.Text = "Recognizing ...";
            // use this because we are in the UI thread
            Application.DoEvents();

            try {
                // perform google speech to text
                List<string> results = speechRecognizer.SpeechToText("temp.wav");
                // if there is any results 
                if (results.Count > 0)
                {
                    // show the first one
                    passwordComboBox.Items.Clear();
                    foreach (string result in results)
                        passwordComboBox.Items.Add(result);
                }
            } catch (Exception ex)
            {
                promptErrorAndExit(ex);
            }

            // update record button status
            recordButton.Text = "Hold To Record";
        }


        // this method is run every second to update microphone inputs in the UI
        private void deviceTimer_Tick(object sender, EventArgs e)
        {
            try {
                // this is copied from https://github.com/naudio/NAudio/tree/master/NAudioDemo/RecordingDemo
                var devices = Enumerable.Range(-1, WaveIn.DeviceCount + 1).Select(n => WaveIn.GetCapabilities(n)).ToArray();

                // if number of devices is changed 
                if (devices.Length != comboWaveInDevice.Items.Count)
                {
                    // set the source for device combo box
                    comboWaveInDevice.DataSource = devices;
                    comboWaveInDevice.DisplayMember = "ProductName";
                    // select the last one because the first one is usually windows mixer
                    comboWaveInDevice.SelectedIndex = comboWaveInDevice.Items.Count - 1;
                }
            } catch (Exception) { /* there was an expected exception just when you added or removed the device */ }
        }
        
        // add the user in the codebook, it also saves it in files for next runs
        private void saveButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            if (username == "")
            {
                MessageBox.Show("Username can't be empty!");
                return;
            }
            codeBook.Add(username, "temp.wav", passwordComboBox.Text);
        }

        // try to authenticate any user using the recorded voice
        private void authButton_Click(object sender, EventArgs e)
        {
            try {
                // ask codebook to identify the voice (it also checks the password, and threshold)
                VoiceIdentity nearestIdentity = codeBook.Identify("temp.wav");

                // if no user is found
                if (nearestIdentity == null)
                {
                    // tell the failure
                    MessageBox.Show("Authentication Failed!");
                    return;
                }
                
                MessageBox.Show("Welcome, " + nearestIdentity.Tag + "!");
            } catch (Exception ex)
            {
                promptErrorAndExit(ex);
            }
        }

        /// <summary>
        /// Prompts general error and exits the program. 
        /// </summary>
        /// <param name="ex">The exception to be logged and showed.</param>
        private void promptErrorAndExit(Exception ex)
        {
            MessageBox.Show(ex.Message);
            StreamWriter logger = new StreamWriter("error.log", true);
            logger.Write(ex.ToString());
            logger.WriteLine();
            logger.Close();
            Environment.Exit(1);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
