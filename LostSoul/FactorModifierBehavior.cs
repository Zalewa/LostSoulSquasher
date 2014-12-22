using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class FactorModifierBehavior : Behavior
    {
        private float countdown = 0.0f;
        private bool started = false;

        public float Time { get; set; }
        public float Factor { get; set; }

        public FactorModifierBehavior(float factor, float countdown)
        {
            Time = countdown;
            Factor = factor;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (!started)
            {
                countdown = Time;
                started = true;
            }
            countdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (countdown < 0.0f)
            {
                entity.Expired = true;
            }
        }
    }
}
