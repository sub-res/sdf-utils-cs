using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SdfUtils.Utility.LinAlg;

namespace SdfUtils.Sdf
{
    //  SDF primitive base class
    abstract class SdfPrimitive : SdfOp
    {
        protected Vector3 diffuse;

        protected SdfPrimitive(Vector3 _diffuse) { diffuse = _diffuse; }
        public override SdfResult CalcSdf(Vector3 p) { return new SdfResult(0, diffuse); }
    }
}
