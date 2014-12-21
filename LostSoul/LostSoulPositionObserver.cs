using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class LostSoulPositionObserver
    {
        private bool wasInPlayfield = false;

        public LostSoulPositionObserver(LostSoul entity)
        {
            entity.PositionChanged += HandlePositionChanged;
        }

        public void HandlePositionChanged(object sender, EventArgs args)
        {
            LostSoul entity = (LostSoul)sender;
            if (!entity.Game.PlayField.Contains(entity.Position))
            {
                if (wasInPlayfield)
                {
                    entity.Game.World.IncrementLostEnemy();
                    entity.Expired = true;
                }
            }
            else
            {
                wasInPlayfield = true;
            }
        }
    }
}
