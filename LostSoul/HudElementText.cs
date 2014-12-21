using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class HudElementText : HudElement
    {
        public HudElementText(LostSoulGame game, string text = "")
            : base(game)
        {
            bodyBehavior = new RenderDependentBodyBehavior(this);
            renderBehavior = new TextRenderBehavior(Game, text);
        }

        public string Text
        {
            get { return Render.Text; }
            set { Render.Text = value; }
        }

        public Color Color
        {
            get { return Render.Color; }
            set { Render.Color = value; }
        }

        private TextRenderBehavior Render
        {
            get { return (TextRenderBehavior)renderBehavior; }
        }

        public override string ToString()
        {
            return base.ToString() + " (" + Text + ")";
        }
    }
}
