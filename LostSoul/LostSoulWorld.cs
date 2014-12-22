using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoulWorld : Entity
    {
        public const int StartingLives = 10;

        public event EventHandler GameOverChanged;

        private LostSoulWorldHud hud;

        private Background background;
        public Background Background { get { return background; } }

        private BonusSpawner bonusSpawner;

        private Player player;
        private LostSoulSpawner enemySpawner;
        private List<Entity> actors = new List<Entity>();
        private List<Entity> expiredActors = new List<Entity>();

        private int lives = StartingLives;
        public int Lives { get { return lives; } }

        private List<CollisionBehavior> collisions = new List<CollisionBehavior>();

        public int Score;
        public List<Entity> Actors { get { return actors; } }

        public LostSoulWorld(LostSoulGame game)
            : base(game)
        {
            inputBehavior = new LostSoulWorldInputBehavior(this);
        }

        public void LoadContent()
        {
            hud = new LostSoulWorldHud(this);

            background = new Background(Game);
            player = new Player(Game);
            player.BodyBehavior.Position = new Vector2(Game.PlayField.Center.X, Game.PlayField.Center.Y);

            enemySpawner = new LostSoulSpawner(Game);
            bonusSpawner = new BonusSpawner(this);

            Game.Audio.PlayMusic(Game.ContentLoader.Ambient1);
        }

        public override void Update(GameTime gameTime)
        {
            background.Update(gameTime);
            if (!IsGameOver())
            {
                player.Update(gameTime);
                enemySpawner.Update(gameTime);
                bonusSpawner.Update(gameTime);
            }
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
            hud.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix scaleMatrix = Game.GetScaleMatrix();
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, scaleMatrix);
            background.Draw(gameTime);
            foreach (Entity actor in actors)
            {
                actor.Draw(gameTime);
            }
            if (!IsGameOver())
            {
                player.Draw(gameTime);
            }

            hud.Draw(gameTime);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddActor(Entity entity)
        {
            actors.Add(entity);
        }

        public void AddLives(int amount)
        {
            lives += amount;
            if (!HasLives())
            {
                GoToGameOver();
            }
        }

        private void GoToGameOver()
        {
            player.Expired = true;
            enemySpawner.Expired = true;
            actors.ForEach(e => e.Expired = true);
            OnGameOverChanged();
        }

        public void RegisterCollision(CollisionBehavior collisionBehavior)
        {
            collisions.Add(collisionBehavior);
        }

        private void RemoveExpiredActors()
        {
            foreach (Entity actor in expiredActors)
            {
                actors.Remove(actor);
                if (actor.CollisionBehavior != null)
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

        public bool IsGameOver()
        {
            return !HasLives();
        }

        private bool HasLives()
        {
            return lives > 0;
        }

        private SpriteBatch SpriteBatch
        {
            get
            {
                return Game.SpriteBatch;
            }
        }

        private SpriteFont Font
        {
            get
            {
                return Game.ContentLoader.Font;
            }
        }

        private void OnGameOverChanged()
        {
            if (GameOverChanged != null)
            {
                GameOverChanged(this, EventArgs.Empty);
            }
        }

        public float Difficulty
        {
            get
            {
                return enemySpawner.Difficulty;
            }
        }

        public float MaxDifficulty
        {
            get
            {
                return enemySpawner.MaxDifficulty;
            }
        }
    }
}
