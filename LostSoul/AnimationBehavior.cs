using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class AnimationBehavior : Behavior
    {
        private AnimationFrame[] frames;
        private float countdownToNextFrame = 0.0f;
        private int frameIndex = 0;

        public float Interval { get; set; }

        public bool IsDone
        {
            get
            {
                return frameIndex >= frames.Length && countdownToNextFrame <= 0.0f;
            }
        }

        public bool MarkEntityAsExpiredWhenDone { get; set; }

        public AnimationBehavior(AnimationFrame[] frames, float interval = 1.0f)
        {
            this.frames = frames;
            this.Interval = interval;
            this.MarkEntityAsExpiredWhenDone = false;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            countdownToNextFrame -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (IsSkipTime())
            {
                var render = (SpriteRenderBehavior)entity.RenderBehavior;
                render.Texture = frames[frameIndex].Texture;
                render.Origin = frames[frameIndex].Origin;
                ++frameIndex;
                countdownToNextFrame += Interval;
            }

            if (IsDone && MarkEntityAsExpiredWhenDone)
            {
                entity.Expired = true;
            }
        }

        private bool IsSkipTime()
        {
            return !IsDone && countdownToNextFrame <= 0.0f && Interval > 0.0f;
        }
    }
}
