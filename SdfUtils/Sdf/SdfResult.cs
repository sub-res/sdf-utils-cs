using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
