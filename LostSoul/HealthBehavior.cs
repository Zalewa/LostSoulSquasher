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

        public event EventHandler DeathEvent;

        public HealthBehavior(Entity entity)
        {
            this.entity = entity;
        }

        public void Damage()
        {
            Console.WriteLine("Damage");
            if (!dead)
            {
                dead = true;
                OnDeathEvent();
            }
        }

        public override void Run(Microsoft.Xna.Framework.GameTime gameTime, Entity entity)
        {
            // no-op
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
