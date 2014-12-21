using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class SpriteRenderBehavior : RenderBehavior
    {
        private LostSoulGame game;
        public Texture2D Texture { get; set; }

        public SpriteRenderBehavior(LostSoulGame game, Texture2D texture)
        {
            this.game = game;
            Texture = texture;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            game.SpriteBatch.Draw(Texture, entity.BodyBehavior.Position, null,
                Color.White, 0.0f, Origin, 1.0f, SpriteEffects.None, 0.0f);
        }

        public override Vector2 Size
        {
            get { return new Vector2(Texture.Width, Texture.Height); }
        }
    }
}
