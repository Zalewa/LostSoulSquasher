using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class ContentLoader
    {
        public Texture2D Background;
        public Texture2D SkullLeft;
        public Texture2D SkullRight;
        public Texture2D Crosshair;

        public Texture2D[] Explosion;

        public void LoadContent(ContentManager content)
        {
            Background = content.Load<Texture2D>("background");
            SkullLeft = content.Load<Texture2D>("SKUL_left");
            SkullRight = content.Load<Texture2D>("SKUL");
            Crosshair = content.Load<Texture2D>("crosshair");

            Explosion = new Texture2D[] {
                content.Load<Texture2D>("MISLB0"),
                content.Load<Texture2D>("MISLC0"),
                content.Load<Texture2D>("MISLD0")
            };
        }
    }
}
