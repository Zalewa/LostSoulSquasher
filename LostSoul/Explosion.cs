using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class Explosion : Entity
    {
        private bool afterFirstUpdate = false;

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!afterFirstUpdate)
            {
                afterFirstUpdate = true;
            }
            else
            {
                CollisionBehavior.Enabled = false;
            }
        }

        private void CollisionDetected(object sender, EventArgs e)
        {
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
