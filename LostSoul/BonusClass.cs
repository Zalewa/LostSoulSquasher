using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public abstract class BonusClass
    {
        public int Score()
        {
            return 10;
        }

        public abstract void Activate(LostSoulWorld world);
        public abstract RenderBehavior MkRender(Entity entity);
        public abstract float Weight();
    }

    public class BonusOneUp : BonusClass
    {
        public override void Activate(LostSoulWorld world)
        {
            world.Game.Audio.PlaySound(world.Game.ContentLoader.OneUpSound);
            world.AddLives(1);
        }

        public override RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusOneUp);
        }

        public override float Weight()
        {
            return 1.0f;
        }
    }

    public class BonusFiveUp : BonusClass
    {
        public override void Activate(LostSoulWorld world)
        {
            world.Game.Audio.PlaySound(world.Game.ContentLoader.OneUpSound);
            world.AddLives(5);
        }

        public override RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusFiveUp);
        }

        public override float Weight()
        {
            return 0.1f;
        }
    }

    public class BonusEnemySlowDown : BonusClass
    {
        public override void Activate(LostSoulWorld world)
        {
            world.AddSpeedModifierActor(new FactorModifierActor(world.Game, 0.2f, 3.0f));
            world.Game.Audio.PlaySound(world.Game.ContentLoader.TurtleSound);
        }

        public override RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusTurtle);
        }

        public override float Weight()
        {
            return 1.0f;
        }
    }

    public class BonusAtomBomb : BonusClass
    {
        public override void Activate(LostSoulWorld world)
        {
            world.AddActor(new AtomBomb(world.Game));
        }

        public override RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusAtom);
        }

        public override float Weight()
        {
            return 0.7f;
        }
    }

    public class BonusDifficultyDecrease : BonusClass
    {
        public override void Activate(LostSoulWorld world)
        {
            world.ModifyDifficultyByFactor(-0.25f);
            world.Game.Audio.PlaySound(world.Game.ContentLoader.BabySound);
        }

        public override RenderBehavior MkRender(Entity entity)
        {
            return BonusRandomFactory.MkBonusRender(entity, entity.Game.ContentLoader.BonusBaby);
        }

        public override float Weight()
        {
            return 0.5f;
        }
    }

    public class BonusRandomFactory
    {
        private struct BonusClassItem
        {
            public BonusClass Klass;
            public float MinWeight;
            public float MaxWeight;
        }

        private static Random random = null;
        private static BonusClassItem[] _bonuses = null;

        public static BonusClass PickRandomBonus()
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
                    var klasses = new BonusClass[] {
                        new BonusAtomBomb(),
                        new BonusOneUp(),
                        new BonusFiveUp(),
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
