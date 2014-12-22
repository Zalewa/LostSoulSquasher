using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class BonusSpawnerBehavior : Behavior
    {
        private const float bonusStartingCountUp = -10.0f;
        private const float bonusProbabilityInterval = 60.0f;
        private float timeSinceLastBonus = bonusStartingCountUp;
        private Random random = new Random();

        public override void Run(GameTime gameTime, Entity entity)
        {
            timeSinceLastBonus += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (random.NextDouble() < BonusProbability)
            {
                SpawnBonus(entity.Game.World);
                timeSinceLastBonus = bonusStartingCountUp;
            }
        }

        private void SpawnBonus(LostSoulWorld world)
        {
            var klass = BonusRandomFactory.PickRandomBonus();
            if (klass == null)
            {
                throw new InvalidOperationException("null bonus class returned");
            }
            var entity = new Bonus(world.Game, klass);
            var spawnField = world.Game.PlayField;
            float margin = entity.BodyBehavior.Size.X;
            spawnField.Inflate((int)-margin, (int)-margin);
            entity.BodyBehavior.Position = new Vector2(
                (float)spawnField.Left + (float)random.NextDouble() * spawnField.Width,
                (float)spawnField.Top + (float)random.NextDouble() * spawnField.Height);
            world.AddActor(entity);
        }

        private float BonusProbability
        {
            get
            {
                return Math.Max(0.0f, timeSinceLastBonus / bonusProbabilityInterval);
            }
        }
    }
}
