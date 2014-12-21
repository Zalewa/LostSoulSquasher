using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class Background : Entity
    {
        public Background(LostSoulGame game)
            : base(game)
        {
            bodyBehavior = new BodyBehavior(this);
            renderBehavior = new SpriteRenderBehavior(game, game.ContentLoader.Backgrounds[2]);
            animationBehavior = new BackgroundAnimationBehavior(this);
        }
    }
}
