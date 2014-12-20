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

        protected Behavior inputBehavior = new NullBehavior();
        protected Behavior actionBehavior = new NullBehavior();
        protected Behavior renderBehavior = new NullBehavior();
        protected Behavior animationBehavior = new NullBehavior();
        protected Behavior movementBehavior = new NullBehavior();

        public event EventHandler PositionChanged;
        public event EventHandler ExpiredChanged;

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPositionChanged();
            }
        }
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

        public RenderBehavior RenderBehavior
        {
            get
            {
                return (RenderBehavior)renderBehavior;
            }
        }

        public MovementBehavior MovementBehavior
        {
            get
            {
                return (MovementBehavior)movementBehavior;
            }
        }

        public Entity(LostSoulGame game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            inputBehavior.Run(gameTime, this);
            actionBehavior.Run(gameTime, this);
            movementBehavior.Run(gameTime, this);
            animationBehavior.Run(gameTime, this);
        }

        public void Draw(GameTime gameTime)
        {
            renderBehavior.Run(gameTime, this);
        }

        private void OnPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
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
