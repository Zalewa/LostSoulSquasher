using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    class LostSoulWorldHud
    {
        private LostSoulWorld world;
        private HudElement root;
        private HudElement gameHud;
        private HudElement gameOverHud;

        private HudElement gameOverTextBackground;
        private HudElementText scoreLabel;
        private HudElementText lostSoulsLabel;

        private Color textColor = Color.Red;
        private Color gameOverTextColor = Color.White;

        public LostSoulWorldHud(LostSoulWorld world)
        {
            this.world = world;
            world.GameOverChanged += OnGameOverChangedHandler;
            root = new HudElement(world.Game);
            root.BodyBehavior.Size = new Vector2(world.Game.PlayField.Width, world.Game.PlayField.Height);

            setupGameHud(world);
            setupGameOverHud(world);
        }

        private void setupGameHud(LostSoulWorld world)
        {
            gameHud = new HudElement(world.Game);
            root.AddChild(gameHud);

            scoreLabel = new HudElementText(world.Game);
            scoreLabel.Color = textColor;
            gameHud.AddChild(scoreLabel);

            lostSoulsLabel = new HudElementText(world.Game);
            lostSoulsLabel.Color = textColor;
            lostSoulsLabel.BodyBehavior.Position = new Vector2(200.0f, 0.0f);
            gameHud.AddChild(lostSoulsLabel);
        }

        private void setupGameOverHud(LostSoulWorld world)
        {
            gameOverHud = new HudElement(world.Game);
            gameOverHud.Visible = false;
            gameOverHud.BodyBehavior.Size = root.BodyBehavior.Size;
            root.AddChild(gameOverHud);

            setupGameOverTextBackground(world);
            setupGameOverText(world);
            setupGameOverInstructions(world);
            adjustGameOverTextBackgroundSize();
        }

        private void setupGameOverTextBackground(LostSoulWorld world)
        {
            gameOverTextBackground = new HudElement(world.Game);
            var center = gameOverHud.BodyBehavior.Size * 0.5f;
            var size = new Vector2(200.0f, 200.0f);
            gameOverTextBackground.BodyBehavior.Size = size;
            gameOverTextBackground.BodyBehavior.Position = new Vector2(
                center.X - size.X / 2.0f,
                center.Y - size.Y / 2.0f);
            var render = new PrimitiveRectangleRenderBehavior(gameOverTextBackground);
            render.Color = new Color(Color.Black, 0.8f);
            gameOverTextBackground.RenderBehavior = render;
            gameOverHud.AddChild(gameOverTextBackground);
        }

        private void setupGameOverText(LostSoulWorld world)
        {
            var center = new Vector2(world.Game.PlayField.Center.X, world.Game.PlayField.Center.Y);
            var label = new HudElementText(world.Game, "Game Over");
            var position = center - label.RenderBehavior.Size / 2.0f;
            position.Y -= label.RenderBehavior.Size.Y;
            label.BodyBehavior.Position = position;
            label.Color = gameOverTextColor;
            gameOverTextBackground.AddChild(label);
        }

        private void setupGameOverInstructions(LostSoulWorld world)
        {
            var center = new Vector2(world.Game.PlayField.Center.X, world.Game.PlayField.Center.Y);
            var label = new HudElementText(world.Game, "Press RMB to restart or Escape to exit.");
            var position = center - label.RenderBehavior.Size / 2.0f;
            position.Y += label.RenderBehavior.Size.Y;
            label.BodyBehavior.Position = position;
            label.Color = gameOverTextColor;
            gameOverTextBackground.AddChild(label);
        }

        private void adjustGameOverTextBackgroundSize()
        {
            Vector2 topLeft = Vector2.Zero;
            Vector2 bottomRight = Vector2.Zero;
            foreach (HudElement element in gameOverTextBackground.Children)
            {
                var candidate = element.BodyBehavior.BoundingRectangle;
                if (topLeft == Vector2.Zero)
                {
                    topLeft = new Vector2(candidate.Left, candidate.Top);
                }
                topLeft.X = Math.Min(candidate.Left, topLeft.X);
                topLeft.Y = Math.Min(candidate.Top, topLeft.Y);
                bottomRight.X = Math.Max(candidate.Right, bottomRight.X);
                bottomRight.Y = Math.Max(candidate.Bottom, bottomRight.Y);
            }
            float margin = 10.0f;
            var size = bottomRight - topLeft;
            size.X += margin * 2.0f;
            size.Y += margin * 2.0f;
            gameOverTextBackground.BodyBehavior.Size = size;
            var center = gameOverTextBackground.Parent.BodyBehavior.BoundingRectangle.Center;
            gameOverTextBackground.BodyBehavior.Position = new Vector2(center.X - size.X / 2.0f, center.Y - size.Y / 2.0f);
        }

        public void Update(GameTime gameTime)
        {
            scoreLabel.Text = "Score: " + world.Score;
            lostSoulsLabel.Text = "Lost souls: " + world.LostEnemies + " of " + world.MaxLostSouls;

            root.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            root.Draw(gameTime);
        }

        void OnGameOverChangedHandler(object sender, EventArgs e)
        {
            var world = (LostSoulWorld)sender;
            gameHud.Visible = !world.IsGameOver();
            gameOverHud.Visible = world.IsGameOver();
            if (world.IsGameOver())
            {
                scoreLabel.Color = gameOverTextColor;
                gameOverHud.AddChild(scoreLabel);
            }
            else
            {
                scoreLabel.Color = textColor;
                gameHud.AddChild(scoreLabel);
            }
        }
    }
}
