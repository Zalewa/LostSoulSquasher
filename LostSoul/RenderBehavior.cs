using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public abstract class RenderBehavior : Behavior
    {
        public Vector2 Origin { get; set; }
        public abstract Vector2 Size { get; }

        public RenderBehavior()
        {
            this.Origin = Vector2.Zero;
        }

        public void CenterOrigin()
        {
            Origin = Size / 2.0f;
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)Origin.X, (int)Origin.Y,
                    (int)(Size.X - Origin.X), (int)(Size.Y - Origin.Y));
            }
        }
    }
}
