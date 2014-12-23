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
            bodyBehavior = new GameObjectBodyBehavior(this);
            renderBehavior = new SpriteRenderBehavior(game, game.ContentLoader.Explosion[0]);
            bodyBehavior.Size = renderBehavior.Size;
            var animationBehavior = new AnimationBehavior(AnimationFrame.MkCentered(game.ContentLoader.Explosion), 0.1f);
            animationBehavior.MarkEntityAsExpiredWhenDone = true;
            this.animationBehavior = animationBehavior;
            collisionBehavior = new CollisionBehavior(this);
            CollisionBehavior.CollisionDetected += CollisionDetectedHandler;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!afterFirstUpdate)
            {
                Game.Audio.PlaySound(Game.ContentLoader.ExplosionSound, BodyBehavior.Position);
                CollisionBehavior.Enabled = false;
                afterFirstUpdate = true;
            }
        }

        private void CollisionDetectedHandler(object sender, EventArgs e)
        {
            foreach (Entity colliding in CollisionBehavior.Colliding)
            {
                if (colliding.HealthBehavior != null)
                {
                    colliding.HealthBehavior.Health -= 1;
                }
            }
        }
    }
}
