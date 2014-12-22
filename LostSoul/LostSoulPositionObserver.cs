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
            entity.BodyBehavior.PositionChanged += HandlePositionChanged;
        }

        public void HandlePositionChanged(object sender, EventArgs args)
        {
            LostSoul entity = (LostSoul)((BodyBehavior)sender).Entity;
            var pos = entity.BodyBehavior.Position;
            if (!entity.Game.PlayField.Contains((int)pos.X, (int)pos.Y))
            {
                if (wasInPlayfield)
                {
                    entity.Game.Audio.PlaySound(entity.Game.ContentLoader.SkullEscapeSound);
                    entity.Game.World.AddLives(-1);
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
