using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoulClass
    {
        public Color Color { get; set; }
        public float Speed { get; set; }
        // Lower factor = less difficulty.
        public float DifficultyFactor { get; set; }
        public Vector2 Scale { get; set; }
        public int Health { get; set; }
        public int KillScore { get; set; }
        public int DamageScore { get; set; }

        public LostSoulClass()
        {
            Color = Color.White;
            Scale = Vector2.One;
            Health = 1;
            KillScore = 100;
            DamageScore = 0;
        }
    }

    public class LostSoulClasses
    {
        private static LostSoulSlow slow;
        public static LostSoulSlow Slow { get { return slow; } }
        private static LostSoulAverage average;
        public static LostSoulAverage Average { get { return average; } }
        private static LostSoulFast fast;
        public static LostSoulFast Fast { get { return fast; } }
        private static LostSoulUltraFast ultraFast;
        public static LostSoulUltraFast UltraFast { get { return ultraFast; } }
        private static LostSoulBig big;
        public static LostSoulBig Big { get { return big; } }

        public static void LoadContent(LostSoulGame game)
        {
            slow = new LostSoulSlow();
            average = new LostSoulAverage();
            fast = new LostSoulFast();
            ultraFast = new LostSoulUltraFast();
            big = new LostSoulBig(game);
        }
    }

    public class LostSoulSlow : LostSoulClass
    {
        public LostSoulSlow()
        {
            Speed = 50.0f;
            Color = Color.White;
            DifficultyFactor = 1.0f;
        }
    }

    public class LostSoulAverage : LostSoulClass
    {
        public LostSoulAverage()
        {
            KillScore = 150;
            Speed = 90.0f;
            Color = Color.Orange;
            DifficultyFactor = 2.0f;
        }
    }

    public class LostSoulFast : LostSoulClass
    {
        public LostSoulFast()
        {
            KillScore = 200;
            Speed = 120.0f;
            Color = Color.Red;
            DifficultyFactor = 3.0f;
        }
    }

    public class LostSoulUltraFast : LostSoulClass
    {
        public LostSoulUltraFast()
        {
            KillScore = 200;
            Speed = 180.0f;
            Color = Color.Crimson;
            DifficultyFactor = 3.0f;
        }
    }

    public class LostSoulBig : LostSoulClass
    {
        public LostSoulBig(LostSoulGame game)
        {
            KillScore = 300;
            DamageScore = 50;
            Health = 3;
            Speed = 45.0f;
            Color = Color.White;
            DifficultyFactor = 3.0f;
            Scale = new Vector2(3.0f, 3.0f);
        }
    }
}
