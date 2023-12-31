﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdfUtils.Utility
{
    class Algo
    {
        //  basic math helper methods

        public static float Mod(float k, float n)
        {
            return (k %= n) < 0 ? k + n : k;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static float Clamp(float f, float min, float max)
        {
            return Math.Max(min, Math.Min(max, f));
        }
    }
}
