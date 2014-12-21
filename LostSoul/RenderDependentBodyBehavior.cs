using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class RenderDependentBodyBehavior : BodyBehavior
    {
        public RenderDependentBodyBehavior(Entity entity)
            :base(entity)
        {
        }

        public override Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - Render.Origin.X),
                    (int)(Position.Y - Render.Origin.Y),
                    (int)Size.X, (int)Size.Y);
            }
        }

        public override Vector2 Size
        {
            get
            {
                return Render.Size;
            }
            set
            {
                throw new InvalidOperationException("size cannot be set on this body");
            }
        }

        private RenderBehavior Render { get { return Entity.RenderBehavior; } }
    }
}
