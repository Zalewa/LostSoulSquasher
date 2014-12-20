using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoulSpawner : Entity
    {
        public LostSoulSpawner(LostSoulGame game)
            : base(game)
        {
            actionBehavior = new LostSoulSpawnerBehavior();
        }
    }
}
