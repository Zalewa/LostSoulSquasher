using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class Background : Entity
    {
        private int currentBackgroundIndex = 0;

        public Background(LostSoulGame game)
            : base(game)
        {
            bodyBehavior = new BodyBehavior(this);
            renderBehavior = new SpriteRenderBehavior(game, game.ContentLoader.Backgrounds[currentBackgroundIndex]);
            animationBehavior = new BackgroundAnimationBehavior(this);
        }

        public void CycleBackground()
        {
            ++currentBackgroundIndex;
            if (currentBackgroundIndex >= Game.ContentLoader.Backgrounds.Length)
            {
                currentBackgroundIndex = 0;
            }
            var render = (SpriteRenderBehavior)renderBehavior;
            render.Texture = Game.ContentLoader.Backgrounds[currentBackgroundIndex];
        }
    }
}
