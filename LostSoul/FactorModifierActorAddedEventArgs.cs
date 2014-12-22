using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class FactorModifierActorAddedEventArgs : EventArgs
    {
        private FactorModifierActor actor;
        public FactorModifierActor Actor { get {return actor;} }

        public FactorModifierActorAddedEventArgs(FactorModifierActor actor)
        {
            this.actor = actor;
        }
    }
}
