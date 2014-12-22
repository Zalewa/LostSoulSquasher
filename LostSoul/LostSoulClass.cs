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
        public static readonly LostSoulSlow Slow = new LostSoulSlow();
        public static readonly LostSoulAverage Average = new LostSoulAverage();
        public static readonly LostSoulFast Fast = new LostSoulFast();
        public static readonly LostSoulUltraFast UltraFast = new LostSoulUltraFast();
        public static readonly LostSoulBig Big = new LostSoulBig();
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
        public LostSoulBig()
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
