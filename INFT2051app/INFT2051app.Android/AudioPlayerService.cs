// All of this code comes from 
// https://www.godo.dev/tutorials/xamarin-forms-play-audio/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Graphics;
using Android.Content;

using Android.Media;
using INFT2051app.Droid.Services;
using INFT2051app.Services;

using Xamarin.Forms;


[assembly: Dependency(typeof(AudioPlayerService))]
namespace INFT2051app.Droid.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private MediaPlayer _mediaPlayer;

        public Action onFinishedPlaying { get; set; }

        public AudioPlayerService()
        {
        }

        [Obsolete]
        public void Play(string pathToSoundName)
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Completion -= MediaPlayer_Completion;
                _mediaPlayer.Stop();
            }

            var fullPath = pathToSoundName;

            global::Android.Content.Res.AssetFileDescriptor afd = null;
            
            try
            {
                afd = Forms.Context.Assets.OpenFd(fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error openfd: " + ex);
            }
            if (afd != null)
            {
                System.Diagnostics.Debug.WriteLine("Length " + afd.Length);
                if (_mediaPlayer == null)
                {
                    _mediaPlayer = new MediaPlayer();
                    _mediaPlayer.Prepared += (sender, args) =>
                    {
                        _mediaPlayer.Start();
                        _mediaPlayer.Completion += MediaPlayer_Completion;
                    };
                }

                _mediaPlayer.Reset();
                _mediaPlayer.SetVolume(1.0f, 1.0f);

                _mediaPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                _mediaPlayer.PrepareAsync();
            }
        }

        void MediaPlayer_Completion(object sender, EventArgs e)
        {
            onFinishedPlaying?.Invoke();
            
            //change this I dont want it to stop
        }

        public void Pause()
        {
            _mediaPlayer?.Pause();
        }

        public void Play()
        {
            _mediaPlayer?.Start();
            //onFinishedPlaying.Invoke();
        }
    }
}