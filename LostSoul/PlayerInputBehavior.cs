using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class PlayerInputBehavior : Behavior
    {
        private LostSoulGame game;
        private MouseState oldState;

        public PlayerInputBehavior(LostSoulGame game)
        {
            this.game = game;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 gamePosition = game.ProjectScreenCoordsToGameCoords(mousePosition);
            entity.Position = gamePosition;

            if (oldState != null)
            {
                processMouseState(entity);
            }
            oldState = Mouse.GetState();
        }

        private void processMouseState(Entity entity)
        {
            MouseState state = Mouse.GetState();
            if (oldState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
            {
                entity.Firing = true;
            }
        }
    }
}
