using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public interface IBonusClass
    {
        void Activate(LostSoulWorld world);
        RenderBehavior MkRender(Entity entity);
        float Weight();
    }

    public class BonusOneUp : IBonusClass
    {
        public void Activate(LostSoulWorld world)
        {
            world.AddLives(1);
        }

        public RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusOneUp);
        }

        public float Weight()
        {
            return 1.0f;
        }
    }

    public class BonusEnemySlowDown : IBonusClass
    {
        public void Activate(LostSoulWorld world)
        {
            throw new NotImplementedException();
        }

        public RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusTurtle);
        }

        public float Weight()
        {
            return 1.0f;
        }
    }

    public class BonusAtomBomb : IBonusClass
    {
        public void Activate(LostSoulWorld world)
        {
            world.AddActor(new AtomBomb(world.Game));
        }

        public RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusAtom);
        }

        public float Weight()
        {
            return 0.5f;
        }
    }

    public class BonusDifficultyDecrease : IBonusClass
    {
        public void Activate(LostSoulWorld world)
        {
            throw new NotImplementedException();
        }

        public RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusOneUp);
        }

        public float Weight()
        {
            return 0.2f;
        }
    }

    public class BonusRandomFactory
    {
        private struct BonusClassItem
        {
            public IBonusClass Klass;
            public float MinWeight;
            public float MaxWeight;
        }

        private static Random random = null;
        private static BonusClassItem[] _bonuses = null;

        public static IBonusClass PickRandomBonus()
        {
            if (random == null)
            {
                random = new Random();
            }
            float bonusWeight = (float)random.NextDouble() * bonuses.Last().MaxWeight;
            foreach (BonusClassItem item in bonuses)
            {
                if (bonusWeight >= item.MinWeight && bonusWeight <= item.MaxWeight)
                {
                    return item.Klass;
                }
            }
            return null;
        }

        private static BonusClassItem[] bonuses
        {
            get
            {
                if (_bonuses == null)
                {
                    var klasses = new IBonusClass[] {
                        new BonusAtomBomb(),
                        new BonusOneUp(),
                        new BonusDifficultyDecrease(),
                        new BonusEnemySlowDown()
                    };
                    BonusClassItem[] items = new BonusClassItem[klasses.Length];
                    float weight = 0.0f;
                    for (int i = 0; i < klasses.Length; ++i)
                    {
                        items[i] = new BonusClassItem();
                        items[i].Klass = klasses[i];
                        items[i].MinWeight = weight;
                        items[i].MaxWeight = weight + klasses[i].Weight();
                        weight += klasses[i].Weight();
                    }
                    _bonuses = items;
                }
                return _bonuses;
            }
        }

        internal static RenderBehavior MkBonusRender(Entity entity, Texture2D texture)
        {
            var render = new SpriteRenderBehavior(entity.Game, texture);
            render.Scale = new Vector2(0.5f, 0.5f);
            return render;
        }
    }
}
