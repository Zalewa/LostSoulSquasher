using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class SoundDefinition
    {
        public SoundEffect SoundEffect { set; get; }
        public float MaxPitchVariation { set; get; }

        public SoundDefinition(SoundEffect effect)
        {
            SoundEffect = effect;
            MaxPitchVariation = 1.0f;
        }
    }
}
