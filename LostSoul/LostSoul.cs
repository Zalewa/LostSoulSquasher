﻿using Microsoft.Xna.Framework;
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
            renderBehavior = new RenderBehavior(game, game.ContentLoader.SkullLeft);
            RenderBehavior.Origin = new Vector2(RenderBehavior.Texture.Width / 2, RenderBehavior.Texture.Height / 2);

            movementBehavior = new MovementBehavior();
            animationBehavior = new LostSoulAnimation(game);
            collisionBehavior = new CollisionBehavior(this);
            healthBehavior = new HealthBehavior(this);
            HealthBehavior.DeathEvent += OnDeath;

            positionObserver = new LostSoulPositionObserver(this);
        }

        private void OnDeath(object sender, EventArgs e)
        {
            Game.Score += 100;
            Expired = true;
        }
    }
}
