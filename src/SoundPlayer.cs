using System;
using System.Threading.Tasks;
using Bassoon;

namespace SoundPlayer
{
    public class SoundPlayer : IDisposable
    {
        private static SoundPlayer? _instance;
        private static readonly object Lock = new();
        
        private readonly BassoonEngine _engine = new();
        private Sound? _sound;
        
        private SoundPlayer(){}

        private float _volume = 1.0f;
        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                if (_sound != null)
                {
                    _sound.Volume = _volume;
                }
            }
        }

        public Task Play(string file)
        {
            try
            {
                _sound = new Sound(file)
                {
                    Volume = Volume
                };
                _sound.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Task.CompletedTask;
        }

        public void Pause()
        {
            _sound?.Pause();
        }

        public static SoundPlayer Instance()
        {
            if (_instance != null) return _instance;
            lock (Lock)
            {
                _instance ??= new SoundPlayer();
            }
            return _instance;
        }

        public void Dispose()
        {
            _engine.Dispose();
            _sound?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}