﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class PlayerActionBehavior : Behavior
    {
        private const float RefireTime = 0.2f;

        private GameTime lastFireTime = null;

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (entity.Firing)
            {
                FireIfTimeIsAppropriate(gameTime, entity);
                entity.Firing = false;
            }
        }

        private void FireIfTimeIsAppropriate(GameTime gameTime, Entity entity)
        {
            if (CanFire(gameTime))
            {
                Fire(entity);
            }
        }

        private void Fire(Entity entity)
        {
            var boomActor = new Explosion(entity.Game);
            boomActor.Position = entity.Position;
            entity.Game.AddActor(boomActor);
        }

        private bool CanFire(GameTime gameTime)
        {
            return lastFireTime == null ||
                lastFireTime.TotalGameTime.TotalSeconds + RefireTime < gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
