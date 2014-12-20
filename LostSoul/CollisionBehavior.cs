using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class CollisionBehavior : Behavior
    {
        public event EventHandler CollisionDetected;
        private HashSet<Entity> colliding = new HashSet<Entity>();
        public HashSet<Entity> Colliding
        {
            get
            {
                return colliding;
            }
        }

        public CollisionBehavior(Entity entity)
        {
            entity.Game.RegisterCollision(this);
        }

        public bool Collides(Entity e1, Entity e2)
        {
            return e1.BoundingRectangle.Intersects(e2.BoundingRectangle);
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            foreach (Entity other in entity.Game.Actors)
            {
                if (ShouldCheckCollision(entity, other) && Collides(entity, other))
                {
                    other.CollisionBehavior.AddColliding(entity);
                    AddColliding(other);
                }
            }
        }

        public void FirePendingCollisionEvents()
        {
            if (CollisionDetected != null)
            {
                CollisionDetected(this, EventArgs.Empty);
            }
        }

        public void ClearCollisions()
        {
            colliding.Clear();
        }

        private bool ShouldCheckCollision(Entity us, Entity other)
        {
            return us != other && !us.Expired && !other.Expired
                && other.HasCollisionBehavior && !colliding.Contains(other);
        }

        private void AddColliding(Entity entity)
        {
            colliding.Add(entity);
        }
    }
}
