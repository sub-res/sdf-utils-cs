using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SdfUtils.Utility;
using SdfUtils.Utility.LinAlg;

namespace SdfUtils.Sdf
{
    struct SdfResult
    {
        public readonly float distance; //  calculated signed distance
        public readonly Vector3 diffuse;    //  calculated diffuse material color

        public SdfResult(float _distance, Vector3 _diffuse)
        {
            distance = _distance;
            diffuse = _diffuse;
        }

        public static SdfResult Min(SdfResult a, SdfResult b) { return a.distance < b.distance ? a : b; }
        public static SdfResult Max(SdfResult a, SdfResult b) { return a.distance > b.distance ? a : b; }

        //  smooth min as described by Quilez, also interpolates diffuse
        public static SdfResult SmoothMin(SdfResult a, SdfResult b, float k)
        {
            float h = Algo.Clamp(0.5f + 0.5f * (b.distance - a.distance) / k, 0, 1);
            Vector3 diffuse_h = Vector3.Lerp(b.diffuse, a.diffuse, h);
            return new SdfResult(Algo.Lerp(b.distance, a.distance, h) - k * h * (1 - h), diffuse_h);
        }

        //  max equivalent
        public static SdfResult SmoothMax(SdfResult a, SdfResult b, float k)
        {
            float h = Algo.Clamp(0.5f - 0.5f * (b.distance - a.distance) / k, 0, 1);
            Vector3 diffuse_h = Vector3.Lerp(b.diffuse, a.diffuse, h);
            return new SdfResult(Algo.Lerp(b.distance, a.distance, h) - k * h * (1 - h), diffuse_h);
        }

        public static SdfResult Lerp(SdfResult a, SdfResult b, float t)
        {
            return new SdfResult(Algo.Lerp(a.distance, b.distance, t), Vector3.Lerp(a.diffuse, b.diffuse, t));
        }
    }
}
