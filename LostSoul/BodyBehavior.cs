using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public abstract class BodyBehavior : Behavior
    {
        private Entity entity;
        private Vector2 position;

        public event EventHandler PositionChanged;

        public BodyBehavior(Entity entity)
        {
            this.entity = entity;
        }

        public override void Run(GameTime gameTime, Entity entity)
        {
        }

        private void OnPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPositionChanged();
            }
        }

        public virtual Vector2 Size { get; set; }
        public abstract Rectangle BoundingRectangle { get; }

        public Entity Entity
        {
            get { return entity; }
        }
    }
}
