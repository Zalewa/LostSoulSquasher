using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class FactorModifierActor : Entity
    {
        public FactorModifierActor(LostSoulGame game, float factor, float countdown)
            :base(game)
        {
            actionBehavior = new FactorModifierBehavior(factor, countdown);
        }

        public float Factor
        {
            get
            {
                return ((FactorModifierBehavior)actionBehavior).Factor;
            }
        }
    }
}
