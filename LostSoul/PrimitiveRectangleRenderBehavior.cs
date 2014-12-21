using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class PrimitiveRectangleRenderBehavior : RenderBehavior
    {
        private static Texture2D pixel = null;
        private Entity entity;
        public Color Color { get; set; }

        public PrimitiveRectangleRenderBehavior(Entity entity)
        {
            if (pixel == null)
            {
                InitPixel(entity.Game);
            }
            Color = Color.Black;
            this.entity = entity;
        }

        private static void InitPixel(LostSoulGame game)
        {
            pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] colorData = new Color[1];
            pixel.GetData<Color>(colorData);
            colorData[0] = Color.White;
            pixel.SetData<Color>(colorData);
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            var rect = entity.BodyBehavior.BoundingRectangle;
            entity.Game.SpriteBatch.Draw(pixel, rect, Color);
        }

        public override Vector2 Size
        {
            get { return entity.BodyBehavior.Size; }
        }
    }
}
