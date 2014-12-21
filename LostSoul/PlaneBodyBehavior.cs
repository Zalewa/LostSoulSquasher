using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class PlaneBodyBehavior : BodyBehavior
    {
        public PlaneBodyBehavior(Entity entity)
            :base(entity)
        {

        }

        public override Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)(Position.X), (int)(Position.Y), (int)Size.X, (int)Size.Y);
            }
        }
    }
}
