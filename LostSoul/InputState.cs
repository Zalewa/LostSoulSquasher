using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public struct InputState
    {
        public MouseState MouseState { get; set; }
        public KeyboardState KeyboardState { get; set; }
    }
}
