using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class AtomBomb : Entity
    {
        public AtomBomb(LostSoulGame game)
            :base(game)
        {
            actionBehavior = new AtomBombAction();
        }
    }
}
