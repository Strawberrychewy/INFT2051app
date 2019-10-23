using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
        Action onFinishedPlaying { get; set; }
    }
}
