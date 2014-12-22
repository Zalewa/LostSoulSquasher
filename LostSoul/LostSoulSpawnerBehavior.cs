using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class LostSoulSpawnerBehavior : Behavior
    {
        private enum Edge
        {
            Left, Right, Top, Bottom
        }

        private const float MaxDifficulty = 50.0f;
        private const float MinDifficulty = 1.0f;
        private const double ChanceOfFasterSpeed = 0.1;
        private const float MinSpawnCountdown = 0.2f;

        private Random random = new Random();
        private float countdownTillSpawn = 1.0f;
        private float difficulty = MinDifficulty;
        private int maxSouls = 30;
        private List<Entity> souls = new List<Entity>();

        public override void Run(GameTime gameTime, Entity entity)
        {
            difficulty += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (difficulty > MaxDifficulty)
            {
                difficulty = MaxDifficulty;
            }
            countdownTillSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (ShouldSpawn())
            {
                Spawn(entity);
                countdownTillSpawn = Math.Max(1.0f - difficulty/ MaxDifficulty, MinSpawnCountdown);
            }
        }

        private void Spawn(Entity entity)
        {
            Edge edge = PickEdge();
            var soul = PickSoul(entity.Game);
            soul.BodyBehavior.Position = PickLocation(entity.Game, edge);
            soul.MovementBehavior.Velocity = PickVelocity(entity.Game, soul);
            soul.ExpiredChanged += OnSoulExpired;
            souls.Add(soul);
            entity.Game.World.AddActor(soul);
        }

        private void OnSoulExpired(object sender, EventArgs e)
        {
            souls.Remove((Entity)sender);
        }

        private LostSoul PickSoul(LostSoulGame game)
        {
            var klass = PickSoulClass();
            return new LostSoul(game, klass);
        }

        private LostSoulClass PickSoulClass()
        {
            float bigSkull = (float)random.NextDouble();
            if (bigSkull < 0.05f)
            {
                return LostSoulClasses.Big;
            }
            float roll = (float)random.NextDouble() * difficulty;

            if (roll > MaxDifficulty * 0.85f)
            {
                return LostSoulClasses.UltraFast;
            }
            else if (roll > MaxDifficulty * 0.65f)
            {
                return LostSoulClasses.Fast;
            }
            else if (roll > MaxDifficulty * 0.30f)
            {
                return LostSoulClasses.Average;
            }
            else
            {
                return LostSoulClasses.Slow;
            }
        }

        private Vector2 PickLocation(LostSoulGame game, Edge edge)
        {
            switch (edge)
            {
                case Edge.Left:
                    return new Vector2(game.PlayField.Left, random.Next(game.PlayField.Top, game.PlayField.Bottom));
                case Edge.Right:
                    return new Vector2(game.PlayField.Right, random.Next(game.PlayField.Top, game.PlayField.Bottom));
                case Edge.Top:
                    return new Vector2(random.Next(game.PlayField.Left, game.PlayField.Right), game.PlayField.Top);
                case Edge.Bottom:
                    return new Vector2(random.Next(game.PlayField.Left, game.PlayField.Right), game.PlayField.Bottom);
                default:
                    throw new NotImplementedException("unknown edge " + edge);
            }
        }

        private Vector2 PickVelocity(LostSoulGame game, LostSoul soul)
        {
            float speed = soul.Klass.Speed;
            if (random.NextDouble() < ChanceOfFasterSpeed)
            {
                speed *= 2.0f;
            }

            var position = soul.BodyBehavior.Position;
            Vector2 diff = new Vector2(game.PlayField.Center.X, game.PlayField.Center.Y) - position;
            diff = Vector2.Normalize(diff);

            float angle = MathHelper.ToRadians(22.5f - (45.0f * (float)random.NextDouble()));
            Vector2 rotated = new Vector2() {
                X = (float)(diff.X * Math.Cos(angle) - diff.Y * Math.Sin(angle)),
                Y = (float)(diff.Y * Math.Cos(angle) + diff.X * Math.Sin(angle))
            };

            return rotated * speed;
        }

        private Edge PickEdge()
        {
            Array values = Enum.GetValues(typeof(Edge));
            return (Edge)values.GetValue(random.Next(values.Length));
        }

        private bool ShouldSpawn()
        {
            return countdownTillSpawn <= 0.0f && souls.Count < maxSouls;
        }
    }
}
