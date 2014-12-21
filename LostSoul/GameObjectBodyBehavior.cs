using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class GameObjectBodyBehavior : BodyBehavior
    {
        public GameObjectBodyBehavior(Entity entity)
            : base(entity)
        {

        }

        public override Microsoft.Xna.Framework.Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - Size.X / 2.0f),
                    (int)(Position.Y - Size.Y / 2.0f),
                    (int)Size.X, (int)Size.Y);
            }
        }
    }
}
