using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class LostSoulWorldInputBehavior : Behavior
    {
        private LostSoulWorld world;
        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;

        public LostSoulWorldInputBehavior(LostSoulWorld world)
        {
            this.world = world;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (world.IsGameOver())
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
                {
                    world.Game.ResetGame();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                world.Game.Exit();
            }
            oldMouseState = Mouse.GetState();
            oldKeyboardState = Keyboard.GetState();
        }
    }
}
