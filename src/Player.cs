using System;
using System.IO;
using System.Threading.Tasks;
using Bassoon;

namespace SoundPlayer
{
    public class Player : IDisposable
    {
        private static Player? _instance;
        private static readonly object Lock = new();
        
        private readonly BassoonEngine _engine = new();
        private Sound? _sound;

        private Player() { }

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

        public static Player Instance()
        {
            if (_instance != null) return _instance;
            lock (Lock)
            {
                _instance ??= new Player();
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