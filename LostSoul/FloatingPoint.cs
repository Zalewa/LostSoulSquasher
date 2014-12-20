using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostSoul
{
    public class FloatingPoint
    {
        public static bool Compare(float a, float b, float epsilon = float.Epsilon)
        {
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b)
            {
                // shortcut, handles infinities
                return true;
            }
            else
            {
                return diff < epsilon;
            }
        }
    }
}
