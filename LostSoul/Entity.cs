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

        protected Behavior bodyBehavior = new NullBehavior();
        protected Behavior inputBehavior = new NullBehavior();
        protected Behavior actionBehavior = new NullBehavior();
        protected Behavior renderBehavior = new NullBehavior();
        protected Behavior animationBehavior = new NullBehavior();
        protected Behavior movementBehavior = new NullBehavior();
        protected Behavior collisionBehavior = new NullBehavior();
        protected Behavior healthBehavior = new NullBehavior();

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
            get { return (BodyBehavior)bodyBehavior;  }
        }

        public RenderBehavior RenderBehavior
        {
            get
            {
                return (RenderBehavior)renderBehavior;
            }
        }

        public bool HasRenderBehavior
        {
            get
            {
                return renderBehavior is RenderBehavior;
            }
        }

        public MovementBehavior MovementBehavior
        {
            get
            {
                return (MovementBehavior)movementBehavior;
            }
        }

        public CollisionBehavior CollisionBehavior
        {
            get
            {
                return (CollisionBehavior)collisionBehavior;
            }
        }

        public bool HasCollisionBehavior
        {
            get
            {
                return collisionBehavior is CollisionBehavior;
            }
        }

        public HealthBehavior HealthBehavior
        {
            get
            {
                return (HealthBehavior)healthBehavior;
            }
        }

        public bool HasHealthBehavior { get { return healthBehavior is HealthBehavior; } }

        public Entity(LostSoulGame game)
        {
            this.game = game;
        }

        public virtual void Update(GameTime gameTime)
        {
            bodyBehavior.Run(gameTime, this);
            inputBehavior.Run(gameTime, this);
            actionBehavior.Run(gameTime, this);
            healthBehavior.Run(gameTime, this);
            movementBehavior.Run(gameTime, this);
            animationBehavior.Run(gameTime, this);
            collisionBehavior.Run(gameTime, this);
        }

        public void Draw(GameTime gameTime)
        {
            renderBehavior.Run(gameTime, this);
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
