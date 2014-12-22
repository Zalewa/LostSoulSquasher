using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class LostSoulSpawner : Entity
    {
        public LostSoulSpawner(LostSoulGame game)
            : base(game)
        {
            actionBehavior = new LostSoulSpawnerBehavior();
        }

        public void ModifyDifficultyByFactor(float factor)
        {
            Difficulty = Difficulty + factor * MaxDifficulty;
        }

        public float Difficulty
        {
            get
            {
                return ((LostSoulSpawnerBehavior)actionBehavior).Difficulty;
            }
            set
            {
                ((LostSoulSpawnerBehavior)actionBehavior).Difficulty = value;
            }
        }

        public float MaxDifficulty
        {
            get
            {
                return LostSoulSpawnerBehavior.MaxDifficulty;
            }
        }
    }
}
