using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class MovementBehavior : Behavior
    {
        public Vector2 Velocity { get; set; }

        public MovementBehavior()
        {
            Velocity = Vector2.Zero;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            entity.BodyBehavior.Position += Vector2.Multiply(Velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
