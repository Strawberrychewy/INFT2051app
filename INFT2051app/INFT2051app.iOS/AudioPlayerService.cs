// All of this code comes from 
// https://www.godo.dev/tutorials/xamarin-forms-play-audio/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPlayer;
using System.IO;
using INFT2051app.iOS.Services;
using INFT2051app.Services;
using AVFoundation;
using Xamarin.Forms;

using Foundation;
using UIKit;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace INFT2051app.iOS.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private AVAudioPlayer _audioPlayer = null;
        public Action onFinishedPlaying { get; set; }

        public AudioPlayerService()
        {
        }

        public void Play(string pathToAudioFile)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.FinishedPlaying -= Player_FinishedPlaying;
                _audioPlayer.Stop();
            }

            string localUrl = pathToAudioFile;
            _audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(localUrl));
            _audioPlayer.FinishedPlaying += Player_FinishedPlaying;
            _audioPlayer.Play();
        }

        private void Player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            onFinishedPlaying?.Invoke();
            
        }

        public void Pause()
        {
            _audioPlayer?.Pause();
        }

        public void Play()
        {
            _audioPlayer?.Play();
            //onFinishedPlaying.Invoke();
        }
    }
}