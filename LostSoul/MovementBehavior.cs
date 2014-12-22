using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class MovementBehavior : Behavior
    {
        public Vector2 Velocity { get; set; }

        private List<FactorModifierActor> speedModifiers = new List<FactorModifierActor>();

        public MovementBehavior()
        {
            Velocity = Vector2.Zero;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
            entity.BodyBehavior.Position += Vector2.Multiply(Velocity,
                (float)gameTime.ElapsedGameTime.TotalSeconds * TotalSpeedModifiers);
        }

        public void AddSpeedModifier(FactorModifierActor actor)
        {
            speedModifiers.Add(actor);
            actor.ExpiredChanged += FactorModifierActorExpiredHandler;
        }

        private void FactorModifierActorExpiredHandler(object sender, EventArgs e)
        {
            speedModifiers.Remove((FactorModifierActor)sender);
        }

        private float TotalSpeedModifiers
        {
            get
            {
                float factor = 1.0f;
                foreach (FactorModifierActor modifier in speedModifiers)
                {
                    if (!modifier.Expired)
                    {
                        factor *= modifier.Factor;
                    }
                }
                return factor;
            }
        }
    }
}
