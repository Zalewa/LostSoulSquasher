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
            renderBehavior = new RenderBehavior(game, texture);
            RenderBehavior.Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            inputBehavior = new PlayerInputBehavior(game);
        }
    }
}
