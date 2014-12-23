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
        private struct VideoMode
        {
            public int Width;
            public int Height;
            public bool FullScreen;
        };

        private GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics { get { return graphics; } }

        public SpriteBatch SpriteBatch;
        public ContentLoader ContentLoader = new ContentLoader();

        private AudioSystem audio;
        public AudioSystem Audio { get { return audio; } }

        private LostSoulWorld world;
        public LostSoulWorld World { get { return world; } }

        private VideoMode windowedMode;

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
            world.LoadContent();

            audio.ListenerPosition = new Vector2(world.PlayField.Center.X, world.PlayField.Center.Y);
            audio.PanDivisor = world.PlayField.Width / 2;
        }

        public void ToggleFullScreen()
        {
            if (!Graphics.IsFullScreen)
            {
                SwitchToFullScreen();
            }
            else
            {
                SwitchToWindowed();
            }
        }

        private void SwitchToWindowed()
        {
            Graphics.PreferredBackBufferWidth = windowedMode.Width;
            Graphics.PreferredBackBufferHeight = windowedMode.Height;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();
        }

        private void SwitchToFullScreen()
        {
            windowedMode = new VideoMode()
            {
                Width = Graphics.PreferredBackBufferWidth,
                Height = Graphics.PreferredBackBufferHeight,
                FullScreen = false
            };
            var modes = GraphicsDevice.Adapter.SupportedDisplayModes;
            DisplayMode displayMode = GraphicsDevice.Adapter.CurrentDisplayMode;
            Console.WriteLine(displayMode);
            Graphics.PreferredBackBufferWidth = displayMode.Width;
            Graphics.PreferredBackBufferHeight = displayMode.Height;
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
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
            audio = new AudioSystem();
            audio.PanClamp = 0.9f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            LostSoulClasses.LoadContent(this);

            ResetGame();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
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
            if (world == null)
            {
                return 1.0f;
            }
            return graphics.PreferredBackBufferHeight / (float)world.PlayField.Height;
        }

        private float GameScaleX()
        {
            if (world == null)
            {
                return 1.0f;
            }
            return graphics.PreferredBackBufferWidth / (float)world.PlayField.Width;
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
