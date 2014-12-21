using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class TextRenderBehavior : RenderBehavior
    {
        private LostSoulGame game;
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; }

        public TextRenderBehavior(LostSoulGame game, string text)
        {
            this.game = game;
            Text = text;
            Font = game.ContentLoader.Font;
            Color = Color.White;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            game.SpriteBatch.DrawString(Font, Text, entity.BodyBehavior.Position, Color);
        }

        public override Vector2 Size
        {
            get { return Font.MeasureString(Text); }
        }
    }
}
