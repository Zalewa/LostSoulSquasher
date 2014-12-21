using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class AnimationFrame
    {
        public Texture2D Texture;
        public Vector2 Origin = Vector2.Zero;

        public AnimationFrame(Texture2D texture)
        {
            this.Texture = texture;
        }

        public static AnimationFrame MkCentered(Texture2D texture)
        {
            var result = new AnimationFrame(texture);
            result.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            return result;
        }

        public static AnimationFrame[] MkCentered(Texture2D[] textures)
        {
            var result = new AnimationFrame[textures.Length];
            for (int i = 0; i < textures.Length; ++i)
            {
                result[i] = MkCentered(textures[i]);
            }
            return result;
        }
    }
}
