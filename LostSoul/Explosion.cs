using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class Explosion : Entity
    {
        public Explosion(LostSoulGame game)
            : base(game)
        {
            renderBehavior = new RenderBehavior(game, game.ContentLoader.Explosion[0]);
            var animationBehavior = new AnimationBehavior(AnimationFrame.mkCentered(game.ContentLoader.Explosion), 0.1f);
            animationBehavior.MarkEntityAsExpiredWhenDone = true;
            this.animationBehavior = animationBehavior;
            collisionBehavior = new CollisionBehavior(this);
            CollisionBehavior.CollisionDetected += CollisionDetected;
        }

        private void CollisionDetected(object sender, EventArgs e)
        {
            Console.WriteLine("Colliding: " + CollisionBehavior.Colliding.Count);
            foreach (Entity colliding in CollisionBehavior.Colliding)
            {
                if (colliding.HasHealthBehavior)
                {
                    colliding.HealthBehavior.Damage();
                }
            }
        }
    }
}
