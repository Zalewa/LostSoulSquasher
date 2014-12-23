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

        public LostSoulWorldInputBehavior(LostSoulWorld world)
        {
            this.world = world;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            if (world.IsGameOver())
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed && PrevMouseState.RightButton == ButtonState.Released)
                {
                    world.Game.ResetGame();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape) && PrevKeyboardState.IsKeyUp(Keys.Escape))
            {
                world.Game.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemCloseBrackets) && PrevKeyboardState.IsKeyUp(Keys.OemCloseBrackets))
            {
                world.Background.CycleBackground();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F) && PrevKeyboardState.IsKeyUp(Keys.F))
            {
                world.Game.ToggleFullScreen();
            }
        }

        private KeyboardState PrevKeyboardState
        {
            get
            {
                return world.Game.PrevInputState.KeyboardState;
            }
        }

        private MouseState PrevMouseState
        {
            get
            {
                return world.Game.PrevInputState.MouseState;
            }
        }
    }
}
