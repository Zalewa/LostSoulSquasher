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

        private HudElementText scoreLabel;
        private HudElementText lostSoulsLabel;

        private Color textColor = Color.Red;
        private Color gameOverTextColor = Color.White;

        public LostSoulWorldHud(LostSoulWorld world)
        {
            this.world = world;
            world.GameOverChanged += OnGameOverChangedHandler;
            root = new HudElement(world.Game);

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
            root.AddChild(gameOverHud);

            setupGameOverText(world);
            setupGameOverInstructions(world);
        }

        private void setupGameOverText(LostSoulWorld world)
        {
            var center = new Vector2(world.Game.PlayField.Center.X, world.Game.PlayField.Center.Y);
            var label = new HudElementText(world.Game, "Game Over");
            var position = center - label.RenderBehavior.Size / 2.0f;
            position.Y -= label.RenderBehavior.Size.Y;
            label.BodyBehavior.Position = position;
            label.Color = gameOverTextColor;
            gameOverHud.AddChild(label);
        }

        private void setupGameOverInstructions(LostSoulWorld world)
        {
            var center = new Vector2(world.Game.PlayField.Center.X, world.Game.PlayField.Center.Y);
            var label = new HudElementText(world.Game, "Press RMB to restart or Escape to exit.");
            var position = center - label.RenderBehavior.Size / 2.0f;
            position.Y += label.RenderBehavior.Size.Y;
            label.BodyBehavior.Position = position;
            label.Color = gameOverTextColor;
            gameOverHud.AddChild(label);
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
