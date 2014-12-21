using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class BonusSpawnerBehavior : Behavior
    {
        private const float bonusProbabilityInterval = 60.0f;
        private float timeSinceLastBonus = -10.0f;
        private Random random = new Random();

        public override void Run(GameTime gameTime, Entity entity)
        {
            timeSinceLastBonus += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (random.NextDouble() < BonusProbability)
            {
                SpawnBonus(entity.Game.World);
                timeSinceLastBonus = -10.0f;
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
            var playField = world.Game.PlayField;
            entity.BodyBehavior.Position = new Vector2(
                (float)(playField.Left + (float)random.NextDouble() * playField.Width - entity.BodyBehavior.Size.X * 4.0f),
                (float)(playField.Top + (float)random.NextDouble() * playField.Height - entity.BodyBehavior.Size.Y * 4.0f));
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
