// All of this code comes from 
// https://www.godo.dev/tutorials/xamarin-forms-play-audio/

using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app.Services
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
        Action onFinishedPlaying { get; set; }
    }
}
