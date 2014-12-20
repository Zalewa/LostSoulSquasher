#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace LostSoul
{
    public class LostSoulGame : Game
    {
        private readonly Rectangle PLAY_FIELD = new Rectangle(0, 0, 640, 480);

        public GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch;
        public ContentLoader ContentLoader = new ContentLoader();

        private Background background;
        private Player player;

        public LostSoulGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            //IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);

            background = new Background(this);
            player = new Player(this);
            player.Position = new Vector2(PLAY_FIELD.Width / 2, PLAY_FIELD.Height / 2);

            Vector2 mousePosition = ProjectGameCoordsToScreenCoords(player.Position);
            Mouse.SetPosition((int)mousePosition.X, (int)mousePosition.Y);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            background.Update(gameTime);
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Matrix scaleMatrix = GetScaleMatrix();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, scaleMatrix);
            background.Draw(gameTime);
            player.Draw(gameTime);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        private Matrix GetScaleMatrix()
        {
            return Matrix.CreateScale(
                GameScaleX(),
                GameScaleY(),
                1.0f);
        }

        private float GameScaleY()
        {
            return graphics.PreferredBackBufferHeight / (float)PLAY_FIELD.Height;
        }

        private float GameScaleX()
        {
            return graphics.PreferredBackBufferWidth / (float)PLAY_FIELD.Width;
        }

        internal Vector2 ProjectScreenCoordsToGameCoords(Vector2 point)
        {
            return new Vector2(point.X / GameScaleX(), point.Y / GameScaleY());
        }

        internal Vector2 ProjectGameCoordsToScreenCoords(Vector2 point)
        {
            return new Vector2(point.X * GameScaleX(), point.Y * GameScaleY());
        }
    }
}
