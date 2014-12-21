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
        public Vector2 Scale { get; set; }

        public SpriteRenderBehavior(LostSoulGame game, Texture2D texture)
        {
            this.game = game;
            Texture = texture;
            Scale = Vector2.One;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            Rectangle destinationRectangle = new Rectangle(
                (int)entity.BodyBehavior.Position.X,
                (int)entity.BodyBehavior.Position.Y,
                (int)(Texture.Width * Scale.X),
                (int)(Texture.Height * Scale.Y));
            game.SpriteBatch.Draw(Texture, destinationRectangle, null,
                Color.White, 0.0f, Origin, SpriteEffects.None, 0.0f);
        }

        public override Vector2 Size
        {
            get { return new Vector2(Texture.Width, Texture.Height); }
        }
    }
}
