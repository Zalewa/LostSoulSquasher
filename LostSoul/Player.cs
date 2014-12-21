using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class Player : Entity
    {
        public Player(LostSoulGame game)
            : base(game)
        {
            Texture2D texture = game.ContentLoader.Crosshair;
            bodyBehavior = new GameObjectBodyBehavior(this);
            renderBehavior = new SpriteRenderBehavior(game, texture);
            RenderBehavior.CenterOrigin();

            inputBehavior = new PlayerInputBehavior(game);
            actionBehavior = new PlayerActionBehavior();
        }
    }
}
