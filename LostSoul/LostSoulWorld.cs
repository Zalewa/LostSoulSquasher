using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoulWorld
    {
        private LostSoulGame game;
        public LostSoulGame Game { get { return game; } }

        private LostSoulWorldHud hud;

        private Background background;
        private Player player;
        private LostSoulSpawner enemySpawner;
        private List<Entity> actors = new List<Entity>();
        private List<Entity> expiredActors = new List<Entity>();

        private int lostEnemies = 0;
        public int LostEnemies { get { return lostEnemies; } }

        private List<CollisionBehavior> collisions = new List<CollisionBehavior>();

        public int MaxLostSouls = 1;
        public int Score;
        public List<Entity> Actors { get { return actors; } }

        public LostSoulWorld(LostSoulGame game)
        {
            this.game = game;

            hud = new LostSoulWorldHud(this);

            background = new Background(game);
            player = new Player(game);
            player.BodyBehavior.Position = new Vector2(game.PlayField.Center.X, game.PlayField.Center.Y);

            enemySpawner = new LostSoulSpawner(game);

        }

        public void Update(GameTime gameTime)
        {
            background.Update(gameTime);
            if (!player.Expired)
            {
                player.Update(gameTime);
            }
            if (!enemySpawner.Expired)
            {
                enemySpawner.Update(gameTime);
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
        }

        public void Draw(GameTime gameTime)
        {
            Matrix scaleMatrix = game.GetScaleMatrix();
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, scaleMatrix);
            background.Draw(gameTime);
            foreach (Entity actor in actors)
            {
                actor.Draw(gameTime);
            }
            if (!player.Expired)
            {
                player.Draw(gameTime);
            }

            hud.Draw(gameTime);
            SpriteBatch.End();
        }

        public void AddActor(Entity entity)
        {
            actors.Add(entity);
        }

        public void IncrementLostEnemy()
        {
            ++lostEnemies;
            if (IsTooManyLost())
            {
                GoToGameOver();
            }
        }

        private void GoToGameOver()
        {
            player.Expired = true;
            enemySpawner.Expired = true;
            actors.ForEach(e => e.Expired = true);
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
            return IsTooManyLost();
        }

        private bool IsTooManyLost()
        {
            return lostEnemies >= MaxLostSouls;
        }

        private SpriteBatch SpriteBatch
        {
            get
            {
                return game.SpriteBatch;
            }
        }

        private SpriteFont Font
        {
            get
            {
                return game.ContentLoader.Font;
            }
        }
    }
}
