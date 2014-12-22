using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class Bonus : Entity
    {
        private BonusClass bonusClass;

        public Bonus(LostSoulGame game, BonusClass bonusClass)
            :base(game)
        {
            this.bonusClass = bonusClass;

            bodyBehavior = new GameObjectBodyBehavior(this);
            bodyBehavior.Size = new Vector2(32.0f, 32.0f);

            renderBehavior = bonusClass.MkRender(this);
            renderBehavior.CenterOrigin();

            healthBehavior = new HealthBehavior(this);
            healthBehavior.DeathEvent += OnDeathHandler;

            collisionBehavior = new CollisionBehavior(this);
            actionBehavior = new ExpirationActionBehavior(2.0f);
        }

        void OnDeathHandler(object sender, EventArgs e)
        {
            Game.World.Score += bonusClass.Score();
            bonusClass.Activate(Game.World);
            Expired = true;
        }
    }
}
