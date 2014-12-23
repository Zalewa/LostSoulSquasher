using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

        public SoundDefinition AtomBoomSound;
        public SoundDefinition ExplosionSound;
        public SoundDefinition OneUpSound;
        public SoundDefinition BabySound;
        public SoundDefinition TurtleSound;
        public SoundDefinition SkullShowSound;
        public SoundDefinition SkullEscapeSound;

        public Song Ambient1;
        public Song GameOverMusic;

        public Texture2D BonusOneUp;
        public Texture2D BonusFiveUp;
        public Texture2D BonusAtom;
        public Texture2D BonusBaby;
        public Texture2D BonusTurtle;

        public void LoadContent(ContentManager content)
        {
            Backgrounds = new Texture2D[] {
                content.Load<Texture2D>("backgrounds/bg1"),
                content.Load<Texture2D>("backgrounds/bg2"),
                content.Load<Texture2D>("backgrounds/bg3"),
                content.Load<Texture2D>("backgrounds/bg4"),
                content.Load<Texture2D>("backgrounds/bg5"),
                content.Load<Texture2D>("backgrounds/bg6"),
                content.Load<Texture2D>("backgrounds/bg7")
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

            AtomBoomSound = new SoundDefinition(content.Load<SoundEffect>("atomboom"));
            ExplosionSound = new SoundDefinition(content.Load<SoundEffect>("DSBAREXP"));
            OneUpSound = new SoundDefinition(content.Load<SoundEffect>("1up"));
            BabySound = new SoundDefinition(content.Load<SoundEffect>("baby"))
                {
                    MaxPitchVariation = 0.15f
                };
            TurtleSound = new SoundDefinition(content.Load<SoundEffect>("turtle"));
            SkullShowSound = new SoundDefinition(content.Load<SoundEffect>("DSDMACT"));
            SkullEscapeSound = new SoundDefinition(content.Load<SoundEffect>("DSSKLATK"));

            Ambient1 = content.Load<Song>("Carefree");
            GameOverMusic = content.Load<Song>("ecfike__the-end-of-the-world");

            BonusOneUp = content.Load<Texture2D>("bonus_1up");
            BonusFiveUp = content.Load<Texture2D>("bonus_5up");
            BonusAtom = content.Load<Texture2D>("bonus_atom");
            BonusBaby = content.Load<Texture2D>("bonus_baby");
            BonusTurtle = content.Load<Texture2D>("bonus_turtle");
        }
    }
}
