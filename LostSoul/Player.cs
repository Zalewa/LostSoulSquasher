using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class Player : Entity
    {
        public int Score { get; set; }

        public Player(LostSoulGame game)
            : base(game)
        {
            Score = 0;

            Texture2D texture = game.ContentLoader.Crosshair;
            bodyBehavior = new GameObjectBodyBehavior(this);
            renderBehavior = new SpriteRenderBehavior(game, texture);
            RenderBehavior.CenterOrigin();

            inputBehavior = new PlayerInputBehavior(game);
            actionBehavior = new PlayerActionBehavior();
            healthBehavior = new HealthBehavior(this);
        }
    }
}
