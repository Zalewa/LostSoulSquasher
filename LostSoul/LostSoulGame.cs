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
        public readonly Rectangle PlayField = new Rectangle(0, 0, 640, 480);

        private GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics { get { return graphics; } }

        public SpriteBatch SpriteBatch;
        public ContentLoader ContentLoader = new ContentLoader();
        public AudioSystem Audio { get { return audio; } }

        private AudioSystem audio;
        private LostSoulWorld world;
        public LostSoulWorld World { get { return world; } }

        public LostSoulGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            //IsFixedTimeStep = false;

            Activated += OnActivatedHandler;
            Deactivated += OnDeactivatedHandler;
        }

        public void ResetGame()
        {
            world = new LostSoulWorld(this);
        }

        void OnActivatedHandler(object sender, EventArgs e)
        {
            IsMouseVisible = false;
        }

        void OnDeactivatedHandler(object sender, EventArgs e)
        {
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            audio = new AudioSystem();
            audio.ListenerPosition = new Vector2(PlayField.Center.X, PlayField.Center.Y);
            audio.PanDivisor = PlayField.Width / 2;
            audio.PanClamp = 0.9f;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);

            world = new LostSoulWorld(this);
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

            world.Update(gameTime);
            Audio.FireSounds();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            world.Draw(gameTime);
            base.Draw(gameTime);
        }

        public Matrix GetScaleMatrix()
        {
            return Matrix.CreateScale(GameScaleX(), GameScaleY(), 1.0f);
        }

        private float GameScaleY()
        {
            return graphics.PreferredBackBufferHeight / (float)PlayField.Height;
        }

        private float GameScaleX()
        {
            return graphics.PreferredBackBufferWidth / (float)PlayField.Width;
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
