using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class AnimationBehavior : Behavior
    {
        private Texture2D[] textures;
        private float countdownToNextFrame = 0.0f;
        private int frameIndex = -1;

        public float Speed { get; set; }

        public AnimationBehavior(Texture2D[] textures)
        {
            this.textures = textures;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            
        }
    }
}
