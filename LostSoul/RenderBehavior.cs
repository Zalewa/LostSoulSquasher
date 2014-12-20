using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class RenderBehavior : Behavior
    {
        LostSoulGame game;

        public Vector2 Origin { get; set; }
        public Texture2D Texture { get; set; }

        public RenderBehavior(LostSoulGame game, Texture2D texture)
        {
            this.game = game;
            this.Texture = texture;
            this.Origin = Vector2.Zero;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            game.SpriteBatch.Draw(Texture, entity.Position, null, Color.White, 0.0f, Origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
