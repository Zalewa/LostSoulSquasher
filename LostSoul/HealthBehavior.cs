using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class HealthBehavior : Behavior
    {
        private bool dead = false;
        private Entity entity;
        public Entity Entity { get { return entity; } }
        public int Health = 0;

        public event EventHandler DamagedEvent;
        public event EventHandler DeathEvent;

        public HealthBehavior(Entity entity)
        {
            this.entity = entity;
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if (!dead && Health <= 0)
            {
                dead = true;
                OnDeathEvent();
            }
            else if (!dead && Health > 0)
            {
                OnDamagedEvent();
            }
        }

        public override void Run(Microsoft.Xna.Framework.GameTime gameTime, Entity entity)
        {
            // no-op
        }

        private void OnDamagedEvent()
        {
            if (DamagedEvent != null)
            {
                DamagedEvent(this, EventArgs.Empty);
            }
        }

        private void OnDeathEvent()
        {
            if (DeathEvent != null)
            {
                DeathEvent(this, EventArgs.Empty);
            }
        }
    }
}
