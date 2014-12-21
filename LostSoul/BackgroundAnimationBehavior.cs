using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class BackgroundAnimationBehavior : Behavior
    {
        private const float backgroundChangeInterval = 20.0f;

        private Entity entity;
        private float countdownTillBackgroundChange = 0.0f;
        private int backgroundIndex = -1;
        private Random random = new Random();

        public BackgroundAnimationBehavior(Entity entity)
        {
            this.entity = entity;
            entity.Game.World.GameOverChanged += GameOverChangedHandler;
            RandomizeBackgroundIndex(entity);
        }


        public override void Run(GameTime gameTime, Entity entity)
        {
            if (!entity.Game.World.IsGameOver())
            {
                countdownTillBackgroundChange -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (countdownTillBackgroundChange < 0.0f)
                {
                    SwitchToNextBackground(entity);
                    countdownTillBackgroundChange = backgroundChangeInterval;
                }
            }
        }

        private void SwitchToNextBackground(Entity entity)
        {
            var render = (SpriteRenderBehavior)entity.RenderBehavior;
            RandomizeBackgroundIndex(entity);
            render.Texture = entity.Game.ContentLoader.Backgrounds[backgroundIndex];
        }

        private void RandomizeBackgroundIndex(Entity entity)
        {
            int currentIndex = backgroundIndex;
            while (backgroundIndex == currentIndex)
            {
                backgroundIndex = random.Next(entity.Game.ContentLoader.Backgrounds.Length);
            }
        }

        private void GameOverChangedHandler(object sender, EventArgs e)
        {
            var render = (SpriteRenderBehavior)entity.RenderBehavior;
            render.Texture = entity.Game.ContentLoader.GameOverBackground;
        }
    }
}
