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
        public int Score;
        public AudioSystem Audio { get { return audio; } }

        private AudioSystem audio;
        private Background background;
        private Player player;
        private LostSoulSpawner enemySpawner;
        private List<Entity> actors = new List<Entity>();
        private List<Entity> expiredActors = new List<Entity>();
        private int lostEnemies = 0;
        private List<CollisionBehavior> collisions = new List<CollisionBehavior>();

        public List<Entity> Actors { get { return actors; } }

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

        void OnActivatedHandler(object sender, EventArgs e)
        {
            IsMouseVisible = false;
        }

        void OnDeactivatedHandler(object sender, EventArgs e)
        {
            IsMouseVisible = true;
        }

        public void AddActor(Entity entity)
        {
            actors.Add(entity);
        }

        public void IncrementLostEnemy()
        {
            ++lostEnemies;
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

            background = new Background(this);
            player = new Player(this);
            player.Position = new Vector2(PlayField.Center.X, PlayField.Center.Y);

            enemySpawner = new LostSoulSpawner(this);
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
            enemySpawner.Update(gameTime);
            foreach (Entity actor in actors)
            {
                actor.Update(gameTime);
                if (actor.Expired)
                {
                    expiredActors.Add(actor);
                }
            }
            FirePendingCollisions();
            ClearCollisions();
            RemoveExpiredActors();
            Audio.FireSounds();

            base.Update(gameTime);
        }

        private void RemoveExpiredActors()
        {
            foreach (Entity actor in expiredActors)
            {
                actors.Remove(actor);
                if (actor.HasCollisionBehavior)
                {
                    collisions.Remove(actor.CollisionBehavior);
                }
            }
            expiredActors.Clear();
        }

        private void ClearCollisions()
        {
            collisions.ForEach(e => e.ClearCollisions());
        }

        private void FirePendingCollisions()
        {
            collisions.ForEach(e => e.FirePendingCollisionEvents());
        }

        protected override void Draw(GameTime gameTime)
        {
            Matrix scaleMatrix = GetScaleMatrix();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, scaleMatrix);
            background.Draw(gameTime);
            foreach (Entity actor in actors)
            {
                actor.Draw(gameTime);
            }
            player.Draw(gameTime);

            SpriteBatch.DrawString(ContentLoader.Font, "Score: " + Score, Vector2.Zero, Color.White);
            SpriteBatch.DrawString(ContentLoader.Font, "Lost souls: " + lostEnemies, new Vector2(200.0f, 0.0f), Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        private Matrix GetScaleMatrix()
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

        internal void RegisterCollision(CollisionBehavior collisionBehavior)
        {
            collisions.Add(collisionBehavior);
        }
    }
}
