using Microsoft.Xna.Framework.Audio;
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
        public Texture2D[] Backgrounds;
        public Texture2D GameOverBackground;
        public Texture2D SkullLeft;
        public Texture2D SkullRight;
        public Texture2D Crosshair;

        public Texture2D[] Explosion;

        public SpriteFont Font;

        public SoundEffect ExplosionSound;

        public void LoadContent(ContentManager content)
        {
            Backgrounds = new Texture2D[] {
                content.Load<Texture2D>("backgrounds/bg1"),
                content.Load<Texture2D>("backgrounds/bg2"),
                content.Load<Texture2D>("backgrounds/bg3"),
                content.Load<Texture2D>("backgrounds/bg4"),
                content.Load<Texture2D>("backgrounds/bg5"),
                content.Load<Texture2D>("backgrounds/bg6")
            };
            GameOverBackground = content.Load<Texture2D>("backgrounds/gameover1");
            SkullLeft = content.Load<Texture2D>("SKUL_left");
            SkullRight = content.Load<Texture2D>("SKUL");
            Crosshair = content.Load<Texture2D>("crosshair");

            Explosion = new Texture2D[] {
                content.Load<Texture2D>("MISLB0"),
                content.Load<Texture2D>("MISLC0"),
                content.Load<Texture2D>("MISLD0")
            };

            Font = content.Load<SpriteFont>("Miramonte");

            ExplosionSound = content.Load<SoundEffect>("DSBAREXP");
        }
    }
}
