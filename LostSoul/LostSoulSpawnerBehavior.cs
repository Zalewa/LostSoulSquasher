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

        private const int MinSpawns = 1;
        private const int MaxSpawns = 5;
        private const double ChanceOfSingularSpawn = 0.7;
        private const double ChanceOfFasterSpeed = 0.1;
        private const float DifficultySpeedMultiplier = 0.2f;
        private const float SpawnCountdownDifficultyDivisor = 1000.0f;
        private const float MinSpawnCountdown = 0.2f;
        private const float SpawnCountIncreaseInterval = 20.0f;

        private Random random = new Random();
        private float countdownTillSpawn = 1.0f;
        private float difficulty = 1.0f;
        private int maxSouls = 30;
        private List<Entity> souls = new List<Entity>();

        public override void Run(GameTime gameTime, Entity entity)
        {
            difficulty += (float)gameTime.ElapsedGameTime.TotalSeconds;
            countdownTillSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shouldSpawn())
            {
                for (int i = 0; i < NumSimultaneousSpawns; ++i)
                {
                    spawn(entity);
                    if (random.NextDouble() < ChanceOfSingularSpawn)
                    {
                        break;
                    }
                }
                countdownTillSpawn = Math.Max(1.0f - (difficulty / SpawnCountdownDifficultyDivisor), MinSpawnCountdown);
            }
        }

        private void spawn(Entity entity)
        {
            Edge edge = pickEdge();
            var soul = new LostSoul(entity.Game);
            soul.Position = pickLocation(entity.Game, edge);
            soul.MovementBehavior.Velocity = pickVelocity(entity.Game, edge);
            soul.ExpiredChanged += OnSoulExpired;
            souls.Add(soul);
            entity.Game.AddActor(soul);
        }

        private void OnSoulExpired(object sender, EventArgs e)
        {
            souls.Remove((Entity)sender);
        }

        private Vector2 pickLocation(LostSoulGame game, Edge edge)
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

        private Vector2 pickVelocity(LostSoulGame game, Edge edge)
        {
            float speed = 30.0f * DifficultySpeedFactor;
            if (random.NextDouble() < ChanceOfFasterSpeed)
            {
                speed *= 2.0f;
            }
            switch (edge)
            {
                case Edge.Left:
                    return new Vector2(speed, 0.0f);
                case Edge.Right:
                    return new Vector2(-speed, 0.0f);
                case Edge.Top:
                    return new Vector2(0.0f, speed);
                case Edge.Bottom:
                    return new Vector2(0.0f, -speed);
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
            return countdownTillSpawn <= 0.0f && souls.Count < maxSouls;
        }

        private float DifficultySpeedFactor
        {
            get
            {
                return Math.Max(1.0f, difficulty * DifficultySpeedMultiplier);
            }
        }

        private int NumSimultaneousSpawns
        {
            get
            {
                return MathHelper.Clamp((int)(difficulty / SpawnCountIncreaseInterval), MinSpawns, MaxSpawns);
            }
        }
    }
}
