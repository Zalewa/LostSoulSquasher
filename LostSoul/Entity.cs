using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class Entity
    {
        private LostSoulGame game;

        protected BodyBehavior bodyBehavior = null;
        protected Behavior inputBehavior = null;
        protected Behavior actionBehavior = null;
        protected RenderBehavior renderBehavior = null;
        protected Behavior animationBehavior = null;
        protected MovementBehavior movementBehavior = null;
        protected CollisionBehavior collisionBehavior = null;
        protected HealthBehavior healthBehavior = null;

        public event EventHandler ExpiredChanged;

        public bool Firing { get; set; }

        private bool expired;
        public bool Expired
        {
            get
            {
                return expired;
            }
            set
            {
                expired = value;
                OnExpiredChanged();
            }
        }

        public LostSoulGame Game
        {
            get
            {
                return game;
            }
        }

        public BodyBehavior BodyBehavior
        {
            get { return bodyBehavior;  }
        }

        public RenderBehavior RenderBehavior
        {
            get
            {
                return renderBehavior;
            }
            set
            {
                renderBehavior = value;
            }
        }

        public MovementBehavior MovementBehavior
        {
            get
            {
                return movementBehavior;
            }
        }

        public CollisionBehavior CollisionBehavior
        {
            get
            {
                return collisionBehavior;
            }
        }

        public HealthBehavior HealthBehavior
        {
            get
            {
                return (HealthBehavior)healthBehavior;
            }
        }

        public Entity(LostSoulGame game)
        {
            this.game = game;
        }

        public virtual void Update(GameTime gameTime)
        {
            var behaviors = new Behavior[] {
                bodyBehavior,
                inputBehavior,
                actionBehavior,
                healthBehavior,
                movementBehavior,
                animationBehavior,
                collisionBehavior
            };
            foreach (var behavior in behaviors)
            {
                if (behavior != null)
                {
                    behavior.Run(gameTime, this);
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (renderBehavior != null)
            {
                renderBehavior.Run(gameTime, this);
            }
        }

        private void OnExpiredChanged()
        {
            if (ExpiredChanged != null)
            {
                ExpiredChanged(this, EventArgs.Empty);
            }
        }
    }
}
