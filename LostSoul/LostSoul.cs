using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoul : Entity
    {
        public LostSoulClass Klass;
        private LostSoulPositionObserver positionObserver;

        public LostSoul(LostSoulGame game, LostSoulClass klass)
            : base(game)
        {
            Klass = klass;
            bodyBehavior = new GameObjectBodyBehavior(this);

            renderBehavior = new SpriteRenderBehavior(game, game.ContentLoader.SkullLeft);
            Render.CenterOrigin();
            Render.Color = klass.Color;
            Render.Scale = klass.Scale;

            bodyBehavior.Size = Vector2.Multiply(renderBehavior.Size, klass.Scale);

            movementBehavior = new MovementBehavior();
            animationBehavior = new LostSoulAnimation(game);
            collisionBehavior = new CollisionBehavior(this);
            healthBehavior = new HealthBehavior(this);
            HealthBehavior.Health = klass.Health;
            HealthBehavior.DeathEvent += OnDeath;
            HealthBehavior.DamagedEvent += OnDamaged;

            positionObserver = new LostSoulPositionObserver(this);
        }

        private void OnDamaged(object sender, EventArgs e)
        {
            Game.World.Score += Klass.DamageScore;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            Game.World.Score += Klass.KillScore;
            Expired = true;
        }

        public SpriteRenderBehavior Render
        {
            get { return (SpriteRenderBehavior)renderBehavior; }
        }
    }
}
