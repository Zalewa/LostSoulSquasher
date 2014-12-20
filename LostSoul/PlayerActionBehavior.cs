using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class PlayerActionBehavior : Behavior
    {
        private const float REFIRE_TIME = 0.2f;

        private GameTime lastFireTime = null;

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (entity.Firing)
            {
                fireIfTimeIsAppropriate(gameTime, entity);
                entity.Firing = false;
            }
        }

        private void fireIfTimeIsAppropriate(GameTime gameTime, Entity entity)
        {
            if (canFire(gameTime))
            {
                fire(entity);
            }
        }

        private void fire(Entity entity)
        {
            
        }

        private bool canFire(GameTime gameTime)
        {
            return lastFireTime == null ||
                lastFireTime.TotalGameTime.TotalSeconds + REFIRE_TIME < gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
