using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class SoundRequest
    {
        public SoundEffect Sound { get; set; }
        public Vector2 Position { get; set; }
    };

    public class AudioSystem
    {
        private Random random = new Random();
        private List<SoundRequest> soundRequests = new List<SoundRequest>();
        public Vector2 ListenerPosition = Vector2.Zero;
        public float PanDivisor = 1.0f;
        public float PanClamp = 1.0f;

        public AudioSystem()
        {
        }

        public void PlaySound(SoundEffect sound, Vector2 position)
        {
            var request = new SoundRequest();
            request.Sound = sound;
            request.Position = position;
            soundRequests.Add(request);
        }


        public void FireSounds()
        {
            soundRequests.ForEach(e => FireSound(e));
            soundRequests.Clear();
        }

        private void FireSound(SoundRequest request)
        {
            SoundEffectInstance instance = request.Sound.CreateInstance();
            Vector2 relative = request.Position - ListenerPosition;
            instance.Pan = MathHelper.Clamp(relative.X / PanDivisor, -PanClamp, PanClamp);
            instance.Pitch = 0.5f - (float)random.NextDouble();
            instance.Play();
        }

        public void PlayMusic(Song song)
        {
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
