﻿using Microsoft.Xna.Framework;
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

        private Random random = new Random();
        private float countdownTillSpawn = 1.0f;

        public override void Run(GameTime gameTime, Entity entity)
        {
            countdownTillSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shouldSpawn())
            {
                spawn(entity);
                countdownTillSpawn = 1.0f;
            }
        }

        private void spawn(Entity entity)
        {
            Vector2 location = pickLocation(entity.Game);
            var soul = new LostSoul(entity.Game);
            soul.Position = location;
            entity.Game.AddActor(soul);
        }

        private Vector2 pickLocation(LostSoulGame game)
        {
            Edge edge = pickEdge();
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

        private Edge pickEdge()
        {
            Array values = Enum.GetValues(typeof(Edge));
            return (Edge)values.GetValue(random.Next(values.Length));
        }

        private bool shouldSpawn()
        {
            return countdownTillSpawn <= 0.0f;
        }
    }
}