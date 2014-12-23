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
            if (oldState != null)
            {
                ProcessMouseState(entity);
            }
            if (game.IsActive)
            {
                Mouse.SetPosition(game.Graphics.PreferredBackBufferWidth / 2, game.Graphics.PreferredBackBufferHeight / 2);
            }
            oldState = Mouse.GetState();
        }

        private void ProcessMouseState(Entity entity)
        {
            if (game.IsActive)
            {
                ProcessMouseMovementIfPossible(entity);
            }
            MouseState state = Mouse.GetState();
            if (oldState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
            {
                entity.Firing = true;
            }
        }

        private void ProcessMouseMovementIfPossible(Entity entity)
        {
            ProcessMouseMovement(entity);
        }

        private void ProcessMouseMovement(Entity entity)
        {
            Vector2 delta = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - new Vector2(oldState.X, oldState.Y);
            var newPosition = entity.BodyBehavior.Position + new Vector2(delta.X, delta.Y);
            var playfield = game.World.PlayField;
            newPosition = Vector2.Clamp(newPosition,
                new Vector2(playfield.Left, playfield.Top),
                new Vector2(playfield.Right, playfield.Bottom));
            entity.BodyBehavior.Position = newPosition;
        }
    }
}
