using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class BodyBehavior : Behavior
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

        public Rectangle BoundingRectangle
        {
            get
            {
                if (entity.RenderBehavior != null)
                {
                    var bounds = entity.RenderBehavior.BoundingRectangle;
                    bounds.Offset((int)Position.X, (int)Position.Y);
                    return bounds;
                }
                return Rectangle.Empty;
            }
        }

        public Entity Entity
        {
            get { return entity; }
        }
    }
}
