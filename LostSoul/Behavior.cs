using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public abstract class Behavior
    {
        public abstract void Run(GameTime gameTime, Entity entity);
    }
}
