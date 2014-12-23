using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class AtomBombAction : Behavior
    {
        private float boomTime = 0.2f;
        private float countDownUntilNextBoom = 0.0f;
        private float intervalBetweenBooms = 0.0f;
        private bool started = false;
        private List<Vector2> positions;

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (!started)
            {
                Start(entity);
            }

            if (positions.Count != 0)
            {
                RunExplosion(gameTime, entity.Game);
            }
            else
            {
                entity.Expired = true;
            }
        }

        private void RunExplosion(GameTime gameTime, LostSoulGame game)
        {
            countDownUntilNextBoom -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (countDownUntilNextBoom <= 0.0f)
            {
                SpawnExplosion(game, positions.ElementAt(0));
                positions.RemoveAt(0);
                countDownUntilNextBoom = intervalBetweenBooms;
            }
        }

        private void SpawnExplosion(LostSoulGame game, Vector2 position)
        {
            var explosion = new Explosion(game);
            explosion.BodyBehavior.Position = position;
            game.World.AddActor(explosion);
        }

        private void Start(Entity entity)
        {
            positions = CalculateExplosionPositions(entity.Game);
            intervalBetweenBooms = boomTime / (float)positions.Count;
            entity.Game.Audio.PlaySound(entity.Game.ContentLoader.AtomBoomSound);
            started = true;
        }

        private List<Vector2> CalculateExplosionPositions(LostSoulGame game)
        {
            var result = new List<Vector2>();
            var explosionPrototype = new Explosion(game);
            var size = explosionPrototype.BodyBehavior.Size;
            var playField = game.World.PlayField;
            for (float x = size.X; x < playField.Width; x += size.X)
            {
                for (float y = size.Y; y < playField.Height; y += size.Y)
                {
                    result.Add(new Vector2(x, y));
                }
            }
            return result;
        }
    }
}
