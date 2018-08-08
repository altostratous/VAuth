# VAuth
Voice Authentication using Google Cloud Speech Recognition and https://github.com/Featherlet/VoiceprintRecognition

# Projects
## VAuth
A C# class library project to wrap https://github.com/Featherlet/VoiceprintRecognition which does not stick to object oriented paradigm. It also contains straight forward classes to use this type of authentication in your application.

## VAuthDemo
A simple windows forms application to test this authentication method.

## VAuthTest
This project contains tests for the library if it works.

# Getting Started
## Prerequisites
*  Visual Studio 2015 or higher,
*  internet connection,
*  putting the Google client secret file instead of the `client.json` file, providing your own application ID and secret obtained from Google developer console.

## Running the Demo Project:
Go through these steps to run the demo:
*  Clone the project using `git clone https://github.com/altostratous/VAuth` and change current directory to the repository,
*  open `VAuth/VAuth.sln`,
*  restore Nuget packages (it is usually done automatically while openning the solution),
*  set VAuthDemo as solutions startup project by right clicking on the project in solution explorer,
*  click on Start in Visual Studio,
*  fill the username feild,
*  use the record button and say what you want as the password (the application will fill the passwork field automatically). Currently it's set to recognize persian language `fa`,
*  make sure the password recognized is correct,
*  click on save to populate your authentication model (these saved records are reloaded on next executions of program and are stored in folders besides the executable),
*  do the last 3 steps as much as enough,
*  you can repeat the last 5 steps for different persons,
*  to test if the program works just use the record button to say the password and then click on authenticate button,
*  the expected behavior is to only authenticate the correct person telling the correct password, if the person is different or the password is wrong then the authentication should fail. 

## Running the Tests
The test project is like any other C# unit test project which you can run it.

## Using the VAuth Class Library
Once you added the built library to your project (using Nuget or building the source or adding the dll), you can use it this way:

    using VAuth;
    
    ...
    
    string googleClientID = "put your google client ID here"
    string googleClientSecret = "put your google client secret here"
    
    ISpeechRecogniser speechRecognizer = new GoogleSpeechRecognizer(googleClientID, googleClientSecret, "fa");
    CodeBook codeBook = new CodeBook(speechRecognizer);

    codeBook.Add("bob", "bob_pass_1.wav", "پسورد باب");
    codeBook.Add("bob", "bob_pass_2.wav", "پسورد باب");
    codeBook.Add("bob", "bob_pass_3.wav", "پسورد باب");
    
    var identity = codeBook.Identify("bob_pass_test.wav");
    if (identity != null)
        MessageBox.Show("Hi " + identity.Tag + "!");
    else
        MessageBox.Show("I don't know you!");

# Explanation
The voice recognition stuff is all done using Google Cloud Speech API and nothing else. We used an implementation of [this work](https://arxiv.org/pdf/1003.4083.pdf) for our voice identity system which is to use the DTW algorithm as distance over MFCC of a voice file. Impirically we found that it is better to use the mean distance for multiple files from the sample set.

Speech recognition systems usually recognize multiple results as alternatives for the spoken phrase. Now we are authenticating if any of the candidates are matched. 

We have a class named `VoiceIdentity` which contains the password, tag and MFCC file adresses of the samples from a person. These are kept inside a `CodeBook` object which handles search and controlling stuff. 

# Documentation
The code has rich inline documentation. Here are some persion videos on how the project is done and how to use it as well. We will add english videos on demand as well.

*  [Code Description](#)
*  [Demo](#)
