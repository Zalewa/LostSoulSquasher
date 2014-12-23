using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class HealthBehavior : Behavior
    {
        private bool dead = false;
        public bool IsDead { get { return dead; } }

        private Entity entity;
        public Entity Entity { get { return entity; } }

        private int health = 0;
        public int Health
        {
            get { return health; }
            set
            {
                var oldHealth = health;
                health = value;
                if (!dead && health <= 0)
                {
                    dead = true;
                    OnDeathEvent();
                }
                else if (!dead && health > 0 && health < oldHealth)
                {
                    OnDamagedEvent();
                }
            }
        }

        public event EventHandler DamagedEvent;
        public event EventHandler DeathEvent;

        public HealthBehavior(Entity entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// Doesn't summon any events. Doesn't revive from dead state.
        /// </summary>
        /// <param name="health"></param>
        public void SetHealthOmittingEventsAndChecks(int health)
        {
            this.health = health;
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
