using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class LostSoulAnimation : Behavior
    {
        private Texture2D left;
        private Texture2D right;

        public LostSoulAnimation(LostSoulGame game)
        {
            left = game.ContentLoader.SkullLeft;
            right = game.ContentLoader.SkullRight;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (FloatingPoint.Compare(entity.MovementBehavior.Velocity.X, 0.0f))
            {
                SetTexture(entity, entity.MovementBehavior.Velocity.Y > 0.0f ? left : right);
            }
            else if (entity.MovementBehavior.Velocity.X < 0.0f)
            {
                SetTexture(entity, left);
            }
            else if (entity.MovementBehavior.Velocity.X > 0.0f)
            {
                SetTexture(entity, right);
            }
        }

        private void SetTexture(Entity entity, Texture2D texture)
        {
            var render = (SpriteRenderBehavior)entity.RenderBehavior;
            render.Texture = texture;
            render.CenterOrigin();
        }
    }
}
