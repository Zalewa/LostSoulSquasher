using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class BackgroundAnimationBehavior : Behavior
    {
        private Entity entity;

        public BackgroundAnimationBehavior(Entity entity)
        {
            this.entity = entity;
            entity.Game.World.GameOverChanged += GameOverChangedHandler;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
        }

        private void GameOverChangedHandler(object sender, EventArgs e)
        {
            var render = (SpriteRenderBehavior)entity.RenderBehavior;
            render.Texture = entity.Game.ContentLoader.GameOverBackground;
        }
    }
}
