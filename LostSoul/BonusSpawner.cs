using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class BonusSpawner : Entity
    {
        private LostSoulWorld world;

        public BonusSpawner(LostSoulWorld world)
            : base(world.Game)
        {
            this.world = world;

            actionBehavior = new BonusSpawnerBehavior();
        }
    }
}
