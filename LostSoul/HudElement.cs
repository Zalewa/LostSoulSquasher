using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class HudElement : Entity
    {
        private HudElement parent = null;
        public HudElement Parent { get { return parent; } }

        private List<HudElement> children = new List<HudElement>();
        public List<HudElement> Children { get { return children; } }

        public bool Visible { get; set; }

        public HudElement(LostSoulGame game)
            : base(game)
        {
            bodyBehavior = new PlaneBodyBehavior(this);
            Visible = true;
        }

        public void AddChild(HudElement element)
        {
            if (element == this)
            {
                throw new InvalidOperationException("HudElement cannot be its own child");
            }
            if (element.parent != null)
            {
                element.parent.RemoveChild(element);
            }
            element.parent = this;
            children.Add(element);
        }

        public void RemoveChild(HudElement element)
        {
            children.Remove(element);
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                base.Update(gameTime);
                children.ForEach(e => e.Update(gameTime));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                base.Draw(gameTime);
                children.ForEach(e => e.Draw(gameTime));
            }
        }
    }
}
