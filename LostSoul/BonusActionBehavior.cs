using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class ExpirationActionBehavior : Behavior
    {
        float expirationCountdown = 0.0f;

        public ExpirationActionBehavior(float expirationTime)
        {
            this.expirationCountdown = expirationTime;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            expirationCountdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (expirationCountdown <= 0.0f)
            {
                entity.Expired = true;
            }
        }
    }
}
