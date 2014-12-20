using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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

        public Vector2 Position { get; set; }
        public bool Firing { get; set; }
        public bool Expired { get; set; }

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
    }
}
