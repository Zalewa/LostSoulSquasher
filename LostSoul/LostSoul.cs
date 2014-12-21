using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoul : Entity
    {
        private LostSoulPositionObserver positionObserver;

        public LostSoul(LostSoulGame game)
            : base(game)
        {
            bodyBehavior = new GameObjectBodyBehavior(this);

            renderBehavior = new SpriteRenderBehavior(game, game.ContentLoader.SkullLeft);
            RenderBehavior.CenterOrigin();

            bodyBehavior.Size = renderBehavior.Size;

            movementBehavior = new MovementBehavior();
            animationBehavior = new LostSoulAnimation(game);
            collisionBehavior = new CollisionBehavior(this);
            healthBehavior = new HealthBehavior(this);
            HealthBehavior.DeathEvent += OnDeath;

            positionObserver = new LostSoulPositionObserver(this);
        }

        private void OnDeath(object sender, EventArgs e)
        {
            Game.World.Score += 100;
            Expired = true;
        }
    }
}
